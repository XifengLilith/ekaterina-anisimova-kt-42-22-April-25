using ekaterina_anisimova_kt_42_22_April_25.Database.Helpers;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ekaterina_anisimova_kt_42_22_April_25.Database.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        private const string TableName = "cd_teacher";

        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder
                .HasKey(p => p.TeacherId)
                .HasName($"pk_{TableName}_teacher_id");

            builder.Property(p => p.TeacherId)
                .ValueGeneratedOnAdd()
                .HasColumnName("teacher_id")
                .HasComment("Идентификатор преподавателя");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("c_teacher_firstname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Имя преподавателя");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName("c_teacher_lastname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Фамилия преподавателя");

            builder.Property(p => p.MiddleName)
                .HasColumnName("c_teacher_middlename")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Отчество преподавателя");

            builder.Property(p => p.DepartmentId)
                .IsRequired()
                .HasColumnName("f_department_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор кафедры");

            builder.Property(p => p.AcademicDegreeId)
                .IsRequired()
                .HasColumnName("f_academic_degree_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор ученой степени");

            builder.Property(p => p.PositionId)
                .IsRequired()
                .HasColumnName("f_position_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор должности");

            builder.ToTable(TableName);

            builder.HasOne(p => p.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(p => p.DepartmentId)
                .HasConstraintName("fk_f_department_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.AcademicDegree)
                .WithMany(ad => ad.Teachers)
                .HasForeignKey(p => p.AcademicDegreeId)
                .HasConstraintName("fk_f_academic_degree_id")
                .OnDelete(DeleteBehavior.Restrict); // Нельзя удалить ученую степень, если есть преподаватели с этой степенью

            builder.HasOne(p => p.Position)
                .WithMany(pos => pos.Teachers)
                .HasForeignKey(p => p.PositionId)
                .HasConstraintName("fk_f_position_id")
                .OnDelete(DeleteBehavior.Restrict); // Нельзя удалить должность, если есть преподаватели на этой должности

            builder.HasIndex(p => p.DepartmentId, $"idx_{TableName}_fk_f_department_id");
            builder.HasIndex(p => p.AcademicDegreeId, $"idx_{TableName}_fk_f_academic_degree_id");
            builder.HasIndex(p => p.PositionId, $"idx_{TableName}_fk_f_position_id");
        }
    }
}