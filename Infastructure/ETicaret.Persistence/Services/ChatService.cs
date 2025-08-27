using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Chats.Requests;
using ETicaret.Application.DTOs.Chats.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class ChatService(ETicaretDbContext context, IHttpContextAccessor httpContextAccessor) : IChatService
{
    private async Task<AppUser?> CurrentUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? appUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return appUser;
    }
    public async Task<IEnumerable<GetChatResultDto>> GetChatsAsync(Guid toUserId)
    {
        var user = await CurrentUser();
        if(user is null)
            return new List<GetChatResultDto>();
        List<Chat> chats = await context.Chats
            .Where(x => x.UserId == user.Id && x.ToUserId == toUserId 
                        || x.ToUserId== user.Id && x.UserId == toUserId)
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
        var user = await CurrentUser();
        Chat chat = new Chat()
        {
            UserId = user.Id,
            Message = dto.Message,
            ToUserId = dto.ToUserId,
            CreatedDate = DateTime.UtcNow
        };
        await context.Chats.AddAsync(chat);
        await context.SaveChangesAsync();
    }
}