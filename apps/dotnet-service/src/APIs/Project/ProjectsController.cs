using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs;

[ApiController()]
public class ProjectsController : ProjectsControllerBase
{
    public ProjectsController(IProjectsService service)
        : base(service) { }
}
