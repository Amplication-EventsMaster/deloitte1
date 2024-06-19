using Microsoft.AspNetCore.Mvc;

namespace DotnetService.APIs;

[ApiController()]
public class CommentsController : CommentsControllerBase
{
    public CommentsController(ICommentsService service)
        : base(service) { }
}
