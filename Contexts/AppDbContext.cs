using Microsoft.EntityFrameworkCore;
using StudyCalendar.Server.Models;

namespace StudiaPraca.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ChosenHour> ChosenHours { get; set; }
    }
}
