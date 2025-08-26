using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Chats.Requests;
using ETicaret.Application.DTOs.Chats.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class ChatService(ETicaretDbContext context) : IChatService
{
    public async Task<IEnumerable<GetChatResultDto>> GetChatsAsync(Guid userId, Guid toUserId)
    {
        List<Chat> chats = await context.Chats
            .Where(x => x.UserId == userId && x.ToUserId == toUserId 
                        || x.ToUserId== userId && x.UserId == toUserId)
            .OrderBy(x=>x.CreatedDate)
            .ToListAsync();
        return chats.Select(x => new GetChatResultDto
        {
            Id = x.Id,
            Message = x.Message,
            UserId = x.UserId,
            ToUserId = x.ToUserId,
            CreatedDate = x.CreatedDate
        });
    }

    public async Task SendMessageAsync(SendMessageDto dto)
    {
        Chat chat = new Chat()
        {
            UserId = dto.UserId,
            Message = dto.Message,
            ToUserId = dto.ToUserId,
            CreatedDate = DateTime.UtcNow
        };
        await context.Chats.AddAsync(chat);
        await context.SaveChangesAsync();
    }
}