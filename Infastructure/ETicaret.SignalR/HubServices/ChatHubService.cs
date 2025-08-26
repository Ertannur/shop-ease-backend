using ETicaret.Application.Abstractions.Hubs;
using ETicaret.Application.DTOs.Chats.Requests;
using ETicaret.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETicaret.SignalR.HubServices;

public class ChatHubService : IChatHubService
{
    private readonly IHubContext<CustomerHub> _hubContext;

    public ChatHubService(IHubContext<CustomerHub> hubContext)
    {
        _hubContext = hubContext;
    }


    public async Task SendMessageAsync(SendMessageDto dto)
    {
        var connectionId = CustomerHub.Users.First(p => p.Value == dto.UserId).Key;
        await _hubContext.Clients.Client(connectionId).SendAsync("Messages", dto);
        var receiverConnectionId = CustomerHub.Users.First(p => p.Value == dto.ToUserId).Key;
        await _hubContext.Clients.Client(receiverConnectionId).SendAsync("Messages", dto);
    }
}
