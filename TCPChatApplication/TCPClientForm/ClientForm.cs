using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCPClientForm
{
    public partial class ClientForm : Form
    {
        // Đối tượng TCPClient để kết nối đến máy chủ
        private TcpClient? client;
        // Gửi và nhận dữ liệu qua kết nối
        private NetworkStream? stream;
        // Xử lý việc nhận nhin nahwns từ máy chủ
        private Thread? thread;
        // Kiểm tra server và client có đang connect không
        private bool isConnected = false;
        // Form đóng bởi người dùng
        private bool isClosingByButton = false;
        // Lưu biệt danh
        private string? nickname;

        public ClientForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(ClientForm_FormClosing);
        }
        //  Kết nối đến máy chủ với địa chỉ IP và cổng được chỉ định. Sau đó, nó gửi biệt danh của người dùng đến máy chủ và bắt đầu một luồng mới để nhận tin nhắn từ máy chủ.
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
        // Gửi tin nhắn từ người dùng đến máy chủ thông qua luồng kết nối stream.
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

        // Nhận tin nhắn từ máy chủ. Nó luôn lắng nghe cho đến khi máy khách được ngắt kết nối hoặc có lỗi xảy ra. Mỗi khi nhận được một tin nhắn từ máy chủ, nó cập nhật hộp văn bản chat trên giao diện người dùng.
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
                if (message.Contains("[Server]"))
                {
                    UpdateChatBox(message);
                }
            }
        }


        // Cập nhật nội dung hộp văn bản chat trên giao diện người dùng.
        private void UpdateChatBox(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateChatBox), message);
                return;
            }

            string formattedMessage = $"[{DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")}] {message}";
            ChatTextBox.AppendText(formattedMessage + Environment.NewLine);
        }


        // Cập nhật trạng thái kết nối trên giao diện người dùng.
        private void UpdateConnectionStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateConnectionStatus));
                return;
            }
            ConnectionStatusLabel.Text = isConnected ? "Connected" : "Disconnected";
        }

        // Xử lý khi người dùng đóng form.
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
