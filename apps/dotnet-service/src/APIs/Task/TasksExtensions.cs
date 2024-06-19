using DotnetService.APIs.Dtos;
using DotnetService.Infrastructure.Models;

namespace DotnetService.APIs.Extensions;

public static class TasksExtensions
{
    public static TaskDto ToDto(this Task model)
    {
        return new TaskDto
        {
            Comments = model.Comments?.Select(x => new CommentIdDto { Id = x.Id }).ToList(),
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Status = model.Status,
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Task ToModel(this TaskUpdateInput updateDto, TaskIdDto idDto)
    {
        var task = new Task
        {
            Id = idDto.Id,
            Description = updateDto.Description,
            Status = updateDto.Status,
            Title = updateDto.Title
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            task.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            task.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return task;
    }
}
