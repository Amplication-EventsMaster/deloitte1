using DotnetService.APIs;
using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;
using DotnetService.APIs.Errors;
using DotnetService.APIs.Extensions;
using DotnetService.Infrastructure;
using DotnetService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetService.APIs;

public abstract class ProjectsServiceBase : IProjectsService
{
    protected readonly DotnetServiceDbContext _context;

    public ProjectsServiceBase(DotnetServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Project
    /// </summary>
    public async Task<ProjectDto> CreateProject(ProjectCreateInput createDto)
    {
        var project = new Project
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            project.Id = createDto.Id;
        }

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Project>(project.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Project
    /// </summary>
    public async Task DeleteProject(ProjectIdDto idDto)
    {
        var project = await _context.Projects.FindAsync(idDto.Id);
        if (project == null)
        {
            throw new NotFoundException();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Projects
    /// </summary>
    public async Task<List<ProjectDto>> Projects(ProjectFindMany findManyArgs)
    {
        var projects = await _context
            .Projects.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return projects.ConvertAll(project => project.ToDto());
    }

    /// <summary>
    /// Get one Project
    /// </summary>
    public async Task<ProjectDto> Project(ProjectIdDto idDto)
    {
        var projects = await this.Projects(
            new ProjectFindMany { Where = new ProjectWhereInput { Id = idDto.Id } }
        );
        var project = projects.FirstOrDefault();
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    public async Task<MetadataDto> ProjectsMeta(ProjectFindMany findManyArgs)
    {
        var count = await _context.Projects.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Project
    /// </summary>
    public async Task UpdateProject(ProjectIdDto idDto, ProjectUpdateInput updateDto)
    {
        var project = updateDto.ToModel(idDto);

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Projects.Any(e => e.Id == project.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
