using ETicaret.Application.Abstractions;
using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.DTOs.Images.Requests;
using ETicaret.Application.DTOs.Images.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;

namespace ETicaret.Persistence.Services;

public class ImageService(ETicaretDbContext context, IStorageService storageService) : IImageService
{
    public async Task<AddImageResultDto> AddImageRangeAsync(AddImageDto dto)
    {
        var images = await storageService.UploadAsync("photo-images", dto.Files);
        foreach (var imageDto in images)
        {
            Image image = new()
            {
                Id = Guid.NewGuid(),
                FileName = imageDto.filename,
                ImageUrl = imageDto.pathOrContainerName,
                ProductId = dto.ProductId,
            };
            await context.Images.AddAsync(image);
            
        } 
        var result =  await context.SaveChangesAsync();
        if (result > 0)
            return new()
            {
                Success = true,
                Message = $"Successfully added {images.Count()} images.",
                ProductId = dto.ProductId
            };
        return new()
        {
            Success = false,
            Message = $"Failed to add {images.Count()} images.",
            ProductId = dto.ProductId
        };
    }
}