using NotesApp.Models.Notes;

namespace NotesApp.Repo.RepoNotes
{
    public interface INotes
    {
        List<NotesDB> GetAll();
        NotesDB GetById(Guid id);
        void Insert(NotesDB newNote);
        void Update(Guid id, NotesDB newNote);
        void Delete(Guid id);
    }
}
