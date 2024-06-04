namespace StudiaPraca.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
