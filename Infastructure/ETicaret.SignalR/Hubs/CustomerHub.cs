using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.SignalR;

namespace ETicaret.SignalR.Hubs;

public class CustomerHub(ETicaretDbContext context) : Hub
{
    /*
    public async Task SendMessage(string receiverUserId, string message)
    {
        await Clients.User(receiverUserId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
    }

    public override async Task OnConnectedAsync()
    {
       
        await base.OnConnectedAsync();
    }
    */
    public static Dictionary<string, Guid> Users { get; set; } = new();

    public async Task Connect(Guid userId)
    {
        Users.Add(Context.ConnectionId, userId);
        AppUser user = await context.Users.FindAsync(userId);
        if (user is not null)
        {
            
        }
        
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Guid userId;
        Users.TryGetValue(Context.ConnectionId, out  userId);
        Users.Remove(Context.ConnectionId);
        AppUser user = await context.Users.FindAsync(userId);
        if (user is not null)
        {
            await Clients.All.SendAsync("UserDisconnected", user);
        }
        
    }
}
