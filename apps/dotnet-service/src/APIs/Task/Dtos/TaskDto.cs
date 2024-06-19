using DotnetService.Core.Enums;

namespace DotnetService.APIs.Dtos;

public class TaskDto : TaskIdDto
{
    public List<CommentIdDto>? Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public StatusEnum? Status { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
