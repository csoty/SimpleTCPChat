using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SecondChatClient
{
    enum Command
    {
        Login,
        Logout,
        Message,
        List,
        Null
    }
    public partial class CClient : Form
    {


        public Socket clientSocket;
        public string user;

        private byte[] byteData = new byte[1024];

        public CClient()
        {
            InitializeComponent();
        }

        private void bt_connect_Click(object sender, EventArgs e)
        {
            string serverip = Interaction.InputBox("Type in the IP where you want to join.", "Connect to a server", "csoty.ddns.net");
            if (serverip.Length > 0)
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ipAddress = Dns.GetHostEntry(serverip).AddressList[0];
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 3333);
                    clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
                tBox_msg.Enabled = true;
                bt_send.Enabled = true;
                Button bt_dc = new Button();
                bt_dc.Text = "Disconnect";
                bt_dc.Top = bt_connect.Top + bt_connect.Height + 5;
                bt_dc.Left = bt_connect.Left;
                bt_dc.Click += new EventHandler(bt_dc_Click);
                this.Controls.Add(bt_dc);
                bt_dc.Show();

                try
                {

                    Data sendMsg = new Data();
                    sendMsg.cmd = Command.List;
                    sendMsg.user = user;
                    sendMsg.Msg = null;

                    byteData = sendMsg.toByte();

                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnRecieve), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void OnConnectSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                user = tB_nick.Text;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket = (Socket)ar.AsyncState;
                if (clientSocket != null)
                {
                    clientSocket.EndConnect(ar);
                }

                Data sendMsg = new Data();
                sendMsg.cmd = Command.Login;
                sendMsg.user = tB_nick.Text;
                sendMsg.Msg = null;

                byte[] b = sendMsg.toByte();

                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnConnectSend), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_dc_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Dispose();
            bt_send.Enabled = false;
            tBox_msg.Enabled = false;
            tB_nick.Enabled = true;
            lb_ip.Text = "0.0.0.0";
            lb_ip.ForeColor = Color.Red;
            lb_constatus.Text = "Disconnected";
            lb_constatus.ForeColor = Color.Red;
        }
        private void bt_send_Click(object sender, EventArgs e)
        {
            try
            {
                Data sendMsg = new Data();

                sendMsg.user = user;
                sendMsg.Msg = tBox_msg.Text;
                sendMsg.cmd = Command.Message;

                byte[] byteData = sendMsg.toByte();

                clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                tB_Enter(tBox_msg, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not send message to server \n Exception: " + ex.ToString());
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnRecieve(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                Data recMsg = new Data(byteData);
                switch (recMsg.cmd)
                {
                    case Command.Login:
                        lBox_users.Items.Add(recMsg.user);
                        break;
                    case Command.Logout:
                        lBox_users.Items.Remove(recMsg.user);
                        break;
                    case Command.Message:
                        break;
                    case Command.List:
                        lBox_users.Items.AddRange(recMsg.Msg.Split('*'));
                        lBox_users.Items.RemoveAt(lBox_users.Items.Count - 1);
                        lBox_msgs.Items.Add(">> " + user + " has joined the server \\o/");
                        break;
                }

                if (recMsg.Msg != null && recMsg.cmd != Command.List)
                    lBox_msgs.Items.Add(">>" + recMsg.Msg);

                byteData = new byte[1024];

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnRecieve), null);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tB_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).Text = "";
        }

        class Data
        {

            public string user;
            public string Msg;
            public Command cmd;

            public Data()
            {
                this.cmd = Command.Null;
                this.user = null;
                this.Msg = null;
            }

            public Data(byte[] data)
            {
                this.cmd = (Command)BitConverter.ToInt32(data, 0);
                int nameLen = BitConverter.ToInt32(data, 4);
                int msgLen = BitConverter.ToInt32(data, 4);
                if (nameLen > 0)
                    this.user = Encoding.UTF8.GetString(data, 12, nameLen);
                else
                    this.user = null;

                if (msgLen > 0)
                    this.Msg = Encoding.UTF8.GetString(data, 12 + nameLen, msgLen);
                else
                    this.Msg = null;
            }

            public byte[] toByte()
            {
                List<byte> result = new List<byte>();

                result.AddRange(BitConverter.GetBytes((int)cmd));

                if (user != null)
                    result.AddRange(BitConverter.GetBytes(user.Length));
                else
                    result.AddRange(BitConverter.GetBytes(0));

                if (Msg != null)
                    result.AddRange(BitConverter.GetBytes(Msg.Length));
                else
                    result.AddRange(BitConverter.GetBytes(0));

                if (user != null)
                    result.AddRange(Encoding.UTF8.GetBytes(user));

                return result.ToArray();
            }
        }

        private void CClient_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}
