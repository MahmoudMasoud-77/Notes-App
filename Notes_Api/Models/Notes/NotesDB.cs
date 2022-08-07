namespace NotesApp.Models.Notes
{
    public class NotesDB
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsVisible  { get; set; }

    }
}
