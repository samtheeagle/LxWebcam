using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ASCOM.Utilities;

namespace ASCOM.LxWebcam
{
	[ComVisible(false)]					// Form not registered for COM!
	public partial class SetupDialogForm : Form
	{
		private readonly VideoCaptureDeviceForm deviceSelectionForm = new VideoCaptureDeviceForm();
		private DialogResult dialogResult;

		public SetupDialogForm()
		{
			InitializeComponent();
			Serial serial = new Serial();
			cboComPorts.Items.Clear();
			foreach(string comPortName in serial.AvailableCOMPorts)
			{
				cboComPorts.Items.Add(comPortName);
			}
			serial.Dispose();
			txtSelectedWebcam.Text = GetDeviceNameFromMoniker(Settings.VideoCaptureDeviceMoniker);
			cboComPorts.Text = Settings.ComPort;
			rbRTS.Checked = Settings.ControlLine == "RTS";
			rbDTR.Checked = Settings.ControlLine == "DTR";
			cbInvert.Checked = Settings.Invert;
		}

		~SetupDialogForm()
		{
			deviceSelectionForm.Dispose();
		}
		
		void BtnSelectWebcamClick(object sender, System.EventArgs e)
		{
			dialogResult = deviceSelectionForm.ShowDialog();
			if (dialogResult == DialogResult.OK && null != deviceSelectionForm.VideoDevice)
			{
				txtSelectedWebcam.Text = GetDeviceNameFromMoniker(deviceSelectionForm.VideoDeviceMoniker);
				btnProperties.Enabled = true;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (dialogResult == DialogResult.OK && null != deviceSelectionForm.VideoDevice)
			{
				Settings.VideoCaptureDeviceMoniker = deviceSelectionForm.VideoDeviceMoniker;
				Settings.VideoWidth = deviceSelectionForm.CaptureSize.Width;
				Settings.VideoHeight = deviceSelectionForm.CaptureSize.Height;
			}
			Settings.ComPort = cboComPorts.Text;
			Settings.ControlLine = rbRTS.Checked ? "RTS" : "DTR";
			Settings.Invert  = cbInvert.Checked;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void BtnPropertiesClick(object sender, EventArgs e)
		{
			deviceSelectionForm.VideoDevice.DisplayPropertyPage(Handle);
		}

		private void BrowseToAscom(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("http://ascom-standards.org/");
			}
			catch (System.ComponentModel.Win32Exception noBrowser)
			{
				if (noBrowser.ErrorCode == -2147467259)
					MessageBox.Show(noBrowser.Message);
			}
			catch (System.Exception other)
			{
				MessageBox.Show(other.Message);
			}
		}
		
		private String GetDeviceNameFromMoniker(String moniker) {
			var videoDevices = new FilterInfoCollection( FilterCategory.VideoInputDevice );
			foreach(FilterInfo finfo in videoDevices) {
				if(finfo.MonikerString.Equals(moniker)) {
				   return finfo.Name;
				}
			}
			return null;
		}
	}
}