using Application.Dtos;
using Application.Models;

namespace Application.Interfaces;

public interface INotesService
{
    Task<PagedResult<NoteDto>> GetAllAsync(NoteQueryParams queryParams);
    Task<NoteDto?> GetByIdAsync(int id);
    Task<NoteDto> CreateAsync(NoteCreateDto dto);
    Task<NoteDto?> UpdateAsync(int id, NoteUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}