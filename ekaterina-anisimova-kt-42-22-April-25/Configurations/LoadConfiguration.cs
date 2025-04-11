using ekaterina_anisimova_kt_42_22_April_25.Database.Helpers;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ekaterina_anisimova_kt_42_22_April_25.Database.Configurations
{
    public class LoadConfiguration : IEntityTypeConfiguration<Load>
    {
        private const string TableName = "cd_load";

        public void Configure(EntityTypeBuilder<Load> builder)
        {
            builder
                .HasKey(p => p.LoadId)
                .HasName($"pk_{TableName}_load_id");

            builder.Property(p => p.LoadId)
                .ValueGeneratedOnAdd()
                .HasColumnName("load_id")
                .HasComment("Идентификатор нагрузки");

            builder.Property(p => p.TeacherId)
                .IsRequired()
                .HasColumnName("f_teacher_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор преподавателя");

            builder.Property(p => p.DisciplineId)
                .IsRequired()
                .HasColumnName("f_discipline_id")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор дисциплины");

            builder.Property(p => p.Hours)
                .IsRequired()
                .HasColumnName("n_hours")
                .HasColumnType(ColumnType.Int)
                .HasComment("Количество часов нагрузки");

            builder.ToTable(TableName);

            builder.HasOne(p => p.Teacher)
                .WithMany(t => t.Loads)
                .HasForeignKey(p => p.TeacherId)
                .HasConstraintName("fk_f_teacher_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Discipline)
                .WithMany(d => d.Loads)
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.TeacherId, $"idx_{TableName}_fk_f_teacher_id");
            builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");
        }
    }
}