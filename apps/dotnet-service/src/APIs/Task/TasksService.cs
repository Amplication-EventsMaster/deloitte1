using DotnetService.Infrastructure;

namespace DotnetService.APIs;

public class TasksService : TasksServiceBase
{
    public TasksService(DotnetServiceDbContext context)
        : base(context) { }
}
