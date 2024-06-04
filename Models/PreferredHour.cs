using StudyCalendar.Server.Models;

namespace StudiaPraca.Models
{
    public class PreferredHour
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ChosenHourId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Student Student { get; set; }
        public ChosenHour ChosenHour { get; set; }
    }
}
