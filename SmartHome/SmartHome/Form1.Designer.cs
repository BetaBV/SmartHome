namespace SmartHome
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
            this.tbComPort = new System.Windows.Forms.TextBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btStartHouse = new System.Windows.Forms.Button();
            this.btStartPipe = new System.Windows.Forms.Button();
            this.btShowConsole = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbComPort
            // 
            this.tbComPort.Location = new System.Drawing.Point(12, 12);
            this.tbComPort.Name = "tbComPort";
            this.tbComPort.Size = new System.Drawing.Size(52, 20);
            this.tbComPort.TabIndex = 0;
            this.tbComPort.Text = "COM17";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(70, 10);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 1;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btStartHouse
            // 
            this.btStartHouse.Location = new System.Drawing.Point(12, 38);
            this.btStartHouse.Name = "btStartHouse";
            this.btStartHouse.Size = new System.Drawing.Size(133, 23);
            this.btStartHouse.TabIndex = 2;
            this.btStartHouse.Text = "Start House";
            this.btStartHouse.UseVisualStyleBackColor = true;
            this.btStartHouse.Click += new System.EventHandler(this.btStartHouse_Click);
            // 
            // btStartPipe
            // 
            this.btStartPipe.Location = new System.Drawing.Point(13, 68);
            this.btStartPipe.Name = "btStartPipe";
            this.btStartPipe.Size = new System.Drawing.Size(132, 23);
            this.btStartPipe.TabIndex = 3;
            this.btStartPipe.Text = "StartPipe";
            this.btStartPipe.UseVisualStyleBackColor = true;
            this.btStartPipe.Click += new System.EventHandler(this.btStartPipe_Click);
            // 
            // btShowConsole
            // 
            this.btShowConsole.Location = new System.Drawing.Point(13, 97);
            this.btShowConsole.Name = "btShowConsole";
            this.btShowConsole.Size = new System.Drawing.Size(132, 23);
            this.btShowConsole.TabIndex = 4;
            this.btShowConsole.Text = "Show Console";
            this.btShowConsole.UseVisualStyleBackColor = true;
            this.btShowConsole.Click += new System.EventHandler(this.btShowConsole_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 128);
            this.Controls.Add(this.btShowConsole);
            this.Controls.Add(this.btStartPipe);
            this.Controls.Add(this.btStartHouse);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.tbComPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "SmartHome";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbComPort;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btStartHouse;
        private System.Windows.Forms.Button btStartPipe;
        private System.Windows.Forms.Button btShowConsole;
    }
}

