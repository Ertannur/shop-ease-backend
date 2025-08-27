using ETicaret.Application.Abstractions;
using ETicaret.Application.Abstractions.Hubs;
using ETicaret.Application.DTOs.Chats.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ChatController(IChatHubService chatHubService,IChatService chatService, IUserService userService) : ControllerBase
{
    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,User,Support")]
    public async Task<IActionResult> GetChats(Guid toUserId)
    {
        var values = await chatService.GetChatsAsync(toUserId);
        return Ok(values);
    }
    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Support")]
    public async Task<IActionResult> GetUsers()
    {
        var results = await userService.GetUsersAsync();
        return Ok(results);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetSupport()
    {
        var user = await userService.GetSupportAsync();
        return Ok(user);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin,User,Support")]
    public async Task<IActionResult> SendMessage(SendMessageDto sendMessageDto)
    {
        await chatService.SendMessageAsync(sendMessageDto);
        await chatHubService.SendMessageAsync(sendMessageDto);
        return Ok();
    }    
}

