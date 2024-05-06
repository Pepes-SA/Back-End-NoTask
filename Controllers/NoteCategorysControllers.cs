using Microsoft.AspNetCore.Mvc;
using BackEndNoTask.Data;
using BackEndNoTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndNoTask.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class NoteCategorysController : Controller
  {
    private readonly BaseContext _context;

    public NoteCategorysController(BaseContext context)
    {
      _context = context;
    }

    //Listado de Notas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteCategory>>> GetNoteCategorys()
    {
      return await _context.NoteCategorys.ToListAsync();
    }

    // Detalles 
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteCategory>> GetNoteCategory(int id)
    {
      var note = await _context.NoteCategorys.FindAsync(id);

      if (note == null)
      {
        return NotFound();
      }
      return note;
    }

    //Crear una nota
    [HttpPost]
    public async Task<ActionResult<NoteCategory>> PostNoteCategory([Bind("Name")] NoteCategory data)
    {
      NoteCategory category = new NoteCategory()
      {
        Name = data.Name,
        CreationDate = DateTime.Now,
        Status = "Activo"
      };
      _context.NoteCategorys.Add(category);
      await _context.SaveChangesAsync();
      return CreatedAtAction("GetNoteCategory", new { id = category.Id }, category);
    }

    //Eliminar una nota

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNoteCategory(int id)
    {
      var note = await _context.NoteCategorys.FindAsync(id);
      if (note == null)
      {
        return NotFound();
      }

      _context.NoteCategorys.Remove(note);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    //Actualizar nota
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoteCategory(int id, NoteCategory updatedNote)
    {
      if (id != updatedNote.Id)
      {
        return BadRequest();
      }

      var note = await _context.NoteCategorys.FindAsync(id);
      if (note == null)
      {
        return NotFound();
      }
      note.Name = updatedNote.Name;
      note.CreationDate = updatedNote.CreationDate;
      note.Status = updatedNote.Status;

      try
      {
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
      return NoContent();
    }

    [HttpPut("{id}/{status}")]
    public async Task<IActionResult> ChangeStatusCategory(int id, string status)
    {
      var note = await _context.NoteCategorys.FindAsync(id);
      if (note == null)
      {
        return NotFound();
      }
      note.Status = status;

      try
      {
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
      return NoContent();
    }

    private bool NoteExists(int id)
    {
      return _context.NoteCategorys.Any(e => e.Id == id);
    }
  }
}
