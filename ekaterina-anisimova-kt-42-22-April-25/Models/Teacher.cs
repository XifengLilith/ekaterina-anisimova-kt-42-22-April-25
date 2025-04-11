using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ekaterina_anisimova_kt_42_22_April_25.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [ForeignKey("AcademicDegree")]
        public int AcademicDegreeId { get; set; }

        public AcademicDegree AcademicDegree { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }

        public Position Position { get; set; }

        public ICollection<Load> Loads { get; set; }
    }
}
