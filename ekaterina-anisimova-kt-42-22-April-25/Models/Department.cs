using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ekaterina_anisimova_kt_42_22_April_25.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public int FoundationYear { get; set; }

        [ForeignKey("HeadOfDepartment")]
        public int? HeadOfDepartmentId { get; set; }

        public Teacher HeadOfDepartment { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}