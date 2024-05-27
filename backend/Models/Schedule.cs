namespace StudiaPraca.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ScheduledEvent> Events { get; set; }
    }
}
