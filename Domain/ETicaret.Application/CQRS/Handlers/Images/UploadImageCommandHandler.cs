using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Commands.Images;
using ETicaret.Application.CQRS.Results.Images;
using ETicaret.Application.DTOs.Images.Requests;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Images;

public class UploadImageCommandHandler(IImageService imageService) :  IRequestHandler<UploadImageCommandRequest, UploadImageCommandResult>
{
    public async Task<UploadImageCommandResult> Handle(UploadImageCommandRequest request, CancellationToken cancellationToken)
    {
        AddImageDto  dto = new ()
        {
            ProductId = request.ProductId,
            Files = request.Files,
        };
        var result = await imageService.AddImageRangeAsync(dto);
        return new UploadImageCommandResult()
        {
            Message = result.Message,
            ProductId = result.ProductId,
            Success = result.Success
        };
    }
}