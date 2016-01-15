using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace SecondChatServer_Form
{
    enum Command
    {
        Login,
        Logout,
        Message,
        List,
        Null
    }

    public partial class Form1 : Form
    {

        struct ClientInfo
        {
            public Socket socket;
            public string user;
        }

        ArrayList clientList;

        Socket serverSocket;

        byte[] byteData = new byte[1024];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 3333);

                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(4);

                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);

                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnRecieve), clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void OnRecieve(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);
                MessageBox.Show(clientSocket.ToString() + "\n" + clientSocket.SocketType.ToString() + "\n" + ar.AsyncState.ToString() + "\n" + ar.ToString());

                Data recMsg = new Data(byteData);

                Data sendMsg = new Data();

                byte[] msg;

                sendMsg.cmd = recMsg.cmd;
                sendMsg.user = recMsg.user;

                switch (recMsg.cmd)
                {
                    case Command.Login:
                        ClientInfo clientInfo = new ClientInfo();
                        clientInfo.socket = clientSocket;
                        clientInfo.user = recMsg.user;

                        clientList.Add(clientInfo);

                        sendMsg.Msg = ">> " + recMsg.user + " has joined the server \\o/";
                        break;

                    case Command.Logout:

                        int id = 0;
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.socket == clientSocket)
                            {
                                clientList.RemoveAt(id);
                                break;
                            }
                            ++id;
                        }
                        clientSocket.Close();
                        sendMsg.Msg = ">> " + recMsg.user + " just left the server :c ";
                        break;

                    case Command.Message:
                        sendMsg.Msg = recMsg.user + ": " + recMsg.Msg;
                        break;

                    case Command.List:
                        sendMsg.cmd = Command.List;
                        sendMsg.user = null;
                        sendMsg.Msg = null;

                        foreach (ClientInfo client in clientList)
                        {
                            sendMsg.Msg += client.user + "*";
                        }

                        msg = sendMsg.toByte();

                        clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), clientSocket);
                        break;
                }
                if (sendMsg.cmd != Command.List)
                {
                    msg = sendMsg.toByte();
                    foreach (ClientInfo c in clientList)
                    {
                        if (c.socket != clientSocket || sendMsg.cmd != Command.Login)
                        {
                            c.socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), c.socket);
                        }
                    }

                    tBox_log.Text += sendMsg.Msg + "\r\n";
                }

                if (recMsg.cmd != Command.Logout)
                {
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnRecieve), clientSocket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
    }
}
