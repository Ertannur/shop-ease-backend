using ETicaret.Application.CQRS.Results.Images;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.CQRS.Commands.Images;

public class UploadImageCommandRequest : IRequest<UploadImageCommandResult>
{
    public Guid ProductId { get; set; }
    public IFormFileCollection  Files { get; set; }
}