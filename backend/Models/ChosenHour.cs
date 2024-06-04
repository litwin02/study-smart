namespace StudyCalendar.Server.Models
{
    public class ChosenHour
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Availability { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
