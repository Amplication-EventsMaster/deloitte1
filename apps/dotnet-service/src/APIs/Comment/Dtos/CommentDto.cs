namespace DotnetService.APIs.Dtos;

public class CommentDto : CommentIdDto
{
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public TaskIdDto? Task { get; set; }

    public DateTime UpdatedAt { get; set; }
}
