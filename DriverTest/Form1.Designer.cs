namespace ASCOM.LxWebcam
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelDriverId = new System.Windows.Forms.Label();
            this.buttonTakePicture = new System.Windows.Forms.Button();
            this.progPercentageComplete = new System.Windows.Forms.ProgressBar();
            this.udExposureLength = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.udExposureLength)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(309, 10);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(72, 23);
            this.buttonChoose.TabIndex = 0;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(309, 39);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelDriverId
            // 
            this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDriverId.Location = new System.Drawing.Point(12, 40);
            this.labelDriverId.Name = "labelDriverId";
            this.labelDriverId.Size = new System.Drawing.Size(291, 21);
            this.labelDriverId.TabIndex = 2;
            this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonTakePicture
            // 
            this.buttonTakePicture.Location = new System.Drawing.Point(55, 76);
            this.buttonTakePicture.Name = "buttonTakePicture";
            this.buttonTakePicture.Size = new System.Drawing.Size(84, 23);
            this.buttonTakePicture.TabIndex = 3;
            this.buttonTakePicture.Text = "Take Picture";
            this.buttonTakePicture.UseVisualStyleBackColor = true;
            this.buttonTakePicture.Click += new System.EventHandler(this.ButtonTakePictureClick);
            // 
            // progPercentageComplete
            // 
            this.progPercentageComplete.Location = new System.Drawing.Point(145, 76);
            this.progPercentageComplete.Name = "progPercentageComplete";
            this.progPercentageComplete.Size = new System.Drawing.Size(236, 23);
            this.progPercentageComplete.TabIndex = 4;
            // 
            // udExposureLength
            // 
            this.udExposureLength.DecimalPlaces = 1;
            this.udExposureLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udExposureLength.Location = new System.Drawing.Point(12, 79);
            this.udExposureLength.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udExposureLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.udExposureLength.Name = "udExposureLength";
            this.udExposureLength.Size = new System.Drawing.Size(37, 20);
            this.udExposureLength.TabIndex = 5;
            this.udExposureLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 112);
            this.Controls.Add(this.udExposureLength);
            this.Controls.Add(this.progPercentageComplete);
            this.Controls.Add(this.buttonTakePicture);
            this.Controls.Add(this.labelDriverId);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonChoose);
            this.Name = "Form1";
            this.Text = "Driver Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.udExposureLength)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button buttonTakePicture;

        #endregion

        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelDriverId;
        private System.Windows.Forms.ProgressBar progPercentageComplete;
        private System.Windows.Forms.NumericUpDown udExposureLength;
    }
}

