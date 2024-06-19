namespace DotnetService.APIs.Dtos;

public class CommentCreateInput
{
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public TaskIdDto? Task { get; set; }

    public DateTime UpdatedAt { get; set; }
}
