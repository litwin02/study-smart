namespace StudiaPraca.Models
{
    public class StudentsLecture
    {
        public int LectureId { get; set; }
        public int StudentId { get; set; }
        public int LecturerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Lecture Lecture { get; set; }
        public Student Student { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}
