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
        // Ba�lant� kuruldu�unda loglama veya grup i�lemleri yap�labilir
        await base.OnConnectedAsync();
    }
}
