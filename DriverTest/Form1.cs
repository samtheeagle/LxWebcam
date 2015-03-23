using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ASCOM.LxWebcam
{
    public partial class Form1 : Form
    {

        private DriverAccess.Camera driver;
        private string driverId;

        public Form1()
        {
            InitializeComponent();
            SetUIState();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
            {
                driver.Connected = false;
                driver.Dispose();
            }
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            driverId = DriverAccess.Camera.Choose("");
            SetUIState();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                driver.Connected = false;
            }
            else
            {
                driver = new DriverAccess.Camera(driverId);
                driver.Connected = true;
            }
            SetUIState();
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(driverId);
            buttonChoose.Enabled = !IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
            buttonTakePicture.Enabled = IsConnected;
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        private void ButtonTakePictureClick(object sender, EventArgs e)
        {
            Debug.WriteLine("Image Ready: " + driver.ImageReady);

            driver.StartExposure(Decimal.ToDouble(udExposureLength.Value), true);

            //System.Threading.Thread.Sleep(200);
            //driver.AbortExposure();

            DateTime start = DateTime.Now;
            while (!driver.ImageReady)
            {
                progPercentageComplete.Value = driver.PercentCompleted;
                if (DateTime.Now.Subtract(start).Seconds > 5) { break; }
            }
            try
            {
                progPercentageComplete.Value = driver.PercentCompleted;
                Debug.WriteLine("Exposure Complete");
                var foo = driver.ImageArray;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
