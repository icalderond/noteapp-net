using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;
public class NoteUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Content { get; set; }
}