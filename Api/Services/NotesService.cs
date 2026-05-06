using Microsoft.EntityFrameworkCore;
using Api.Interfaces;
using Api.Models;

namespace Api.Services;

public class NotesService : INotesService
{
    private readonly AppDbContext _context;

    public NotesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NoteDto>> GetAllAsync()
    {
     var notes = await _context.Notes.ToListAsync();

     return notes.Select(note => new NoteDto
     {
         Id = note.Id,
         Title = note.Title,
         Content = note.Content,
         CreatedAt = note.CreatedAt
     });
    }

    public async Task<NoteDto?> GetByIdAsync(int id)
    {
        var dto = await _context.Notes.FindAsync(id);

        return dto == null ? null : new NoteDto
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = dto.CreatedAt
        };
    }

    public async Task<NoteDto> CreateAsync(NoteCreateDto dto)
    {
        var note = new Note
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, NoteUpdateDto dto)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
            return false;

        note.Title = dto.Title;
        note.Content = dto.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
            return false;

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return true;
    }
}