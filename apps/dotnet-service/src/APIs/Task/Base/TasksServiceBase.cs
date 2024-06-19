using DotnetService.APIs;
using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;
using DotnetService.APIs.Errors;
using DotnetService.APIs.Extensions;
using DotnetService.Infrastructure;
using DotnetService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetService.APIs;

public abstract class TasksServiceBase : ITasksService
{
    protected readonly DotnetServiceDbContext _context;

    public TasksServiceBase(DotnetServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Task
    /// </summary>
    public async Task<TaskDto> CreateTask(TaskCreateInput createDto)
    {
        var task = new Task
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Status = createDto.Status,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            task.Id = createDto.Id;
        }
        if (createDto.Comments != null)
        {
            task.Comments = await _context
                .Comments.Where(comment =>
                    createDto.Comments.Select(t => t.Id).Contains(comment.Id)
                )
                .ToListAsync();
        }

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Task>(task.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Task
    /// </summary>
    public async Task DeleteTask(TaskIdDto idDto)
    {
        var task = await _context.Tasks.FindAsync(idDto.Id);
        if (task == null)
        {
            throw new NotFoundException();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Tasks
    /// </summary>
    public async Task<List<TaskDto>> Tasks(TaskFindMany findManyArgs)
    {
        var tasks = await _context
            .Tasks.Include(x => x.Comments)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return tasks.ConvertAll(task => task.ToDto());
    }

    /// <summary>
    /// Get one Task
    /// </summary>
    public async Task<TaskDto> Task(TaskIdDto idDto)
    {
        var tasks = await this.Tasks(
            new TaskFindMany { Where = new TaskWhereInput { Id = idDto.Id } }
        );
        var task = tasks.FirstOrDefault();
        if (task == null)
        {
            throw new NotFoundException();
        }

        return task;
    }

    /// <summary>
    /// Connect multiple Comments records to Task
    /// </summary>
    public async Task ConnectComments(TaskIdDto idDto, CommentIdDto[] commentsId)
    {
        var task = await _context
            .Tasks.Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (task == null)
        {
            throw new NotFoundException();
        }

        var comments = await _context
            .Comments.Where(t => commentsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (comments.Count == 0)
        {
            throw new NotFoundException();
        }

        var commentsToConnect = comments.Except(task.Comments);

        foreach (var comment in commentsToConnect)
        {
            task.Comments.Add(comment);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Comments records from Task
    /// </summary>
    public async Task DisconnectComments(TaskIdDto idDto, CommentIdDto[] commentsId)
    {
        var task = await _context
            .Tasks.Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (task == null)
        {
            throw new NotFoundException();
        }

        var comments = await _context
            .Comments.Where(t => commentsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var comment in comments)
        {
            task.Comments?.Remove(comment);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Comments records for Task
    /// </summary>
    public async Task<List<CommentDto>> FindComments(TaskIdDto idDto, CommentFindMany taskFindMany)
    {
        var comments = await _context
            .Comments.Where(m => m.TaskId == idDto.Id)
            .ApplyWhere(taskFindMany.Where)
            .ApplySkip(taskFindMany.Skip)
            .ApplyTake(taskFindMany.Take)
            .ApplyOrderBy(taskFindMany.SortBy)
            .ToListAsync();

        return comments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    public async Task<MetadataDto> TasksMeta(TaskFindMany findManyArgs)
    {
        var count = await _context.Tasks.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Comments records for Task
    /// </summary>
    public async Task UpdateComments(TaskIdDto idDto, CommentIdDto[] commentsId)
    {
        var task = await _context
            .Tasks.Include(t => t.Comments)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (task == null)
        {
            throw new NotFoundException();
        }

        var comments = await _context
            .Comments.Where(a => commentsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (comments.Count == 0)
        {
            throw new NotFoundException();
        }

        task.Comments = comments;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Task
    /// </summary>
    public async Task UpdateTask(TaskIdDto idDto, TaskUpdateInput updateDto)
    {
        var task = updateDto.ToModel(idDto);

        if (updateDto.Comments != null)
        {
            task.Comments = await _context
                .Comments.Where(comment =>
                    updateDto.Comments.Select(t => t.Id).Contains(comment.Id)
                )
                .ToListAsync();
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == task.Id))
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
