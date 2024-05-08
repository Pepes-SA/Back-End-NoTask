using Microsoft.AspNetCore.Mvc;
using BackEndNoTask.Data;
using BackEndNoTask.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEndNoTask.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NotesController : Controller
  {
    private readonly BaseContext _context;

    public NotesController(BaseContext context)
    {
      _context = context;
    }

    //Listado de Notas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
      return await _context.Notes.ToListAsync();
    }

    // Detalles 
    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
      var note = await _context.Notes.FindAsync(id);

      if (note == null)
      {
        return NotFound();
      }
      return note;
    }

    [HttpGet("SearchByTitle/{title}")]
    public async Task<ActionResult<List<Note>>> SearchNotes(string title)
    {
      var lowercaseTitle = title.ToLowerInvariant();
      var notes = await _context.Notes.ToListAsync();
      var matchingNotes = notes.Where(n => n.Title.ToLowerInvariant().StartsWith(lowercaseTitle)).ToList();
      if (matchingNotes == null || !matchingNotes.Any())
      {
        return NotFound();
      }
      return matchingNotes;
    }



    //Crear una nota
    [HttpPost]
    public async Task<ActionResult<Note>> PostNote([Bind("Title,Text,IdNoteCategory")] Note data)
    {
      Note note = new Note
      {
        Title = data.Title,
        Text = data.Text,
        CreationDate = DateTime.Now,
        Status = "Activo",
        IdNoteCategory = data.IdNoteCategory
      };

      _context.Notes.Add(note);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetNote", new { id = note.Id }, note);

    }


    //Eliminar una nota

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
      var note = await _context.Notes.FindAsync(id);
      if (note == null)
      {
        return NotFound();
      }

      _context.Notes.Remove(note);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    //Actualizar nota
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNote(int id, Note updatedNote)
    {
      if (id != updatedNote.Id)
      {
        return BadRequest();
      }

      var note = await _context.Notes.FindAsync(id);
      if (note == null)
      {
        return NotFound();
      }
      note.Title = updatedNote.Title;
      note.Text = updatedNote.Text;
      note.CreationDate = updatedNote.CreationDate;
      note.Status = updatedNote.Status;

      try
      {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!NoteExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return CreatedAtAction("GetNote", new { id = note.Id }, note);
    }

    [HttpPut("changeStatus/{id}/{status}")]
    public async Task<IActionResult> ChangeStatusNote(int id, [FromRoute] string status)
    {
      var note = await _context.Notes.FindAsync(id);
      if (note == null)
      {
        return NotFound();
      }
      //note.Status = status;

      try
      {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!NoteExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return CreatedAtAction("GetNote", new { id = note.Id }, note);
    }
    private bool NoteExists(int id)
    {
      return _context.Notes.Any(e => e.Id == id);
    }

    [HttpGet("filter/{filter}")]
    public async Task<ActionResult<IEnumerable<Note>>> GetFilteredNotes(string filter)
    {
      IQueryable<Note> notesQuery = _context.Notes;

      switch (filter)
      {
        case "1":
          notesQuery = notesQuery.Where(n => n.Status == "Archivado");
          break;
        case "2":
          notesQuery = notesQuery.Where(n => n.Status == "Eliminado");
          break;
        case "3":
          notesQuery = notesQuery.OrderBy(n => n.Status); ;
          break;
        case "4":
          notesQuery = notesQuery.OrderBy(n => n.CreationDate);
          break;
        default:
          return BadRequest("Filtro no válido");
      }
      var notes = await notesQuery.ToListAsync();
      return notes;
    }


  }
}
