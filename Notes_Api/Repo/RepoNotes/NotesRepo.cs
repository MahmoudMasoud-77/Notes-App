using NotesApp.Models;
using NotesApp.Models.Notes;

namespace NotesApp.Repo.RepoNotes
{
    public class NotesRepo :INotes
    {
        ContextDB context;
        public NotesRepo(ContextDB _context)
        {
            context = _context;
        }
        public void Delete(Guid id)
        {
            NotesDB note = GetById(id);
            context.Notes.Remove(note);
            context.SaveChanges();
        }

        public List<NotesDB> GetAll()
        {
            List<NotesDB> notes= context.Notes.ToList();
            return notes;
        }

        public NotesDB GetById(Guid id)
        {
            NotesDB note = context.Notes.FirstOrDefault(x => x.Id == id);
            return note;
        }

        public void Insert(NotesDB newNote)
        {
            context.Notes.Add(newNote);
            context.SaveChanges();
        }

        public void Update(Guid id, NotesDB newNote)
        {
            NotesDB OldNote = GetById(id);
            OldNote.Title = newNote.Title;
            OldNote.Description = newNote.Description;
            OldNote.IsVisible = newNote.IsVisible;
            
            context.SaveChanges();

        }
    }
}
