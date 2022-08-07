using Microsoft.EntityFrameworkCore;
using NotesApp.Models.Notes;

namespace NotesApp.Models
{
    public class ContextDB:DbContext
    {
        public ContextDB()
        {   
        }
        public ContextDB(DbContextOptions options):base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProjectWebApi;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<NotesDB> Notes { get; set; }
    }
}
