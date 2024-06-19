using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;

namespace DotnetService.APIs;

public interface ICommentsService
{
    /// <summary>
    /// Get a task record for Comment
    /// </summary>
    public Task<TaskDto> GetTask(CommentIdDto idDto);

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    public Task<MetadataDto> CommentsMeta(CommentFindMany findManyArgs);

    /// <summary>
    /// Create one Comment
    /// </summary>
    public Task<CommentDto> CreateComment(CommentCreateInput commentDto);

    /// <summary>
    /// Delete one Comment
    /// </summary>
    public Task DeleteComment(CommentIdDto idDto);

    /// <summary>
    /// Find many Comments
    /// </summary>
    public Task<List<CommentDto>> Comments(CommentFindMany findManyArgs);

    /// <summary>
    /// Get one Comment
    /// </summary>
    public Task<CommentDto> Comment(CommentIdDto idDto);

    /// <summary>
    /// Update one Comment
    /// </summary>
    public Task UpdateComment(CommentIdDto idDto, CommentUpdateInput updateDto);
}
