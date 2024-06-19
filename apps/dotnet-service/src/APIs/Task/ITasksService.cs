using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;

namespace DotnetService.APIs;

public interface ITasksService
{
    /// <summary>
    /// Create one Task
    /// </summary>
    public Task<TaskDto> CreateTask(TaskCreateInput taskDto);

    /// <summary>
    /// Delete one Task
    /// </summary>
    public Task DeleteTask(TaskIdDto idDto);

    /// <summary>
    /// Find many Tasks
    /// </summary>
    public Task<List<TaskDto>> Tasks(TaskFindMany findManyArgs);

    /// <summary>
    /// Get one Task
    /// </summary>
    public Task<TaskDto> Task(TaskIdDto idDto);

    /// <summary>
    /// Connect multiple Comments records to Task
    /// </summary>
    public Task ConnectComments(TaskIdDto idDto, CommentIdDto[] commentsId);

    /// <summary>
    /// Disconnect multiple Comments records from Task
    /// </summary>
    public Task DisconnectComments(TaskIdDto idDto, CommentIdDto[] commentsId);

    /// <summary>
    /// Find multiple Comments records for Task
    /// </summary>
    public Task<List<CommentDto>> FindComments(TaskIdDto idDto, CommentFindMany CommentFindMany);

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    public Task<MetadataDto> TasksMeta(TaskFindMany findManyArgs);

    /// <summary>
    /// Update multiple Comments records for Task
    /// </summary>
    public Task UpdateComments(TaskIdDto idDto, CommentIdDto[] commentsId);

    /// <summary>
    /// Update one Task
    /// </summary>
    public Task UpdateTask(TaskIdDto idDto, TaskUpdateInput updateDto);
}
