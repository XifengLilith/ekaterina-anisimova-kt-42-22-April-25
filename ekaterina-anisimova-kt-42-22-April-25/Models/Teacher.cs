using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System; // Добавлено для DateTime

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

        [Required] // Предполагаем, что дата рождения обязательна
        public DateTime DateOfBirth { get; set; }

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

        public bool IsWorkingAge()
        {
            // Вычисляем возраст на текущую дату
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age >= 22 && age <= 65; // Примерный трудоспособный возраст
        }
    }
}