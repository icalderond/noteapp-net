using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Models;
using Application.Dtos;
using Domain.Models;
using Infraestructure;

namespace Api.Services;

public class NotesService : INotesService
{
    private readonly AppDbContext _context;

    public NotesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<NoteDto>> GetAllAsync(NoteQueryParams query)
    {

        var notesQuery = _context.Notes.AsQueryable();

        // 🔍 Filtro (search)
        if (!string.IsNullOrEmpty(query.Search))
        {
            notesQuery = notesQuery.Where(n =>
                n.Title.Contains(query.Search) ||
                n.Content.Contains(query.Search));
        }

        // 🔽 Ordenamiento
        notesQuery = query.SortBy?.ToLower() switch
        {
            "title" => query.Desc
                ? notesQuery.OrderByDescending(n => n.Title)
                : notesQuery.OrderBy(n => n.Title),

            "createdat" => query.Desc
                ? notesQuery.OrderByDescending(n => n.CreatedAt)
                : notesQuery.OrderBy(n => n.CreatedAt),

            _ => notesQuery.OrderByDescending(n => n.CreatedAt)
        };

        // 📊 Total antes de paginar
        var total = await notesQuery.CountAsync();

        // 📄 Paginación
        var data = await notesQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();

        return new PagedResult<NoteDto>
        {
            Total = total,
            Page = query.Page,
            PageSize = query.PageSize,
            Data = data
        };
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

    public async Task<NoteDto?> UpdateAsync(int id, NoteUpdateDto dto)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
            return null;

        note.Title = dto.Title;
        note.Content = dto.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
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