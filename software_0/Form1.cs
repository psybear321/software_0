using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace software_0
{
    public partial class Form1 : Form
    {
        private HubConnection connection;

        public Form1()
        {
            InitializeComponent();
            txtUserInput.KeyDown += txtUserInput_KeyDown;
            ConnectToServer();  // Connect to SignalR server when the form loads
        }

        private void DisplayMessage(string username, string message)
        {
            // Check if we need to invoke on the UI thread
            if (rtbChatHistory.InvokeRequired)
            {
                rtbChatHistory.Invoke(new Action(() =>
                {
                    rtbChatHistory.AppendText($"{username}: {message}\n");
                }));
            }
            else
            {
                rtbChatHistory.AppendText($"{username}: {message}\n");
            }
        }


        private async void ConnectToServer()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub") // Server URL
                .Build();

            connection.On<string, string>("ReceiveMessage", (username, message) =>
            {
                // Avoid displaying the same message twice
                if (username != "You")
                {
                    DisplayMessage(username, message);
                }
            });

            try
            {
                await connection.StartAsync();
                DisplayMessage("System", "Connected to chat server!");

                // Load previous messages if needed
                await LoadMessages();
            }
            catch (Exception ex)
            {
                DisplayMessage("System", "Could not connect to server: " + ex.Message);
            }
        }

        private async Task LoadMessages()
        {
            var messages = await connection.InvokeAsync<List<Tuple<string, string>>>("GetAllMessages");

            foreach (var messageTuple in messages)
            {
                string sender = messageTuple.Item1;
                string message = messageTuple.Item2;

                DisplayMessage(sender, message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = txtUserInput.Text.Trim();

            if (!string.IsNullOrEmpty(userMessage) && connection.State == HubConnectionState.Connected)
            {
                try
                {
                    string username = "You"; // Your local username
                    await connection.InvokeAsync("SendMessage", username, userMessage);

                    // Show the message locally only
                    DisplayMessage(username, userMessage);

                    txtUserInput.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending message: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Not connected to the server.");
            }
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChatboxBorder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUserInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChatBox_Label_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Event_Click(object sender, EventArgs e)
        {

        }

        private void Memebrship_icon_Click(object sender, EventArgs e)
        {

        }

        private void Event_icon_Click(object sender, EventArgs e)
        {

        }

        private void Chat_icon_Click(object sender, EventArgs e)
        {

        }

        private void Home_icon_Click(object sender, EventArgs e)
        {

        }

        private void Profile_icon_Click(object sender, EventArgs e)
        {

        }

        private void Membership_Click(object sender, EventArgs e)
        {

        }

        private void rtbChatHistory_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChatBox_Button_Click(object sender, EventArgs e)
        {

        }

        private void Home_Click(object sender, EventArgs e)
        {

        }

        private void Profile_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}