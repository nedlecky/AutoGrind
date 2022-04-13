namespace AutoGrind
{
    partial class SplashForm
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
            this.components = new System.ComponentModel.Container();
            this.CloseTmr = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.DeveloperLogoPic = new System.Windows.Forms.PictureBox();
            this.DistributorLogoPic = new System.Windows.Forms.PictureBox();
            this.CustomerLogoPic = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.VersionLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DeveloperLogoPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistributorLogoPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerLogoPic)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseTmr
            // 
            this.CloseTmr.Tick += new System.EventHandler(this.CloseTmr_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(185, 324);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "By";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Agency FB", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(251, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 77);
            this.label2.TabIndex = 1;
            this.label2.Text = "AutoGrind";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.Green;
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.Location = new System.Drawing.Point(12, 170);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(127, 108);
            this.CloseBtn.TabIndex = 81;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // DeveloperLogoPic
            // 
            this.DeveloperLogoPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DeveloperLogoPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DeveloperLogoPic.Image = global::AutoGrind.Properties.Resources.Lecky_Engineering_Logo_12_4_2020_Full;
            this.DeveloperLogoPic.Location = new System.Drawing.Point(499, 504);
            this.DeveloperLogoPic.Name = "DeveloperLogoPic";
            this.DeveloperLogoPic.Size = new System.Drawing.Size(163, 84);
            this.DeveloperLogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DeveloperLogoPic.TabIndex = 85;
            this.DeveloperLogoPic.TabStop = false;
            // 
            // DistributorLogoPic
            // 
            this.DistributorLogoPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DistributorLogoPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DistributorLogoPic.Image = global::AutoGrind.Properties.Resources.O_C_Logo;
            this.DistributorLogoPic.Location = new System.Drawing.Point(45, 396);
            this.DistributorLogoPic.Name = "DistributorLogoPic";
            this.DistributorLogoPic.Size = new System.Drawing.Size(364, 194);
            this.DistributorLogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DistributorLogoPic.TabIndex = 84;
            this.DistributorLogoPic.TabStop = false;
            // 
            // CustomerLogoPic
            // 
            this.CustomerLogoPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CustomerLogoPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CustomerLogoPic.Image = global::AutoGrind.Properties.Resources.TQ_Logo;
            this.CustomerLogoPic.Location = new System.Drawing.Point(215, 170);
            this.CustomerLogoPic.Name = "CustomerLogoPic";
            this.CustomerLogoPic.Size = new System.Drawing.Size(300, 108);
            this.CustomerLogoPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CustomerLogoPic.TabIndex = 83;
            this.CustomerLogoPic.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Agency FB", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(481, 397);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 102);
            this.label3.TabIndex = 86;
            this.label3.Text = "Engineering\r\nRobot Programming\r\nSoftware Development";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionLbl
            // 
            this.VersionLbl.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLbl.ForeColor = System.Drawing.Color.Black;
            this.VersionLbl.Location = new System.Drawing.Point(11, 86);
            this.VersionLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VersionLbl.Name = "VersionLbl";
            this.VersionLbl.Size = new System.Drawing.Size(709, 81);
            this.VersionLbl.TabIndex = 87;
            this.VersionLbl.Text = "VersionLbl\r\nLine 2";
            this.VersionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(731, 610);
            this.ControlBox = false;
            this.Controls.Add(this.VersionLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DeveloperLogoPic);
            this.Controls.Add(this.DistributorLogoPic);
            this.Controls.Add(this.CustomerLogoPic);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AutoGrind Information";
            this.Load += new System.EventHandler(this.SplashForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeveloperLogoPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistributorLogoPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerLogoPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer CloseTmr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.PictureBox CustomerLogoPic;
        private System.Windows.Forms.PictureBox DistributorLogoPic;
        private System.Windows.Forms.PictureBox DeveloperLogoPic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label VersionLbl;
    }
}