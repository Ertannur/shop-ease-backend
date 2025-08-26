namespace ETicaret.Application.DTOs.Chats.Results;

public class GetChatResultDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ToUserId { get; set; }
    public string Message { get; set; } =  string.Empty;
    public DateTime CreatedDate { get; set; }
}