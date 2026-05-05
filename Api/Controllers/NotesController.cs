using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Api.Models;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;

    public NotesController(INotesService notesService)
    {
        _notesService = notesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _notesService.GetAllAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var note = await _notesService.GetByIdAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Note note)
    {
        var createdNote = await _notesService.CreateAsync(note);
        return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, Note note)
    {
        var updatedNote = await _notesService.UpdateAsync(id, note);
        if (updatedNote == null)
        {
            return NotFound();
        }
        return Ok(updatedNote);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var result = await _notesService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}