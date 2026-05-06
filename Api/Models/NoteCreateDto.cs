using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class NoteCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Content { get; set; }
}