using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.DTOs.Images.Requests;

public class AddImageDto
{
   public IFormFileCollection Files { get; set; }
   public Guid ProductId { get; set; }
}