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
                    clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), clientSocket);
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
                lb_constatus.Text = "Connected";
                lb_ip.Text = Dns.GetHostEntry(serverip).AddressList[0].ToString();
                lb_constatus.ForeColor = lb_ip.ForeColor = Color.Green;
            }
        }

        private void OnConnectSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                user = tB_nick.Text; 
                try
                {
                    byteData = new byte[1024];
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnRecieve), null); //should be null
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
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

                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnConnectSend), clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_dc_Click(object sender, EventArgs e)
        {
            try
            {
                Data sendMsg = new Data();
                sendMsg.cmd = Command.Logout;
                sendMsg.user = user;
                sendMsg.Msg = null;

                byte[] b = sendMsg.toByte();
                clientSocket.Send(b, 0, b.Length, SocketFlags.None);
                clientSocket.Close();
            }
            catch (ObjectDisposedException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Button bt = sender as Button;
            bt.Dispose();
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

                byte[] buf = sendMsg.toByte();

                clientSocket.BeginSend(buf, 0, buf.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                tB_Enter(tBox_msg, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not send message to server \n Exception: " + ex.ToString());
            }
            try
            {
                Data reqList = new Data();
                reqList.user = user;
                reqList.Msg = null;
                reqList.cmd = Command.List;

                byte[] req = reqList.toByte();
                clientSocket.BeginSend(req, 0, req.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                        lBox_users.Items.Clear();
                        lBox_users.Items.AddRange(recMsg.Msg.Split('*'));
                        lBox_users.Items.RemoveAt(lBox_users.Items.Count - 1);
                        break;
                }

                if (recMsg.Msg != null && recMsg.cmd != Command.List)
                    lBox_msgs.Items.Add(">>" + recMsg.Msg);

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
                int msgLen = Math.Min(BitConverter.ToInt32(data, 8), 1000 - nameLen);
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

                if (Msg != null)
                    result.AddRange(Encoding.UTF8.GetBytes(Msg));

                return result.ToArray();
            }
        }

        private void CClient_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void tBox_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_send_Click(sender, null);
        }

        private void CClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tBox_msg.Enabled == true)
            {
                try
                {
                    Data sendMsg = new Data();
                    sendMsg.cmd = Command.Logout;
                    sendMsg.user = user;
                    sendMsg.Msg = null;

                    byte[] b = sendMsg.toByte();
                    clientSocket.Send(b, 0, b.Length, SocketFlags.None);
                    clientSocket.Close();
                }
                catch (ObjectDisposedException)
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
