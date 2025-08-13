using ETicaret.Application.DTOs.Images.Requests;
using ETicaret.Application.DTOs.Images.Results;

namespace ETicaret.Application.Abstractions;

public interface IImageService
{
    Task<AddImageResultDto> AddImageRangeAsync(AddImageDto dto);
}