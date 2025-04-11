using ekaterina_anisimova_kt_42_22_April_25.Database.Helpers;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ekaterina_anisimova_kt_42_22_April_25.Database.Configurations
{
    public class AcademicDegreeConfiguration : IEntityTypeConfiguration<AcademicDegree>
    {
        private const string TableName = "cd_academic_degree";

        public void Configure(EntityTypeBuilder<AcademicDegree> builder)
        {
            builder
                .HasKey(p => p.AcademicDegreeId)
                .HasName($"pk_{TableName}_academic_degree_id");

            builder.Property(p => p.AcademicDegreeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("academic_degree_id")
                .HasComment("Идентификатор ученой степени");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_academic_degree_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название ученой степени");

            builder.ToTable(TableName);
        }
    }
}