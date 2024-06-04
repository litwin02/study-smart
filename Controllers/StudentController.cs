using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiaPraca.Models;
using StudiaPraca;
using StudyCalendar.Server.Models;
using System.Linq;
using System.Threading.Tasks;
using StudiaPraca.Contexts;

namespace StudiaPraca
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint for student login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IndexNumber == request.IndexNumber);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Normally, you would validate the password here, but for simplicity, we'll skip that step.
            return Ok(student);
        }



        // Endpoint for setting preferred hours
        [HttpPost("set-preference")]
        public async Task<IActionResult> SetPreference([FromBody] PreferredHourRequest request)
        {
            var student = await _context.Students
                .Include(s => s.PreferredHours)
                .FirstOrDefaultAsync(s => s.Id == request.StudentId);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            var chosenHour = await _context.ChosenHours
                .FirstOrDefaultAsync(ch => ch.Id == request.ChosenHourId);

            if (chosenHour == null)
            {
                return NotFound("Chosen hour not found");
            }

            var existingPreference = student.PreferredHours
                .FirstOrDefault(ph => ph.ChosenHourId == request.ChosenHourId);

            if (existingPreference != null)
            {
                return BadRequest("This hour is already set as preferred");
            }

            var preferredHour = new PreferredHour
            {
                StudentId = student.Id,
                ChosenHourId = chosenHour.Id,
                CreatedAt = DateTime.UtcNow
            };

            student.PreferredHours.Add(preferredHour);
            await _context.SaveChangesAsync();

            return Ok(preferredHour);
        }

        // Endpoint for removing preferred hour
        [HttpDelete("remove-preference/{studentId}/{chosenHourId}")]
        public async Task<IActionResult> RemovePreference(int studentId, int chosenHourId)
        {
            var student = await _context.Students
                .Include(s => s.PreferredHours)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            var preferredHour = student.PreferredHours
                .FirstOrDefault(ph => ph.ChosenHourId == chosenHourId);

            if (preferredHour == null)
            {
                return NotFound("Preferred hour not found");
            }

            student.PreferredHours.Remove(preferredHour);
            await _context.SaveChangesAsync();

            return Ok("Preferred hour removed");
        }
    }

    public class LoginRequest
    {
        public string IndexNumber { get; set; }
        // public string Password { get; set; } // Add password if needed
    }

    public class PreferredHourRequest
    {
        public int StudentId { get; set; }
        public int ChosenHourId { get; set; }
    }
}
