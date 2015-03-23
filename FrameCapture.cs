using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics;

namespace ASCOM.LxWebcam
{
    class FrameCapture
    {
        private readonly VideoCaptureDevice _videoCaptureDevice;
        private readonly LinkedList<FrameInfo> lxFrameCandidateBuffer = new LinkedList<FrameInfo>();
        private bool awaitingLxFrame;

        #region Events

        public delegate void StatusUpdateEventHandler(object sender, string status);
        public event StatusUpdateEventHandler StatusUpdateEvent;
        protected virtual void OnStatusUpdateEvent(string status)
        {
            if (StatusUpdateEvent != null)
                StatusUpdateEvent(this, status);
        }

        public delegate void NewFrameEventHandler(object sender, Bitmap lxFrame);
        public event NewFrameEventHandler NewFrameEvent;
        protected virtual void OnNewFrameEvent(Bitmap newFrame)
        {
            if (NewFrameEvent != null)
                NewFrameEvent(this, newFrame);
        }

        #endregion

        public FrameCapture(VideoCaptureDevice videoCaptureDevice)
        {
            _videoCaptureDevice = videoCaptureDevice;
            _videoCaptureDevice.NewFrame += _videoCaptureDevice_NewFrame;
        }

        ~FrameCapture()
        {
            if (null != _videoCaptureDevice)
            {
                _videoCaptureDevice.SignalToStop();
                _videoCaptureDevice.WaitForStop();
            }
        }

        public VideoCaptureDevice VideoCaptureDevice
        {
            get { return _videoCaptureDevice; }
        }

        /// <summary>
        /// Start the video frame capture.
        /// </summary>
        public void Start()
        {
            _videoCaptureDevice.Start();
        }

        public void AwaitLxFrame()
        {
            awaitingLxFrame = true;
            //Debug.WriteLine("AwaitLxFrame");
        }

        /// <summary>
        /// Stop the video frame capture.
        /// </summary>
        public void Stop()
        {
            _videoCaptureDevice.SignalToStop();
            _videoCaptureDevice.WaitForStop();
        }

        void _videoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Take a copy of the frame as the original will get disposed of lead to errors if we hold a reference to it.
            FrameInfo frameInfo = new FrameInfo((Bitmap)eventArgs.Frame.Clone(), DateTime.Now);
            //Debug.WriteLine(frameInfo.FrameScore);
            if (awaitingLxFrame)
            {
                BufferLxCandidateFrameInfo(frameInfo);
                if (lxFrameCandidateBuffer.Count == 3)
                {
                    OnNewFrameEvent(GetBestLxFrameCandidate().Frame);
                    awaitingLxFrame = false;
                }
            }
        }

        private void BufferLxCandidateFrameInfo(FrameInfo frameInfo)
        {
            lxFrameCandidateBuffer.AddLast(frameInfo);
        }

        private FrameInfo GetBestLxFrameCandidate()
        {
            FrameInfo retVal = null;
            foreach (FrameInfo frameInfo in lxFrameCandidateBuffer)
            {
                if (null == retVal || frameInfo.FrameScore > retVal.FrameScore)
                    retVal = frameInfo;
            }
            lxFrameCandidateBuffer.Clear();
            return retVal;
        }
    }
}