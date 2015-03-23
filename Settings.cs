using System;
using ASCOM.Utilities;

namespace ASCOM.LxWebcam
{
	public static class Settings
	{
		private static Profile profile;
		
		static Settings() 
		{
			profile = new Profile();
			profile.DeviceType = "Camera";
        	if(!profile.IsRegistered(Camera.driverID))
        	{
        		profile.Register(Camera.driverID, Camera.driverDescription);
        	}
		}

        public static void Dispose()
        {
            profile.Dispose();
            profile = null;
        }
			
		public static string VideoCaptureDeviceMoniker
		{
			get{ return profile.GetValue(Camera.driverID, "VideoCaptureDeviceMoniker", null); }
			set{ profile.WriteValue(Camera.driverID, "VideoCaptureDeviceMoniker", value); }
		}
		
		public static int VideoWidth
		{
			get
			{ 
				int retVal;
				if(int.TryParse(profile.GetValue(Camera.driverID, "VideoWidth"), out retVal))
				{
					return retVal;
				}
				return 0;
			}
			set{ profile.WriteValue(Camera.driverID, "VideoWidth", value.ToString()); }
		}
		
		public static int VideoHeight
		{
			get
			{ 
				int retVal;
				if(int.TryParse(profile.GetValue(Camera.driverID, "VideoHeight"), out retVal))
				{
					return retVal;
				}
				return 0;
			}
			set{ profile.WriteValue(Camera.driverID, "VideoHeight", value.ToString()); }
		}
		
		public static string ComPort
		{
			get{ return profile.GetValue(Camera.driverID, "ComPort", null); }
			set{ profile.WriteValue(Camera.driverID, "ComPort", value); }
		}
		
		public static string ControlLine
		{
			get{ return profile.GetValue(Camera.driverID, "ControlLine", null); }
			set{ profile.WriteValue(Camera.driverID, "ControlLine", value); }
		}
		
		public static bool Invert
		{
			get
			{
				bool retVal;
				if(bool.TryParse(profile.GetValue(Camera.driverID, "Invert"), out retVal))
				{
					return retVal;
				}
				return false;
			}
			set{ profile.WriteValue(Camera.driverID, "Invert", value.ToString()); }
		}
	}
}
