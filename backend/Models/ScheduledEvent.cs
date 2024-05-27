namespace StudiaPraca.Models
{
    public class ScheduledEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Subject Subject { get; set; }
        public Lecturer Lecturer { get; set; }
        public Room Room { get; set; }
    }
}
