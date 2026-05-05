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

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note?> GetByIdAsync(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task<Note> CreateAsync(Note note)
    {
        note.CreatedAt = DateTime.UtcNow;

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return note;
    }

    public async Task<bool> UpdateAsync(int id, Note updatedNote)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
            return false;

        note.Title = updatedNote.Title;
        note.Content = updatedNote.Content;
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