using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCPChatServer
{
    public partial class ServerForm : Form
    {
        private TcpListener? listener;
        private List<TcpClient> clients = new List<TcpClient>();
        private Dictionary<TcpClient, string> clientNicknames = new Dictionary<TcpClient, string>();
        private bool isClosingByButton = false;

        public ServerForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(ServerForm_FormClosing);
        }

        private void StartServerButton_Click_1(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            listener = new TcpListener(ip, port);
            listener.Start();

            string serverStartedMessage = "Server started.";
            UpdateStatus($"{serverStartedMessage}{Environment.NewLine}");

            MessageBox.Show(serverStartedMessage);

            Thread acceptThread = new Thread(new ThreadStart(ListenForClients));
            acceptThread.Start();
        }

        private void ListenForClients()
        {
            while (true && listener != null)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private bool isMessageFromServer(byte[] message)
        {
            // Kiểm tra xem tin nhắn có chứa dấu ngoặc vuông mở hay không (đánh dấu tin nhắn từ máy chủ)
            string messageStr = Encoding.UTF8.GetString(message);
            return messageStr.Contains("[Server]");
        }

        private void HandleClientComm(object? client)
        {
            if (client == null) return; // Kiểm tra tránh trường hợp client null
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] nicknameBuffer = new byte[4096];
            int nicknameBytesRead = clientStream.Read(nicknameBuffer, 0, 4096);
            string nickname = Encoding.UTF8.GetString(nicknameBuffer, 0, nicknameBytesRead);
            clientNicknames.Add(tcpClient, nickname);
            UpdateStatus($"Client connected with nickname: {nickname}{Environment.NewLine}");

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                string receivedMessage = $"{DateTime.Now.ToString("[HH:mm:ss]")} [{clientNicknames[tcpClient]}]: {Encoding.UTF8.GetString(message, 0, bytesRead)}";
                UpdateStatus($"Received from client: {receivedMessage}");

                if (!isMessageFromServer(message)) // Chỉ gửi tin nhắn từ máy khách đến tất cả máy khách trừ máy gửi
                {
                    foreach (TcpClient c in clients)
                    {
                        if (c != tcpClient)
                        {
                            NetworkStream stream = c.GetStream();
                            byte[] broadcastMessage = Encoding.UTF8.GetBytes(receivedMessage + Environment.NewLine);
                            stream.Write(broadcastMessage, 0, broadcastMessage.Length);
                            stream.Flush();
                        }
                    }
                }
            }

            clients.Remove(tcpClient);
            clientNicknames.Remove(tcpClient);
            tcpClient.Close();
        }


        // Gửi tin nhắn từ server đến tất cả client
        private void SendServerMessage(string message)
        {
            foreach (TcpClient c in clients)
            {
                NetworkStream stream = c.GetStream();
                byte[] serverMessage = Encoding.UTF8.GetBytes($"[Server] {message}{Environment.NewLine}");
                stream.Write(serverMessage, 0, serverMessage.Length);
                stream.Flush();
            }
        }

        // Sự kiện click cho nút gửi tin nhắn từ server đến client
        private void SendToClientsButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ServerMessageTextBox.Text))
            {
                string message = ServerMessageTextBox.Text;
                SendServerMessage(message);
                UpdateStatus($"Server sent message to all clients: {message}");
                ServerMessageTextBox.Clear();
            }
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }
            // Kiểm tra nếu chuỗi kết thúc bằng ký tự xuống dòng thì không thêm vào cuối
            if (message.EndsWith(Environment.NewLine))
                StatusTextBox.AppendText(message);
            else
                StatusTextBox.AppendText(message + Environment.NewLine);
        }


        private void ServerForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            isClosingByButton = e.CloseReason == CloseReason.UserClosing;
            if (isClosingByButton)
            {
                Application.Exit();
                Environment.Exit(0);
            }
        }
    }
}