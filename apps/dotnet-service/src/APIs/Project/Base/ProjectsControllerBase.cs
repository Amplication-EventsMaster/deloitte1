using DotnetService.APIs;
using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;
using DotnetService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProjectsControllerBase : ControllerBase
{
    protected readonly IProjectsService _service;

    public ProjectsControllerBase(IProjectsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Project
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateInput input)
    {
        var project = await _service.CreateProject(input);

        return CreatedAtAction(nameof(Project), new { id = project.Id }, project);
    }

    /// <summary>
    /// Delete one Project
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteProject([FromRoute()] ProjectIdDto idDto)
    {
        try
        {
            await _service.DeleteProject(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Projects
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<ProjectDto>>> Projects([FromQuery()] ProjectFindMany filter)
    {
        return Ok(await _service.Projects(filter));
    }

    /// <summary>
    /// Get one Project
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ProjectDto>> Project([FromRoute()] ProjectIdDto idDto)
    {
        try
        {
            return await _service.Project(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProjectsMeta([FromQuery()] ProjectFindMany filter)
    {
        return Ok(await _service.ProjectsMeta(filter));
    }

    /// <summary>
    /// Update one Project
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateProject(
        [FromRoute()] ProjectIdDto idDto,
        [FromQuery()] ProjectUpdateInput projectUpdateDto
    )
    {
        try
        {
            await _service.UpdateProject(idDto, projectUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
