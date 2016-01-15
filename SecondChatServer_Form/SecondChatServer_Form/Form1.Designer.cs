namespace SecondChatServer_Form
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
            this.lBox_log = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lBox_log
            // 
            this.lBox_log.BackColor = System.Drawing.SystemColors.WindowText;
            this.lBox_log.ForeColor = System.Drawing.Color.Lime;
            this.lBox_log.FormattingEnabled = true;
            this.lBox_log.Location = new System.Drawing.Point(12, 12);
            this.lBox_log.Name = "lBox_log";
            this.lBox_log.Size = new System.Drawing.Size(513, 303);
            this.lBox_log.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(537, 330);
            this.Controls.Add(this.lBox_log);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "CsoTy\'s chatostroi :3 #csotychatethajt";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lBox_log;

    }
}

