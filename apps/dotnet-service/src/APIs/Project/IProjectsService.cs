using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;

namespace DotnetService.APIs;

public interface IProjectsService
{
    /// <summary>
    /// Create one Project
    /// </summary>
    public Task<ProjectDto> CreateProject(ProjectCreateInput projectDto);

    /// <summary>
    /// Delete one Project
    /// </summary>
    public Task DeleteProject(ProjectIdDto idDto);

    /// <summary>
    /// Find many Projects
    /// </summary>
    public Task<List<ProjectDto>> Projects(ProjectFindMany findManyArgs);

    /// <summary>
    /// Get one Project
    /// </summary>
    public Task<ProjectDto> Project(ProjectIdDto idDto);

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    public Task<MetadataDto> ProjectsMeta(ProjectFindMany findManyArgs);

    /// <summary>
    /// Update one Project
    /// </summary>
    public Task UpdateProject(ProjectIdDto idDto, ProjectUpdateInput updateDto);
}
