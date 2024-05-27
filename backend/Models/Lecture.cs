namespace StudiaPraca.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Subject Subject { get; set; }
        public Schedule Schedule { get; set; }
    }
}
