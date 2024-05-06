using Microsoft.EntityFrameworkCore;
using BackEndNoTask.Models;

namespace BackEndNoTask.Data
{
  public class BaseContext : DbContext
  {
    public BaseContext(DbContextOptions<BaseContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; }
    public DbSet<NoteCategory> NoteCategorys { get; set; }
  }

}