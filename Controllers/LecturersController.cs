using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiaPraca.Contexts;
using StudiaPraca.Models;

namespace StudiaPraca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LecturersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/lecturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecturer>>> GetLecturers()
        {
            return await _context.Lecturers.Include(l => l.LecturerSubjects)
                                           .Include(l => l.StudentsLectures)
                                           .ToListAsync();
        }

        // GET: api/lecturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecturer>> GetLecturer(int id)
        {
            var lecturer = await _context.Lecturers.Include(l => l.LecturerSubjects)
                                                   .Include(l => l.StudentsLectures)
                                                   .FirstOrDefaultAsync(l => l.Id == id);

            if (lecturer == null)
            {
                return NotFound();
            }

            return lecturer;
        }

        // PUT: api/lecturers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturer(int id, Lecturer lecturer)
        {
            if (id != lecturer.Id)
            {
                return BadRequest();
            }

            _context.Entry(lecturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/lecturers
        [HttpPost]
        public async Task<ActionResult<Lecturer>> PostLecturer(Lecturer lecturer)
        {
            lecturer.CreatedAt = DateTime.SpecifyKind(lecturer.CreatedAt, DateTimeKind.Utc);
            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturer", new { id = lecturer.Id }, lecturer);
        }

        // DELETE: api/lecturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.Id == id);
        }
    }

}
