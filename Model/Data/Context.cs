using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CourseProjectOOP.Model.Data
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public string DataBaseConnection = "Server=localhost; port=5432; user id=postgres; password=marta2010; database=CourseProjectDB";
        public Context() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DataBaseConnection);
        }
    }
}
