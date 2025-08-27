using ETicaret.Application.DTOs.Chats.Requests;
using ETicaret.Application.DTOs.Chats.Results;

namespace ETicaret.Application.Abstractions;

public interface IChatService
{
    Task<IEnumerable<GetChatResultDto>> GetChatsAsync(Guid toUserId);
    Task SendMessageAsync(SendMessageDto dto);
}