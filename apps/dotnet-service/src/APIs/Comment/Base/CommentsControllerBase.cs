using DotnetService.APIs;
using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;
using DotnetService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CommentsControllerBase : ControllerBase
{
    protected readonly ICommentsService _service;

    public CommentsControllerBase(ICommentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get a task record for Comment
    /// </summary>
    [HttpGet("{Id}/tasks")]
    public async Task<ActionResult<List<TaskDto>>> GetTask([FromRoute()] CommentIdDto idDto)
    {
        var task = await _service.GetTask(idDto);
        return Ok(task);
    }

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CommentsMeta([FromQuery()] CommentFindMany filter)
    {
        return Ok(await _service.CommentsMeta(filter));
    }

    /// <summary>
    /// Create one Comment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CommentDto>> CreateComment(CommentCreateInput input)
    {
        var comment = await _service.CreateComment(input);

        return CreatedAtAction(nameof(Comment), new { id = comment.Id }, comment);
    }

    /// <summary>
    /// Delete one Comment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteComment([FromRoute()] CommentIdDto idDto)
    {
        try
        {
            await _service.DeleteComment(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Comments
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<CommentDto>>> Comments([FromQuery()] CommentFindMany filter)
    {
        return Ok(await _service.Comments(filter));
    }

    /// <summary>
    /// Get one Comment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CommentDto>> Comment([FromRoute()] CommentIdDto idDto)
    {
        try
        {
            return await _service.Comment(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Comment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateComment(
        [FromRoute()] CommentIdDto idDto,
        [FromQuery()] CommentUpdateInput commentUpdateDto
    )
    {
        try
        {
            await _service.UpdateComment(idDto, commentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
