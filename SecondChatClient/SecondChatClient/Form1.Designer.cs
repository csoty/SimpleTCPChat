namespace SecondChatClient
{
    partial class CClient
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
            this.bt_connect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_ip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_constatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lBox_msgs = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tBox_msg = new System.Windows.Forms.TextBox();
            this.bt_send = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tB_nick = new System.Windows.Forms.TextBox();
            this.lBox_users = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_connect
            // 
            this.bt_connect.Location = new System.Drawing.Point(12, 12);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(164, 23);
            this.bt_connect.TabIndex = 0;
            this.bt_connect.Text = "Connect to server";
            this.bt_connect.UseVisualStyleBackColor = true;
            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_ip);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lb_constatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(210, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 125);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // lb_ip
            // 
            this.lb_ip.AutoSize = true;
            this.lb_ip.ForeColor = System.Drawing.Color.Red;
            this.lb_ip.Location = new System.Drawing.Point(88, 55);
            this.lb_ip.Name = "lb_ip";
            this.lb_ip.Size = new System.Drawing.Size(40, 13);
            this.lb_ip.TabIndex = 3;
            this.lb_ip.Text = "0.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP:";
            // 
            // lb_constatus
            // 
            this.lb_constatus.AutoSize = true;
            this.lb_constatus.ForeColor = System.Drawing.Color.Red;
            this.lb_constatus.Location = new System.Drawing.Point(88, 25);
            this.lb_constatus.Name = "lb_constatus";
            this.lb_constatus.Size = new System.Drawing.Size(73, 13);
            this.lb_constatus.TabIndex = 1;
            this.lb_constatus.Text = "Disconnected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection:";
            // 
            // lBox_msgs
            // 
            this.lBox_msgs.FormattingEnabled = true;
            this.lBox_msgs.Location = new System.Drawing.Point(12, 154);
            this.lBox_msgs.Name = "lBox_msgs";
            this.lBox_msgs.Size = new System.Drawing.Size(775, 199);
            this.lBox_msgs.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 377);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Message:";
            // 
            // tBox_msg
            // 
            this.tBox_msg.Enabled = false;
            this.tBox_msg.Location = new System.Drawing.Point(68, 374);
            this.tBox_msg.MaxLength = 950;
            this.tBox_msg.Name = "tBox_msg";
            this.tBox_msg.Size = new System.Drawing.Size(719, 20);
            this.tBox_msg.TabIndex = 4;
            this.tBox_msg.Text = "Hail Csoty";
            this.tBox_msg.Enter += new System.EventHandler(this.tB_Enter);
            this.tBox_msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBox_msg_KeyDown);
            // 
            // bt_send
            // 
            this.bt_send.Enabled = false;
            this.bt_send.Location = new System.Drawing.Point(12, 400);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(775, 23);
            this.bt_send.TabIndex = 5;
            this.bt_send.Text = "Send";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nickname:";
            // 
            // tB_nick
            // 
            this.tB_nick.Location = new System.Drawing.Point(76, 64);
            this.tB_nick.Name = "tB_nick";
            this.tB_nick.Size = new System.Drawing.Size(100, 20);
            this.tB_nick.TabIndex = 7;
            this.tB_nick.Text = "DefaultUser";
            this.tB_nick.Enter += new System.EventHandler(this.tB_Enter);
            // 
            // lBox_users
            // 
            this.lBox_users.FormattingEnabled = true;
            this.lBox_users.Location = new System.Drawing.Point(658, 16);
            this.lBox_users.Name = "lBox_users";
            this.lBox_users.Size = new System.Drawing.Size(120, 121);
            this.lBox_users.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(562, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Connected users:";
            // 
            // CClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 429);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lBox_users);
            this.Controls.Add(this.tB_nick);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bt_send);
            this.Controls.Add(this.tBox_msg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lBox_msgs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_connect);
            this.Name = "CClient";
            this.Text = "CsoTy\'s Chatostroi #csotychatethajt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CClient_FormClosing);
            this.Load += new System.EventHandler(this.CClient_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_connect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_constatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lBox_msgs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBox_msg;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tB_nick;
        private System.Windows.Forms.ListBox lBox_users;
        private System.Windows.Forms.Label label5;
    }
}

