using System;
using System.Drawing;
using AForge.Imaging;

namespace ASCOM.LxWebcam
{
    class FrameInfo
    {
        private readonly Bitmap _frame;
        private readonly DateTime _frameTime;
        private readonly double _frameScore;

        public FrameInfo(Bitmap frame, DateTime frameTime)
        {
            _frame = frame;
            _frameTime = frameTime;
            _frameScore = GetFrameScore(frame);
        }

        ~FrameInfo()
        {
            _frame.Dispose();
        }

        public Bitmap Frame
        {
            get { return _frame; }
        }

        public DateTime FrameTime
        {
            get { return _frameTime; }
        }

        public double FrameScore
        {
            get { return _frameScore; }
        }

        private static double GetFrameScore(Bitmap frame)
        {
            ImageStatistics frameStats = new ImageStatistics(frame);
            return frameStats.Red.Mean + frameStats.Green.Mean + frameStats.Blue.Mean;
        }
    }
}
