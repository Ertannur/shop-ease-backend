using ETicaret.Application.Abstractions.Hubs;
using ETicaret.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETicaret.SignalR.HubServices;

public class CustomerHubService : ICustomerHubService
{
    private readonly IHubContext<CustomerHub> _hubContext;

    public CustomerHubService(IHubContext<CustomerHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToUser(string userId, string message)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", "SupportAgent", message);
    }
}
