using DotnetService.APIs;
using DotnetService.APIs.Common;
using DotnetService.APIs.Dtos;
using DotnetService.APIs.Errors;
using DotnetService.APIs.Extensions;
using DotnetService.Infrastructure;
using DotnetService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetService.APIs;

public abstract class CommentsServiceBase : ICommentsService
{
    protected readonly DotnetServiceDbContext _context;

    public CommentsServiceBase(DotnetServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a task record for Comment
    /// </summary>
    public async Task<TaskDto> GetTask(CommentIdDto idDto)
    {
        var comment = await _context
            .Comments.Where(comment => comment.Id == idDto.Id)
            .Include(comment => comment.Task)
            .FirstOrDefaultAsync();
        if (comment == null)
        {
            throw new NotFoundException();
        }
        return comment.Task.ToDto();
    }

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    public async Task<MetadataDto> CommentsMeta(CommentFindMany findManyArgs)
    {
        var count = await _context.Comments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Comment
    /// </summary>
    public async Task<CommentDto> CreateComment(CommentCreateInput createDto)
    {
        var comment = new Comment
        {
            Content = createDto.Content,
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            comment.Id = createDto.Id;
        }
        if (createDto.Task != null)
        {
            comment.Task = await _context
                .Tasks.Where(task => createDto.Task.Id == task.Id)
                .FirstOrDefaultAsync();
        }

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Comment>(comment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Comment
    /// </summary>
    public async Task DeleteComment(CommentIdDto idDto)
    {
        var comment = await _context.Comments.FindAsync(idDto.Id);
        if (comment == null)
        {
            throw new NotFoundException();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Comments
    /// </summary>
    public async Task<List<CommentDto>> Comments(CommentFindMany findManyArgs)
    {
        var comments = await _context
            .Comments.Include(x => x.Task)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return comments.ConvertAll(comment => comment.ToDto());
    }

    /// <summary>
    /// Get one Comment
    /// </summary>
    public async Task<CommentDto> Comment(CommentIdDto idDto)
    {
        var comments = await this.Comments(
            new CommentFindMany { Where = new CommentWhereInput { Id = idDto.Id } }
        );
        var comment = comments.FirstOrDefault();
        if (comment == null)
        {
            throw new NotFoundException();
        }

        return comment;
    }

    /// <summary>
    /// Update one Comment
    /// </summary>
    public async Task UpdateComment(CommentIdDto idDto, CommentUpdateInput updateDto)
    {
        var comment = updateDto.ToModel(idDto);

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Comments.Any(e => e.Id == comment.Id))
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
