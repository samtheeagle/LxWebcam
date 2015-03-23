//tabs=4
// --------------------------------------------------------------------------------
//
// ASCOM Camera driver for LxWebcam
//
// Description:	ASCOM camera driver for Lx Modified webcams.
//
// Implements:	ASCOM Camera interface version: 2
// Author:		Guy Webb (samtheeagle@hotmail.com)
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 17-Mar-2013	GW	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//

// This is used to define code in the template that is specific to one class implementation
// unused code can be deleted and this definition removed.
#define Camera

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;

using AForge;
using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using ASCOM.DeviceInterface;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace ASCOM.LxWebcam
{
	//
	// Your driver's DeviceID is ASCOM.LxWebcam.Camera
	//
	// The Guid attribute sets the CLSID for ASCOM.LxWebcam.Camera
	// The ClassInterface/None addribute prevents an empty interface called
	// _LxWebcam from being created and used as the [default] interface
	//

	/// <summary>
	/// ASCOM Camera Driver for LxWebcam.
	/// </summary>
	[Guid("f318b512-d71d-447f-84f1-cf36fb1b9263")]
	[ClassInterface(ClassInterfaceType.None)]
	public class Camera : ICameraV2
	{
		/// <summary>
		/// ASCOM DeviceID (COM ProgID) for this driver.
		/// The DeviceID is used by ASCOM applications to load the driver at runtime.
		/// </summary>
		public static string driverID = "ASCOM.LxWebcam.Camera";
		/// <summary>
		/// Driver description that displays in the ASCOM Chooser.
		/// </summary>
		public static string driverDescription = "ASCOM Camera Driver for LxWebcam";

		FrameCapture frameCapture;
		SerialPort lxControl;
		bool imageReady = false;
		DateTime? lastExposureStartTime;
		double requestedExposureDuration;
		DateTime? lastExposureEndTime;
		Bitmap availableImage;
		CameraStates cameraState = CameraStates.cameraIdle;
        Timer exposureTimer;

		/// <summary>
		/// Initializes a new instance of the <see cref="LxWebcam"/> class.
		/// Must be public for COM registration.
		/// </summary>
		public Camera()
		{
			cameraState = CameraStates.cameraIdle;
			NumX = CameraXSize;
			NumY = CameraYSize;
		}


		void FrameCaptureNewFrameEvent(object sender, Bitmap newFrame)
		{
			cameraState = CameraStates.cameraReading;
			availableImage = (Bitmap)newFrame.Clone();
			imageReady = true;
			cameraState = CameraStates.cameraIdle;
		}

		void StartLx()
		{
			switch (Settings.ControlLine)
			{
				case "RTS":
					lxControl.RtsEnable = !Settings.Invert;
					break;
				case "DTR":
					lxControl.RtsEnable = !Settings.Invert;
					break;
			}
		}

		void StopLx()
		{
			switch (Settings.ControlLine)
			{
				case "RTS":
					lxControl.RtsEnable = Settings.Invert;
					break;
				case "DTR":
					lxControl.RtsEnable = Settings.Invert;
					break;
			}
		}

		#region ASCOM Registration
		//
		// Register or unregister driver for ASCOM. This is harmless if already
		// registered or unregistered. 
		//
		/// <summary>
		/// Register or unregister the driver with the ASCOM Platform.
		/// This is harmless if the driver is already registered/unregistered.
		/// </summary>
		/// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
		static void RegUnregASCOM(bool bRegister)
		{
			using (var P = new ASCOM.Utilities.Profile())
			{
				P.DeviceType = "Camera";
				if (bRegister)
				{
					P.Register(driverID, driverDescription);
				}
				else
				{
					P.Unregister(driverID);
				}
			}
		}

		/// <summary>
		/// This function registers the driver with the ASCOM Chooser and
		/// is called automatically whenever this class is registered for COM Interop.
		/// </summary>
		/// <param name="t">Type of the class being registered, not used.</param>
		/// <remarks>
		/// This method typically runs in two distinct situations:
		/// <list type="numbered">
		/// <item>
		/// In Visual Studio, when the project is successfully built.
		/// For this to work correctly, the option <c>Register for COM Interop</c>
		/// must be enabled in the project settings.
		/// </item>
		/// <item>During setup, when the installer registers the assembly for COM Interop.</item>
		/// </list>
		/// This technique should mean that it is never necessary to manually register a driver with ASCOM.
		/// </remarks>
		[ComRegisterFunction]
		public static void RegisterASCOM(Type t)
		{
			RegUnregASCOM(true);
		}

		/// <summary>
		/// This function unregisters the driver from the ASCOM Chooser and
		/// is called automatically whenever this class is unregistered from COM Interop.
		/// </summary>
		/// <param name="t">Type of the class being registered, not used.</param>
		/// <remarks>
		/// This method typically runs in two distinct situations:
		/// <list type="numbered">
		/// <item>
		/// In Visual Studio, when the project is cleaned or prior to rebuilding.
		/// For this to work correctly, the option <c>Register for COM Interop</c>
		/// must be enabled in the project settings.
		/// </item>
		/// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
		/// </list>
		/// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
		/// </remarks>
		[ComUnregisterFunction]
		public static void UnregisterASCOM(Type t)
		{
			RegUnregASCOM(false);
		}
		#endregion

		//
		// PUBLIC COM INTERFACE ICameraV2 IMPLEMENTATION
		//

		/// <summary>
		/// Displays the Setup Dialog form.
		/// If the user clicks the OK button to dismiss the form, then
		/// the new settings are saved, otherwise the old values are reloaded.
		/// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
		/// </summary>
		public void SetupDialog()
		{
			// consider only showing the setup dialog if not connected
			// or call a different dialog if connected
			if (IsConnected)
				System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

			using (SetupDialogForm F = new SetupDialogForm())
			{
				var result = F.ShowDialog();
				if (result == System.Windows.Forms.DialogResult.OK)
				{
					return;
				}
			}
		}


		#region common properties and methods. All set to no action

		public ArrayList SupportedActions
		{
			get { return new ArrayList(); }
		}

		public string Action(string actionName, string actionParameters)
		{
			throw new ASCOM.MethodNotImplementedException("Action");
		}

		public void CommandBlind(string command, bool raw)
		{
			CheckConnected("CommandBlind");
			// Call CommandString and return as soon as it finishes
			this.CommandString(command, raw);
			// or
			throw new ASCOM.MethodNotImplementedException("CommandBlind");
		}

		public bool CommandBool(string command, bool raw)
		{
			CheckConnected("CommandBool");
			string ret = CommandString(command, raw);
			throw new ASCOM.MethodNotImplementedException("CommandBool");
		}

		public string CommandString(string command, bool raw)
		{
			CheckConnected("CommandString");
			// it's a good idea to put all the low level communication with the device here,
			// then all communication calls this function
			// you need something to ensure that only one command is in progress at a time

			throw new ASCOM.MethodNotImplementedException("CommandString");
		}

		#endregion

		#region public properties and methods
		public void Dispose()
		{
			if (null != lxControl)
			{
				StopLx();
				lxControl.Close();
				lxControl = null;
			}
			if(null != frameCapture)
			{
				frameCapture.Stop();
				frameCapture = null;
			}
		}

		public bool Connected
		{
			get { return IsConnected; }
			set
			{
				if (value == IsConnected)
					return;

				if (value)
				{
					if(null != Settings.VideoCaptureDeviceMoniker)
					{
						VideoCaptureDevice videoCaptureDevice = 
							new VideoCaptureDevice(Settings.VideoCaptureDeviceMoniker);
						videoCaptureDevice.DesiredFrameSize = 
							new Size(Settings.VideoWidth, Settings.VideoHeight);
						videoCaptureDevice.DesiredFrameRate = 5; // TODO - could be a configurable value.
						
						frameCapture = new FrameCapture(videoCaptureDevice);
						frameCapture.NewFrameEvent += FrameCaptureNewFrameEvent;
						frameCapture.Start();
					}
					if (null != Settings.ComPort)
					{
						lxControl = new SerialPort();
						lxControl.PortName = Settings.ComPort;
						lxControl.Open();
					}
				}
				else
				{
					if(null != frameCapture) {
						StopLx();
						frameCapture.Stop();
						frameCapture = null;
					}
					if(null != lxControl) 
					{
						lxControl.Close();
						lxControl = null;
					}
				}
			}
		}

		public string Description
		{
			get { return driverDescription; }
		}

		public string DriverInfo
		{
			get { return driverDescription; }
		}

		public string DriverVersion
		{
			get
			{
				Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
				return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
			}
		}

		public short InterfaceVersion
		{
			// set by the driver wizard
			get { return 2; }
		}

		#endregion

		#region private properties and methods
		// here are some useful properties and methods that can be used as required
		// to help with

		/// <summary>
		/// Returns true if there is a valid connection to the driver hardware
		/// </summary>
		private bool IsConnected
		{
			get
			{
				return frameCapture != null && lxControl != null && lxControl.IsOpen;
			}
		}

		/// <summary>
		/// Use this function to throw an exception if we aren't connected to the hardware
		/// </summary>
		/// <param name="message"></param>
		private void CheckConnected(string message)
		{
			if (!IsConnected)
			{
				throw new ASCOM.NotConnectedException(message);
			}
		}
		#endregion

		public void AbortExposure()
		{
			if (CameraStates.cameraReading == cameraState || CameraStates.cameraDownload == cameraState)
			{
				throw new InvalidOperationException();
			}
			StopLx();
            exposureTimer.Dispose();
			if (null != availableImage)
			{
				availableImage.Dispose();
			}
			cameraState = CameraStates.cameraIdle;
		}

		public short BayerOffsetX
		{
			get { return 0; }
		}

		public short BayerOffsetY
		{
			get { return 0; }
		}

		public short BinX
		{
			get
			{
				return 1;
			}
			set
			{
				if(1 != value) throw new InvalidValueException();
			}
		}

		public short BinY
		{
			get
			{
				return 1;
			}
			set
			{
				if (1 != value) throw new InvalidValueException();
			}
		}

		public double CCDTemperature
		{
			get { throw new PropertyNotImplementedException(); }
		}

		public CameraStates CameraState
		{
			get { return cameraState; }
		}

		public int CameraXSize
		{
			get { return Settings.VideoWidth; }
		}

		public int CameraYSize
		{
			get { return Settings.VideoHeight; }
		}

		public bool CanAbortExposure
		{
			get { return true; }
		}

		public bool CanAsymmetricBin
		{
			get { return false; }
		}

		public bool CanFastReadout
		{
			get { return false; }
		}

		public bool CanGetCoolerPower
		{
			get { return false; }
		}

		public bool CanPulseGuide
		{
			get { return false; }
		}

		public bool CanSetCCDTemperature
		{
			get { return false; }
		}

		public bool CanStopExposure
		{
			get { return true; }
		}

		public bool CoolerOn
		{
			get
			{
				throw new MethodNotImplementedException();
			}
			set
			{
				throw new MethodNotImplementedException();
			}
		}

		public double CoolerPower
		{
			get { throw new MethodNotImplementedException(); }
		}

		public double ElectronsPerADU
		{
			get { throw new MethodNotImplementedException(); }
		}

		public double ExposureMax // TODO - could be a configurable value.
		{
			get { return 120; }
		}

		public double ExposureMin // TODO - could be a configurable value.
		{
			get { return 0.5; }
		}

		public double ExposureResolution // TODO - could be a configurable value.
		{
			get { return 0.5; }
		}

		public bool FastReadout
		{
			get
			{
				throw new MethodNotImplementedException();
			}
			set
			{
				throw new MethodNotImplementedException();
			}
		}

		public double FullWellCapacity
		{
			get { throw new MethodNotImplementedException(); }
		}

		public short Gain
		{
			get
			{
				throw new MethodNotImplementedException();
			}
			set
			{
				throw new MethodNotImplementedException();
			}
		}

		public short GainMax
		{
			get { throw new MethodNotImplementedException(); }
		}

		public short GainMin
		{
			get { throw new MethodNotImplementedException(); }
		}

		public ArrayList Gains
		{
			get { throw new MethodNotImplementedException(); }
		}

		public bool HasShutter
		{
			get { return false; }
		}

		public double HeatSinkTemperature
		{
			get { throw new MethodNotImplementedException(); }
		}

		// https://groups.yahoo.com/neo/groups/ASCOM-Talk/conversations/topics/24698
		// http://stackoverflow.com/questions/17387509/how-to-convert-1d-byte-array-to-2d-byte-array-which-holds-a-bitmap
		public object ImageArray
		{
			get 
			{
				if (false == ImageReady)
				{
					throw new InvalidOperationException();
				}
				cameraState = CameraStates.cameraDownload;
				var imageData = new int[CameraXSize, CameraYSize, 3];
				// *************************************************************
				// Bitmap data byte order appears to BestFitMappingAttribute BGR
				// *************************************************************
				var bitmapData = availableImage.LockBits(new Rectangle(0, 0, availableImage.Width, availableImage.Height),
														 ImageLockMode.ReadOnly,
														 availableImage.PixelFormat);
				try
				{
					var length = bitmapData.Stride * bitmapData.Height;
					byte[] bytes = new byte[length];
					Marshal.Copy(bitmapData.Scan0, bytes, 0, length);

					unsafe
					{
						byte* scan0 = (byte*)bitmapData.Scan0.ToPointer();
						int stride = bitmapData.Stride;
						for (int y = 0; y < bitmapData.Height; y++)
						{
							int row = y * stride;
							byte* currentPixelPointer = scan0 + row;

							for (int x = 0; x < bitmapData.Width; x++)
							{
								imageData[x, y, 0] = currentPixelPointer[0]; // BLUE
								imageData[x, y, 1] = currentPixelPointer[1]; // GREEN
								imageData[x, y, 2] = currentPixelPointer[2]; // RED
								currentPixelPointer += 3;
							}
						};
					}
				}
				finally
				{
					availableImage.UnlockBits(bitmapData);
					cameraState = CameraStates.cameraIdle;
				}
				return imageData;
			}
		}

		public object ImageArrayVariant
		{
			get 
			{
				if (false == ImageReady)
				{
					throw new InvalidOperationException();
				}
				cameraState = CameraStates.cameraDownload;
				var imageData = new object[CameraXSize, CameraYSize, 3];
				// *************************************************************
				// Bitmap data byte order appears to BestFitMappingAttribute BGR
				// *************************************************************
				var bitmapData = availableImage.LockBits(new Rectangle(0, 0, availableImage.Width, availableImage.Height),
														 ImageLockMode.ReadOnly,
														 availableImage.PixelFormat);
				try
				{
					var length = bitmapData.Stride * bitmapData.Height;
					byte[] bytes = new byte[length];
					Marshal.Copy(bitmapData.Scan0, bytes, 0, length);

					unsafe
					{
						byte* scan0 = (byte*)bitmapData.Scan0.ToPointer();
						int stride = bitmapData.Stride;
						for (int y = 0; y < bitmapData.Height; y++)
						{
							int row = y * stride;
							byte* currentPixelPointer = scan0 + row;

							for (int x = 0; x < bitmapData.Width; x++)
							{
								imageData[x, y, 0] = (int)currentPixelPointer[0]; // BLUE
								imageData[x, y, 1] = (int)currentPixelPointer[1]; // GREEN
								imageData[x, y, 2] = (int)currentPixelPointer[2]; // RED
								currentPixelPointer += 3;
							}
						};
					}
				}
				finally
				{
					availableImage.UnlockBits(bitmapData);
					cameraState = CameraStates.cameraIdle;
				}
				return imageData;
			}
		}

		public bool ImageReady
		{
			get { return imageReady; }
		}

		public bool IsPulseGuiding
		{
			get { throw new MethodNotImplementedException(); }
		}

		public double LastExposureDuration
		{
			get
			{
				if (lastExposureStartTime.HasValue && lastExposureEndTime.HasValue)
				{
					return lastExposureEndTime.Value.Subtract(lastExposureStartTime.Value).Seconds;
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}

		public string LastExposureStartTime
		{
			get 
			{
				if (lastExposureStartTime.HasValue)
				{
					return lastExposureStartTime.Value.ToString("yyyy-MM-ddTHH:mm:ss");
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}

		public int MaxADU
		{
			get { throw new MethodNotImplementedException(); }
		}

		public short MaxBinX
		{
			get { return 1; }
		}

		public short MaxBinY
		{
			get { return 1; }
		}

		public string Name
		{
			get { return "LxWebcam"; }
		}

		public int NumX
		{
			get;
			set;
		}

		public int NumY
		{
			get;
			set;
		}

		public short PercentCompleted
		{
			get
			{
				TimeSpan exposedSoFar = lastExposureStartTime.HasValue ? DateTime.Now.Subtract(lastExposureStartTime.Value) : new TimeSpan(0);
				short pct = (short)((exposedSoFar.TotalMilliseconds / (requestedExposureDuration * 1000)) * 100);
				return pct > 100 ? (short)100 : pct;
			}
		}

		public double PixelSizeX // TODO - could be a configurable value.
		{
			get { return 5.6; }
		}

		public double PixelSizeY // TODO - could be a configurable value.
		{
			get { return 5.6; }
		}

		public void PulseGuide(GuideDirections Direction, int Duration)
		{
			throw new MethodNotImplementedException();
		}

		public short ReadoutMode
		{
			get
			{
				return 0;
			}
			set
			{
				//nop throw new MethodNotImplementedException();
			}
		}

		public ArrayList ReadoutModes
		{
			get { return new ArrayList(){"Normal"}; }
		}

		public string SensorName // TODO - could be a configurable value.
		{
			get { return "ICX098QB"; }
		}

		public SensorType SensorType // TODO - could be a configurable value.
		{
			get { return SensorType.Color; }
		}

		public double SetCCDTemperature
		{
			get
			{
				throw new MethodNotImplementedException();
			}
			set
			{
				throw new MethodNotImplementedException();
			}
		}

		/// <summary>
		/// Starts an exposure. Use ImageReady to check when the exposure is complete.
		/// </summary>
		/// <param name="Duration">Duration of exposure in seconds, can be zero if Light is false</param>
		/// <param name="Light">True for light frame, False for dark frame (ignored if no shutter)</param>
		public void StartExposure(double Duration, bool Light)
		{
			if (CameraXSize != NumX || CameraYSize != NumY || 0 != StartX || 0 != StartY || 0 >= Duration)
			{
				throw new InvalidValueException();
			}
			cameraState = CameraStates.cameraExposing; 
			imageReady = false;
			if(null != availableImage)
			{
				availableImage.Dispose();
				availableImage = null;
			}
			requestedExposureDuration = Duration;
			lastExposureStartTime = DateTime.Now;
			lastExposureEndTime = null;
			StartLx();
            exposureTimer = new Timer((callback) => 
            {
                lastExposureEndTime = DateTime.Now; 
                StopLx();
                frameCapture.AwaitLxFrame();
            }, null, (int)Duration * 1000, Timeout.Infinite);
		}

		public int StartX
		{
			get;
			set;
		}

		public int StartY
		{
			get;
			set;
		}

		public void StopExposure()
		{
            exposureTimer.Change(Timeout.Infinite, Timeout.Infinite);
			lastExposureEndTime = DateTime.Now;
			StopLx();
			frameCapture.AwaitLxFrame();
		}
	}
}
