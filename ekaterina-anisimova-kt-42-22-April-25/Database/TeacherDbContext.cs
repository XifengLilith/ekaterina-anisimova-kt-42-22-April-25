using Microsoft.EntityFrameworkCore;
using ekaterina_anisimova_kt_42_22_April_25.Models; 

namespace ekaterina_anisimova_kt_42_22_April_25.Database
{
    public class TeacherDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Load> Loads { get; set; } 

        public TeacherDbContext(DbContextOptions<TeacherDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация для Teacher
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MiddleName).HasMaxLength(50);

                // Связь с Department (кафедрой)
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Teachers)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Связь с AcademicDegree (ученой степенью)
                entity.HasOne(e => e.AcademicDegree)
                      .WithMany(ad => ad.Teachers)
                      .HasForeignKey(e => e.AcademicDegreeId)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.SetNull);

                // Связь с Position (должностью)
                entity.HasOne(e => e.Position)
                      .WithMany(p => p.Teachers)
                      .HasForeignKey(e => e.PositionId)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.SetNull); 
            });

            // Конфигурация для Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Конфигурация для AcademicDegree
            modelBuilder.Entity<AcademicDegree>(entity =>
            {
                entity.HasKey(e => e.AcademicDegreeId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Конфигурация для Position
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Конфигурация для Discipline
            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.HasKey(e => e.DisciplineId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Конфигурация для Load
            modelBuilder.Entity<Load>(entity =>
            {
                entity.HasKey(e => e.LoadId);
                entity.Property(e => e.Hours).IsRequired();

                // Связь с Teacher
                entity.HasOne(e => e.Teacher)
                      .WithMany(t => t.Loads)
                      .HasForeignKey(e => e.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade); // Если удалить преподавателя, удалить его нагрузки

                // Связь с Discipline
                entity.HasOne(e => e.Discipline)
                      .WithMany(d => d.Loads)
                      .HasForeignKey(e => e.DisciplineId)
                      .OnDelete(DeleteBehavior.Cascade); // Если удалить дисциплину, удалить связанные нагрузки
            });
        }
    }
}