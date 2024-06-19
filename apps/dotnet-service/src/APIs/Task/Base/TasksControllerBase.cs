using DotnetService.APIs;
using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;
using DotnetService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TasksControllerBase : ControllerBase
{
    protected readonly ITasksService _service;

    public TasksControllerBase(ITasksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Task
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<TaskDto>> CreateTask(TaskCreateInput input)
    {
        var task = await _service.CreateTask(input);

        return CreatedAtAction(nameof(Task), new { id = task.Id }, task);
    }

    /// <summary>
    /// Delete one Task
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteTask([FromRoute()] TaskIdDto idDto)
    {
        try
        {
            await _service.DeleteTask(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Tasks
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<TaskDto>>> Tasks([FromQuery()] TaskFindMany filter)
    {
        return Ok(await _service.Tasks(filter));
    }

    /// <summary>
    /// Get one Task
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<TaskDto>> Task([FromRoute()] TaskIdDto idDto)
    {
        try
        {
            return await _service.Task(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Comments records to Task
    /// </summary>
    [HttpPost("{Id}/comments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectComments(
        [FromRoute()] TaskIdDto idDto,
        [FromQuery()] CommentIdDto[] commentsId
    )
    {
        try
        {
            await _service.ConnectComments(idDto, commentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Comments records from Task
    /// </summary>
    [HttpDelete("{Id}/comments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectComments(
        [FromRoute()] TaskIdDto idDto,
        [FromBody()] CommentIdDto[] commentsId
    )
    {
        try
        {
            await _service.DisconnectComments(idDto, commentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Comments records for Task
    /// </summary>
    [HttpGet("{Id}/comments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<CommentDto>>> FindComments(
        [FromRoute()] TaskIdDto idDto,
        [FromQuery()] CommentFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindComments(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TasksMeta([FromQuery()] TaskFindMany filter)
    {
        return Ok(await _service.TasksMeta(filter));
    }

    /// <summary>
    /// Update multiple Comments records for Task
    /// </summary>
    [HttpPatch("{Id}/comments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateComments(
        [FromRoute()] TaskIdDto idDto,
        [FromBody()] CommentIdDto[] commentsId
    )
    {
        try
        {
            await _service.UpdateComments(idDto, commentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Task
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateTask(
        [FromRoute()] TaskIdDto idDto,
        [FromQuery()] TaskUpdateInput taskUpdateDto
    )
    {
        try
        {
            await _service.UpdateTask(idDto, taskUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
