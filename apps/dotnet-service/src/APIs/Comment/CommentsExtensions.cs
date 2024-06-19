using DotnetService.APIs.Dtos;
using DotnetService.Infrastructure.Models;

namespace DotnetService.APIs.Extensions;

public static class CommentsExtensions
{
    public static CommentDto ToDto(this Comment model)
    {
        return new CommentDto
        {
            Content = model.Content,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Task = new TaskIdDto { Id = model.TaskId },
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Comment ToModel(this CommentUpdateInput updateDto, CommentIdDto idDto)
    {
        var comment = new Comment { Id = idDto.Id, Content = updateDto.Content };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            comment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            comment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return comment;
    }
}
