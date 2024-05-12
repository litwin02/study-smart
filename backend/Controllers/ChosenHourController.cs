using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudyCalendar.Server.Models;

namespace StudyCalendar.Server.Controllers
{
	[EnableCors]
	[ApiController]
    [Route("[controller]")]
    public class ChosenHourController : ControllerBase
    {

        private static IEnumerable<ChosenHour> Hours = new[]
        {
            new ChosenHour{Id = 1, Day = 1, Hour = 10, Availability = 0},
            new ChosenHour{Id = 2, Day = 1, Hour = 11, Availability = 0},
            new ChosenHour{Id = 3, Day = 2, Hour = 10, Availability = 0},
            new ChosenHour{Id = 4, Day = 2, Hour = 11, Availability = 0},
            new ChosenHour{Id = 5, Day = 3, Hour = 10, Availability = 0},
            new ChosenHour{Id = 6, Day = 3, Hour = 11, Availability = 0},
            new ChosenHour{Id = 7, Day = 4, Hour = 10, Availability = 0},
            new ChosenHour{Id = 8, Day = 4, Hour = 11, Availability = 0},
            new ChosenHour{Id = 9, Day = 5, Hour = 10, Availability = 0},
            new ChosenHour{Id = 10, Day = 5, Hour = 11, Availability = 0}
        };

        [HttpGet("{id:int}")]
        public ChosenHour[] Get(int id)
        {
            ChosenHour[] hours = Hours.Where(i => i.Id == id).ToArray();
            return hours;
        }

        [HttpGet]
        public IEnumerable<ChosenHour> Index()
        {
            ChosenHour[] hours = Hours.ToArray();
            return hours;
        }

		[HttpPut("add")]
		public IActionResult Add([FromBody] Dictionary<string, int> requestData)
		{
			if (requestData.ContainsKey("day") && requestData.ContainsKey("hour"))
			{
				int day = requestData["day"];
				int hour = requestData["hour"];

				var hourToUpdate = Hours.FirstOrDefault(h => h.Day == day && h.Hour == hour);
				if (hourToUpdate != null)
				{
					hourToUpdate.Availability++;
					return Ok(hourToUpdate);
				}
			}

			return BadRequest("Invalid data provided.");
		}

		[HttpPut("remove")]
		public IActionResult Remove([FromBody] ChosenHour updatedHour)
		{
			var hourToUpdate = Hours.FirstOrDefault(h => h.Day == updatedHour.Day && h.Hour == updatedHour.Hour);
			if (hourToUpdate != null)
			{
				if(hourToUpdate.Availability == 0) return BadRequest();
				hourToUpdate.Availability--;
				return Ok(hourToUpdate);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
