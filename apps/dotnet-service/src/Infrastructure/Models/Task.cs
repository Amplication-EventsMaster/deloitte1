using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetService.Core.Enums;

namespace DotnetService.Infrastructure.Models;

[Table("Tasks")]
public class Task
{
    public List<Comment>? Comments { get; set; } = new List<Comment>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public StatusEnum? Status { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
