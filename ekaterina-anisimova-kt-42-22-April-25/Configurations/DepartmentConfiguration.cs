using ekaterina_anisimova_kt_42_22_April_25.Database.Helpers;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ekaterina_anisimova_kt_42_22_April_25.Database.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        private const string TableName = "cd_department";

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .HasKey(p => p.DepartmentId)
                .HasName($"pk_{TableName}_department_id");

            builder.Property(p => p.DepartmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("department_id")
                .HasComment("Идентификатор кафедры");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_department_name")
                .HasColumnType(ColumnType.String).HasMaxLength(200)
                .HasComment("Название кафедры");

            builder.Property(p => p.FoundationDate)
                .HasColumnName("d_foundation_date")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата основания кафедры");

            builder.Property(p => p.HeadOfDepartmentId)
                .HasColumnName("f_head_of_department_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор заведующего кафедрой");

            builder.ToTable(TableName);

            builder.HasOne(p => p.HeadOfDepartment)
                .WithOne() // Одна кафедра имеет одного заведующего
                .HasForeignKey<Department>(p => p.HeadOfDepartmentId)
                .HasConstraintName("fk_f_head_of_department_id")
                .OnDelete(DeleteBehavior.SetNull); // При удалении преподавателя, который является заведующим, связь обнуляется

            builder.HasMany(p => p.Teachers)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId)
                .HasConstraintName("fk_f_department_id")
                .OnDelete(DeleteBehavior.Cascade); // При удалении кафедры удаляются все преподаватели кафедры

            builder.HasIndex(p => p.HeadOfDepartmentId, $"idx_{TableName}_fk_f_head_of_department_id");
        }
    }
}