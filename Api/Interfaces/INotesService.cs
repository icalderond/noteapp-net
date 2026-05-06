using Api.Models;
namespace Api.Interfaces;

public interface INotesService
{
    Task<IEnumerable<NoteDto>> GetAllAsync();
    Task<NoteDto?> GetByIdAsync(int id);
    Task<NoteDto> CreateAsync(NoteCreateDto dto);
    Task<bool> UpdateAsync(int id, NoteUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}