namespace BackEndNoTask.Models
{
  public class Note
  {
    public int Id { get; set; }

    public string? Title { get; set; }
    
    public string? Text { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Status { get; set; }
    public int? IdNoteCategory { get; set; }
  }
}