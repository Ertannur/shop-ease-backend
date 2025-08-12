using ETicaret.API.Validations;
using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Auths;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController(IMediator mediator, IEmailService service) : ControllerBase
    {
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            var result = await mediator.Send(request);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommandRequest request)
        {
            var result = await mediator.Send(request);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult Roles()
        {
            return Ok("Başarılı");
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return Ok();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword()
        {
            await service.SendEmailAsync("umutcanguncu@icloud.com", "Şifre Sıfırlama", "Adsfdmdfmd");
            return Ok();
        }
    }

}
