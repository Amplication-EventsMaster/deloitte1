using DotnetService.Infrastructure;

namespace DotnetService.APIs;

public class ProjectsService : ProjectsServiceBase
{
    public ProjectsService(DotnetServiceDbContext context)
        : base(context) { }
}
