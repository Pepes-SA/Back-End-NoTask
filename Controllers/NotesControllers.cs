using Microsoft.AspNetCore.Components;
using BackEndNoTask.Data;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]



  public class NotesController : ControllerBase
  {
    private readonly BaseContext _context;

    public NotesController(BaseContext context)
    {
      _context = context;
    }
  }
