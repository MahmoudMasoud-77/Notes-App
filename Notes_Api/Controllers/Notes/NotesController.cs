using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models.Notes;
using NotesApp.Repo.RepoNotes;

namespace NotesApp.Controllers.Notes
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotes NotesRepo;
        public NotesController(INotes _NotesRepo)
        {
            this.NotesRepo = _NotesRepo;
        }
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            return Ok(NotesRepo.GetAll());
        }
        [HttpGet("{id:Guid}")]
        [ActionName("GetNoteById")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var notesDB = NotesRepo.GetById(id);
            if (notesDB == null)
                return NotFound();
            
            return Ok(notesDB);
        }
        [HttpPost]
        public IActionResult AddNote( NotesDB newNote)
        {
            if (ModelState.IsValid)
            {
                newNote.Id = Guid.NewGuid();
                NotesRepo.Insert(newNote);
                return Created("http://localhost:64871/api/Notes" + newNote.Id, newNote);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:Guid}")]
        public IActionResult UpdateNote([FromRoute] Guid id, [FromBody] NotesDB newNote)
        {
            if (ModelState.IsValid)
            {
                //updata
                NotesRepo.Update(id, newNote);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteNote([FromRoute] Guid id)
        {
            try
            {
                NotesRepo.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

    }
}
