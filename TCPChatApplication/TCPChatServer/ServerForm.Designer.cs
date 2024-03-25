namespace TCPChatServer
{
    partial class ServerForm
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
            StartServerButton = new Button();
            StatusTextBox = new TextBox();
            ServerMessageTextBox = new TextBox();
            SendToClientsButton = new Button();
            SuspendLayout();
            // 
            // StartServerButton
            // 
            StartServerButton.Location = new Point(618, 391);
            StartServerButton.Name = "StartServerButton";
            StartServerButton.Size = new Size(185, 34);
            StartServerButton.TabIndex = 0;
            StartServerButton.Text = "Start Server";
            StartServerButton.UseVisualStyleBackColor = true;
            StartServerButton.Click += StartServerButton_Click_1;
            // 
            // StatusTextBox
            // 
            StatusTextBox.Location = new Point(12, 28);
            StatusTextBox.Multiline = true;
            StatusTextBox.Name = "StatusTextBox";
            StatusTextBox.Size = new Size(791, 345);
            StatusTextBox.TabIndex = 1;
            // 
            // ServerMessageTextBox
            // 
            ServerMessageTextBox.Location = new Point(12, 394);
            ServerMessageTextBox.Name = "ServerMessageTextBox";
            ServerMessageTextBox.Size = new Size(460, 31);
            ServerMessageTextBox.TabIndex = 2;
            // 
            // SendToClientsButton
            // 
            SendToClientsButton.Location = new Point(478, 391);
            SendToClientsButton.Name = "SendToClientsButton";
            SendToClientsButton.Size = new Size(134, 34);
            SendToClientsButton.TabIndex = 3;
            SendToClientsButton.Text = "Send";
            SendToClientsButton.UseVisualStyleBackColor = true;
            SendToClientsButton.Click += SendToClientsButton_Click;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(819, 450);
            Controls.Add(SendToClientsButton);
            Controls.Add(ServerMessageTextBox);
            Controls.Add(StatusTextBox);
            Controls.Add(StartServerButton);
            Name = "ServerForm";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartServerButton;
        private TextBox StatusTextBox;
        private TextBox ServerMessageTextBox;
        private Button SendToClientsButton;
    }
}
