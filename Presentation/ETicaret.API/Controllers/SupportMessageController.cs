using Microsoft.AspNetCore.Mvc;
using ETicaret.SignalR.HubServices;

namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/support")]
public class SupportMessageController : ControllerBase
{
    private readonly CustomerHubService _hubService;

    public SupportMessageController(CustomerHubService hubService)
    {
        _hubService = hubService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SupportMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest("UserId ve Message boş olamaz.");

        await _hubService.SendToUser(request.UserId, request.Message);
        return Ok(new { status = "Mesaj gönderildi" });
    }
}

// Geçici model – DTO yerine
public class SupportMessageRequest
{
    public string UserId { get; set; }
    public string Message { get; set; }
}
