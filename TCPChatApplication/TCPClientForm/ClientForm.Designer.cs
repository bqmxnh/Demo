namespace TCPClientForm
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ConnectButton = new Button();
            SendButton = new Button();
            MessageTextBox = new TextBox();
            ChatTextBox = new TextBox();
            ConnectionStatusLabel = new Label();
            NicknameTextBox = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(575, 19);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(112, 34);
            ConnectButton.TabIndex = 0;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(712, 404);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(112, 34);
            SendButton.TabIndex = 1;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // MessageTextBox
            // 
            MessageTextBox.Location = new Point(12, 407);
            MessageTextBox.Name = "MessageTextBox";
            MessageTextBox.Size = new Size(694, 31);
            MessageTextBox.TabIndex = 2;
            // 
            // ChatTextBox
            // 
            ChatTextBox.Location = new Point(12, 57);
            ChatTextBox.Multiline = true;
            ChatTextBox.Name = "ChatTextBox";
            ChatTextBox.Size = new Size(812, 325);
            ChatTextBox.TabIndex = 3;
            // 
            // ConnectionStatusLabel
            // 
            ConnectionStatusLabel.AutoSize = true;
            ConnectionStatusLabel.Location = new Point(713, 22);
            ConnectionStatusLabel.Name = "ConnectionStatusLabel";
            ConnectionStatusLabel.Size = new Size(60, 25);
            ConnectionStatusLabel.TabIndex = 4;
            ConnectionStatusLabel.Text = "Status";
            // 
            // NicknameTextBox
            // 
            NicknameTextBox.Location = new Point(150, 19);
            NicknameTextBox.Name = "NicknameTextBox";
            NicknameTextBox.Size = new Size(419, 31);
            NicknameTextBox.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(132, 25);
            label1.TabIndex = 6;
            label1.Text = "Enter nickname";
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 450);
            Controls.Add(label1);
            Controls.Add(NicknameTextBox);
            Controls.Add(ConnectionStatusLabel);
            Controls.Add(ChatTextBox);
            Controls.Add(MessageTextBox);
            Controls.Add(SendButton);
            Controls.Add(ConnectButton);
            Name = "ClientForm";
            Text = "Client";
            FormClosing += ClientForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ConnectButton;
        private Button SendButton;
        private TextBox MessageTextBox;
        private TextBox ChatTextBox;
        private Label ConnectionStatusLabel;
        private TextBox NicknameTextBox;
        private Label label1;
    }
}
