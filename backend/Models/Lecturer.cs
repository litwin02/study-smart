namespace StudiaPraca.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LecturerSubject> LecturerSubjects { get; set; }
        public List<StudentsLecture> StudentsLectures { get; set; }
    }
}
