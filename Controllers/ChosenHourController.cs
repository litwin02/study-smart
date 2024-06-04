using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudiaPraca.Contexts;
using StudyCalendar.Server.Models;

namespace StudyCalendar.Server.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class ChosenHourController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChosenHourController(AppDbContext context)
        {
            _context = context;
        }

        // Metoda GET zwracająca wybrane godziny dla danego studenta
        [HttpGet("student/{studentId:int}")]
        public async Task<ActionResult<IEnumerable<ChosenHour>>> GetChosenHoursByStudent(int studentId)
        {
            var preferredHours = await _context.PreferredHours
                .Where(ph => ph.StudentId == studentId)
                .Select(ph => ph.ChosenHour)
                .ToListAsync();

            if (preferredHours == null || !preferredHours.Any())
            {
                return NotFound();
            }

            return Ok(preferredHours);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ChosenHour>> GetChosenHour(int id)
        {
            var chosenHour = await _context.ChosenHours.FindAsync(id);
            if (chosenHour == null)
            {
                return NotFound();
            }
            return chosenHour;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChosenHour>>> GetChosenHours()
        {
            return await _context.ChosenHours.ToListAsync();
        }

        [HttpPut("add")]
        public async Task<IActionResult> AddChosenHour([FromBody] Dictionary<string, int> requestData)
        {
            if (requestData.ContainsKey("day") && requestData.ContainsKey("hour"))
            {
                int day = requestData["day"];
                int hour = requestData["hour"];

                var chosenHour = await _context.ChosenHours
                    .FirstOrDefaultAsync(h => h.Day == day && h.Hour.Hour == hour);

                if (chosenHour != null)
                {
                    chosenHour.Availability++;
                    await _context.SaveChangesAsync();
                    return Ok(chosenHour);
                }
            }

            return BadRequest("Invalid data provided.");
        }

        [HttpPut("remove")]
        public async Task<IActionResult> RemoveChosenHour([FromBody] ChosenHour updatedHour)
        {
            var chosenHour = await _context.ChosenHours
                .FirstOrDefaultAsync(h => h.Day == updatedHour.Day && h.Hour.Hour == updatedHour.Hour);

            if (chosenHour != null)
            {
                if (chosenHour.Availability == 0)
                {
                    return BadRequest();
                }

                chosenHour.Availability--;
                await _context.SaveChangesAsync();
                return Ok(chosenHour);
            }

            return BadRequest();
        }
    }
}
