using Microsoft.AspNetCore.SignalR;

namespace ETicaret.SignalR.Hubs;

public class CustomerHub : Hub
{
    public async Task SendMessage(string receiverUserId, string message)
    {
        await Clients.User(receiverUserId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
    }

    public override async Task OnConnectedAsync()
    {
        // Baðlantý kurulduðunda loglama veya grup iþlemleri yapýlabilir
        await base.OnConnectedAsync();
    }
}
