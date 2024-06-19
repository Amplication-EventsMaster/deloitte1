using DotnetService.APIs.Dtos;
using DotnetService.Infrastructure.Models;

namespace DotnetService.APIs.Extensions;

public static class ProjectsExtensions
{
    public static ProjectDto ToDto(this Project model)
    {
        return new ProjectDto
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Name = model.Name,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Project ToModel(this ProjectUpdateInput updateDto, ProjectIdDto idDto)
    {
        var project = new Project
        {
            Id = idDto.Id,
            Description = updateDto.Description,
            Name = updateDto.Name,
            Status = updateDto.Status
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            project.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            project.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return project;
    }
}
