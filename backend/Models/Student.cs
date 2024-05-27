namespace StudiaPraca.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public string FieldOfStudy { get; set; }
        public int Year { get; set; }
        public string Group { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PreferredHour> PreferredHours { get; set; }
        public List<StudentsLecture> StudentsLectures { get; set; }
    }
}
