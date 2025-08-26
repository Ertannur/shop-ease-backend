using ETicaret.Application.DTOs.Chats.Requests;

namespace ETicaret.Application.Abstractions.Hubs;

public interface IChatHubService
{
    Task SendMessageAsync(SendMessageDto dto);
}