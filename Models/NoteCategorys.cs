namespace BackEndNoTask.Models

{
  public class NoteCategory
  {
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Status { get; set; }
  }
}