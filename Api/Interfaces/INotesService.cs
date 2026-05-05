using Api.Models;
namespace Api.Interfaces;

public interface INotesService
{
    Task<IEnumerable<Note>> GetAllAsync();
    Task<Note?> GetByIdAsync(int id);
    Task<Note> CreateAsync(Note note);
    Task<bool> UpdateAsync(int id, Note note);
    Task<bool> DeleteAsync(int id);
}