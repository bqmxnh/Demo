using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCPClientForm
{
    public partial class ClientForm : Form
    {
        private TcpClient? client;
        private NetworkStream? stream;
        private Thread? thread;
        private bool isConnected = false;
        private bool isClosingByButton = false;
        private string? nickname;

        public ClientForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(ClientForm_FormClosing);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            try
            {
                client.Connect("127.0.0.1", 8888);
                stream = client.GetStream();
                nickname = NicknameTextBox.Text;
                byte[] nicknameData = Encoding.UTF8.GetBytes(nickname);
                stream.Write(nicknameData, 0, nicknameData.Length);
                thread = new Thread(ReceiveMessages);
                thread.Start();
                isConnected = true;
                UpdateConnectionStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to server: " + ex.Message);
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (isConnected && stream != null) // Kiểm tra stream có null hay không
            {
                string message = MessageTextBox.Text;
                byte[] messageData = Encoding.UTF8.GetBytes(message);
                stream.Write(messageData, 0, messageData.Length);
                stream.Flush();
                UpdateChatBox("You: " + message);
                MessageTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Not connected to server.");
            }
        }


        private void ReceiveMessages()
        {
            while (isConnected && stream != null) // Kiểm tra stream có null hay không
            {
                byte[] data = new byte[4096];
                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, 4096);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error receiving data: " + ex.Message);
                    break;
                }

                if (bytesRead == 0)
                {
                    Console.WriteLine("Disconnected from server.");
                    break;
                }

                string message = Encoding.UTF8.GetString(data, 0, bytesRead);

                // Chỉ hiển thị tin nhắn từ máy chủ
                if (message.Contains("[Server]"))
                {
                    UpdateChatBox(message);
                }
            }
        }



        private void UpdateChatBox(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateChatBox), message);
                return;
            }
            ChatTextBox.AppendText(message + Environment.NewLine);
        }

        private void UpdateConnectionStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateConnectionStatus));
                return;
            }
            ConnectionStatusLabel.Text = isConnected ? "Connected" : "Disconnected";
        }

        private void ClientForm_FormClosing(object? sender, FormClosingEventArgs e)
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
