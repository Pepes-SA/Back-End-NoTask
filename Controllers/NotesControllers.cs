using Microsoft.AspNetCore.Mvc;
using BackEndNoTask.Data;

namespace BackEndNoTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly BaseContext _context;

        public NotesController(BaseContext context)
        {
            _context = context;
        }

        // Aquí podrías agregar acciones (endpoints) para manejar solicitudes HTTP,
        // por ejemplo, GET, POST, PUT, DELETE, etc.
    }
}
