namespace StudiaPraca.Models
{
    public class LecturerSubject
    {
        public int Id { get; set; }
        public int LecturerId { get; set; }
        public int SubjectId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Lecturer Lecturer { get; set; }
        public Subject Subject { get; set; }
    }
}
