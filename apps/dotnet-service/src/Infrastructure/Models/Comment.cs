using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetService.Infrastructure.Models;

[Table("Comments")]
public class Comment
{
    [StringLength(1000)]
    public string? Content { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? TaskId { get; set; }

    [ForeignKey(nameof(TaskId))]
    public Task? Task { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
