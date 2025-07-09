using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ekaterina_anisimova_kt_42_22_April_25.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [ForeignKey("AcademicDegree")]
        public int? AcademicDegreeId { get; set; }
        public AcademicDegree? AcademicDegree { get; set; }

        [ForeignKey("Position")]
        public int? PositionId { get; set; } 
        public Position? Position { get; set; }

        public ICollection<Load> Loads { get; set; } = new List<Load>();
    }
}