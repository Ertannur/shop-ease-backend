using System.ComponentModel.DataAnnotations;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities;

public class Chat : BaseEntity
{
    public Guid UserId { get; set; }
     public Guid ToUserId { get; set; }
     public string Message { get; set; } =  string.Empty;
   
    
}