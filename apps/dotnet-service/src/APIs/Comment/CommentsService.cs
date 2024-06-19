using DotnetService.Infrastructure;

namespace DotnetService.APIs;

public class CommentsService : CommentsServiceBase
{
    public CommentsService(DotnetServiceDbContext context)
        : base(context) { }
}
