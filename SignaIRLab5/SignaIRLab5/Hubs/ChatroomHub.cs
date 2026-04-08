using Microsoft.AspNetCore.SignalR;
namespace SignaIRLab5.Hubs
{
    public class ChatroomHub : Hub
    {
        public async Task BroadcastMessage(string username, string message)
        { await Clients.All.SendAsync("ReceiveMessage", username, message); }
    }
}
