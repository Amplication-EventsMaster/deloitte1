using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs;

[ApiController()]
public class TasksController : TasksControllerBase
{
    public TasksController(ITasksService service)
        : base(service) { }
}
