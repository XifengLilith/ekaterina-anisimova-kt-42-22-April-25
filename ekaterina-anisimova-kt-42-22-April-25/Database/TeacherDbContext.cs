using ekaterina_anisimova_kt_42_22_April_25.Database.Configurations;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using Microsoft.EntityFrameworkCore;

namespace ekaterina_anisimova_kt_42_22_April_25.Database
{
    public class TeacherDbContext : DbContext
    {
        public TeacherDbContext(DbContextOptions<TeacherDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Load> Loads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicDegreeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
            modelBuilder.ApplyConfiguration(new LoadConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
