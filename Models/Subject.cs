namespace StudiaPraca.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LecturerSubject> LecturerSubjects { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}
