using DotnetService.APIs;

namespace DotnetService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
