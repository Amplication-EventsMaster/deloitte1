using DotnetService.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotnetService.Infrastructure;

public class DotnetServiceDbContext : IdentityDbContext<IdentityUser>
{
    public DotnetServiceDbContext(DbContextOptions<DotnetServiceDbContext> options)
        : base(options) { }

    public DbSet<Task> Tasks { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<User> Users { get; set; }
}
