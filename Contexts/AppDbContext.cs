using Microsoft.EntityFrameworkCore;
using StudiaPraca.Models;
using StudyCalendar.Server.Models;

namespace StudiaPraca.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ChosenHour> ChosenHours { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<PreferredHour> PreferredHours { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<LecturerSubject> LecturerSubjects { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvents { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<StudentsLecture> StudentsLectures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite keys and relationships
            modelBuilder.Entity<PreferredHour>()
                .HasKey(ph => ph.Id);

            modelBuilder.Entity<LecturerSubject>()
                .HasKey(ls => ls.Id);

            modelBuilder.Entity<StudentsLecture>()
                .HasKey(sl => new { sl.LectureId, sl.StudentId, sl.LecturerId });

            modelBuilder.Entity<StudentsLecture>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.StudentsLectures)
                .HasForeignKey(sl => sl.StudentId);

            modelBuilder.Entity<StudentsLecture>()
                .HasOne(sl => sl.Lecturer)
                .WithMany(l => l.StudentsLectures)
                .HasForeignKey(sl => sl.LecturerId);
        }
    }
}
