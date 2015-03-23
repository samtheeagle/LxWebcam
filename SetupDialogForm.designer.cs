namespace ASCOM.LxWebcam
{
    partial class SetupDialogForm
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
        	this.cmdOK = new System.Windows.Forms.Button();
        	this.cmdCancel = new System.Windows.Forms.Button();
        	this.picASCOM = new System.Windows.Forms.PictureBox();
        	this.btnSelectWebcam = new System.Windows.Forms.Button();
        	this.txtSelectedWebcam = new System.Windows.Forms.TextBox();
        	this.cboComPorts = new System.Windows.Forms.ComboBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.rbRTS = new System.Windows.Forms.RadioButton();
        	this.rbDTR = new System.Windows.Forms.RadioButton();
        	this.grpControlLine = new System.Windows.Forms.GroupBox();
        	this.cbInvert = new System.Windows.Forms.CheckBox();
        	this.btnProperties = new System.Windows.Forms.Button();
        	((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
        	this.grpControlLine.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// cmdOK
        	// 
        	this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.cmdOK.Location = new System.Drawing.Point(281, 112);
        	this.cmdOK.Name = "cmdOK";
        	this.cmdOK.Size = new System.Drawing.Size(59, 24);
        	this.cmdOK.TabIndex = 0;
        	this.cmdOK.Text = "OK";
        	this.cmdOK.UseVisualStyleBackColor = true;
        	this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
        	// 
        	// cmdCancel
        	// 
        	this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.cmdCancel.Location = new System.Drawing.Point(281, 142);
        	this.cmdCancel.Name = "cmdCancel";
        	this.cmdCancel.Size = new System.Drawing.Size(59, 25);
        	this.cmdCancel.TabIndex = 1;
        	this.cmdCancel.Text = "Cancel";
        	this.cmdCancel.UseVisualStyleBackColor = true;
        	this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
        	// 
        	// picASCOM
        	// 
        	this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
        	this.picASCOM.Image = global::ASCOM.LxWebcam.Properties.Resources.ASCOM;
        	this.picASCOM.Location = new System.Drawing.Point(292, 9);
        	this.picASCOM.Name = "picASCOM";
        	this.picASCOM.Size = new System.Drawing.Size(48, 56);
        	this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        	this.picASCOM.TabIndex = 3;
        	this.picASCOM.TabStop = false;
        	this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
        	this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
        	// 
        	// btnSelectWebcam
        	// 
        	this.btnSelectWebcam.Location = new System.Drawing.Point(201, 7);
        	this.btnSelectWebcam.Name = "btnSelectWebcam";
        	this.btnSelectWebcam.Size = new System.Drawing.Size(27, 23);
        	this.btnSelectWebcam.TabIndex = 5;
        	this.btnSelectWebcam.Text = "...";
        	this.btnSelectWebcam.UseVisualStyleBackColor = true;
        	this.btnSelectWebcam.Click += new System.EventHandler(this.BtnSelectWebcamClick);
        	// 
        	// txtSelectedWebcam
        	// 
        	this.txtSelectedWebcam.Location = new System.Drawing.Point(12, 9);
        	this.txtSelectedWebcam.Name = "txtSelectedWebcam";
        	this.txtSelectedWebcam.ReadOnly = true;
        	this.txtSelectedWebcam.Size = new System.Drawing.Size(183, 20);
        	this.txtSelectedWebcam.TabIndex = 6;
        	this.txtSelectedWebcam.Text = "No Webcam Selected";
        	// 
        	// cboComPorts
        	// 
        	this.cboComPorts.FormattingEnabled = true;
        	this.cboComPorts.Location = new System.Drawing.Point(109, 44);
        	this.cboComPorts.Name = "cboComPorts";
        	this.cboComPorts.Size = new System.Drawing.Size(86, 21);
        	this.cboComPorts.TabIndex = 7;
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(9, 47);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(76, 13);
        	this.label1.TabIndex = 8;
        	this.label1.Text = "Lx Control Port";
        	// 
        	// rbRTS
        	// 
        	this.rbRTS.AutoSize = true;
        	this.rbRTS.Checked = true;
        	this.rbRTS.Location = new System.Drawing.Point(6, 20);
        	this.rbRTS.Name = "rbRTS";
        	this.rbRTS.Size = new System.Drawing.Size(47, 17);
        	this.rbRTS.TabIndex = 9;
        	this.rbRTS.TabStop = true;
        	this.rbRTS.Text = "RTS";
        	this.rbRTS.UseVisualStyleBackColor = true;
        	// 
        	// rbDTR
        	// 
        	this.rbDTR.AutoSize = true;
        	this.rbDTR.Location = new System.Drawing.Point(6, 43);
        	this.rbDTR.Name = "rbDTR";
        	this.rbDTR.Size = new System.Drawing.Size(48, 17);
        	this.rbDTR.TabIndex = 10;
        	this.rbDTR.Text = "DTR";
        	this.rbDTR.UseVisualStyleBackColor = true;
        	// 
        	// grpControlLine
        	// 
        	this.grpControlLine.Controls.Add(this.cbInvert);
        	this.grpControlLine.Controls.Add(this.rbRTS);
        	this.grpControlLine.Controls.Add(this.rbDTR);
        	this.grpControlLine.Location = new System.Drawing.Point(12, 80);
        	this.grpControlLine.Name = "grpControlLine";
        	this.grpControlLine.Size = new System.Drawing.Size(183, 70);
        	this.grpControlLine.TabIndex = 11;
        	this.grpControlLine.TabStop = false;
        	this.grpControlLine.Text = "Control Line";
        	// 
        	// cbInvert
        	// 
        	this.cbInvert.AutoSize = true;
        	this.cbInvert.Location = new System.Drawing.Point(97, 32);
        	this.cbInvert.Name = "cbInvert";
        	this.cbInvert.Size = new System.Drawing.Size(53, 17);
        	this.cbInvert.TabIndex = 11;
        	this.cbInvert.Text = "Invert";
        	this.cbInvert.UseVisualStyleBackColor = true;
        	// 
        	// btnProperties
        	// 
        	this.btnProperties.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProperties.BackgroundImage")));
        	this.btnProperties.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        	this.btnProperties.Enabled = false;
        	this.btnProperties.Location = new System.Drawing.Point(234, 7);
        	this.btnProperties.Name = "btnProperties";
        	this.btnProperties.Size = new System.Drawing.Size(27, 23);
        	this.btnProperties.TabIndex = 12;
        	this.btnProperties.UseVisualStyleBackColor = true;
        	this.btnProperties.Click += new System.EventHandler(this.BtnPropertiesClick);
        	// 
        	// SetupDialogForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(350, 175);
        	this.Controls.Add(this.btnProperties);
        	this.Controls.Add(this.grpControlLine);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.cboComPorts);
        	this.Controls.Add(this.txtSelectedWebcam);
        	this.Controls.Add(this.btnSelectWebcam);
        	this.Controls.Add(this.picASCOM);
        	this.Controls.Add(this.cmdCancel);
        	this.Controls.Add(this.cmdOK);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "SetupDialogForm";
        	this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "LxWebcam Setup";
        	((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
        	this.grpControlLine.ResumeLayout(false);
        	this.grpControlLine.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.Button btnSelectWebcam;

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.TextBox txtSelectedWebcam;
        private System.Windows.Forms.ComboBox cboComPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbRTS;
        private System.Windows.Forms.RadioButton rbDTR;
        private System.Windows.Forms.GroupBox grpControlLine;
        private System.Windows.Forms.CheckBox cbInvert;
        
        
    }
}