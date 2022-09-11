using Microsoft.EntityFrameworkCore;

namespace Api.Model
{
    public class StudentContext:DbContext
    {
        public DbSet<Student> students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=OSMAN;Initial Catalog=StudentDb;Trusted_Connection=true;");
        }
    }
}
