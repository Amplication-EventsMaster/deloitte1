using DotnetService.Infrastructure;

namespace DotnetService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(DotnetServiceDbContext context)
        : base(context) { }
}
