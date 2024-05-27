namespace StudyCalendar.Server.Models
{
    public class ChosenHour
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public DateTime Hour { get; set; }
        public int Availability { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
