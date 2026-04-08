using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SignalRClient
{
    public partial class Page1 : Page
    {
        private HubConnection _hubConnection;
        private bool _isConnected;

        public Page1()
        {
            InitializeComponent();
            InitializeHubComponent();
            // Use the _hubConnection instance, not the HubConnection type

            _hubConnection.On<string, string>(
                         "ReceiveMessage",
                         (username, message) => GetMessage(username, message));

        }

        //ok it works now didn't need a main page page 1 is fine ig? but it connects to the localhost site I did
        //changed it from local host 5012 to my IP address
        private void InitializeHubComponent()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://192.168.1.137/chatroomhub")
                .Build();

            _isConnected = false;
            SendButton.IsEnabled = false;
        }

        // so this is recieving the message from the server and then calling it to the list box in page1 i did 
        private void GetMessage(string username, string message)
        {
            this.Dispatcher.Invoke(
                () =>
                {
                    var chat = $"{username}: {message}";
                    MessagesListBox.Items.Add(chat);
                });
        }
        //forgot to add async in method 
        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Example: show username in the TextBlock
            var userName = MessageTextBox_UserName.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Please enter a user name first.");
                return;
            }

            //currently doing it rn yee it works

            try
            {
                await _hubConnection.StartAsync();
                _isConnected = true;
                SendButton.IsEnabled = true;
                UserNameTextBlock.Text = "Connected as: " + userName;
                MessagesListBox.Items.Add("Connection Opened");
            }
            catch (Exception ex)
            {
                _isConnected = false;
                SendButton.IsEnabled = false;
                MessagesListBox.Items.Add("Connection Failed: " + ex.Message);
            }
        }
        //need to have it be void but can't make it task why??
        //also don't think this works? it does save a file though but can't read it 
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isConnected)
            {
                MessagesListBox.Items.Add("Not connected. Click Connect first.");
                return;
            }

            // Add to the ListBox to see it in the UI
            var message = MessageTextBox.Text;
            var userName = MessageTextBox_UserName.Text;

            //broadcasts the message to the server and needed to use _hubconnection
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            try
            {
                await _hubConnection.InvokeAsync("BroadcastMessage", userName, message);
                MessageTextBox.Clear();
                MessageTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessagesListBox.Items.Add(ex.Message);
            }
        }


    }
}