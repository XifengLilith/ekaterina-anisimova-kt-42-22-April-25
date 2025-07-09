using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ekaterina_anisimova_kt_42_22_April_25.Models
{
    public class Load
    {
        [Key]
        public int LoadId { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; } 

        [ForeignKey("Discipline")]
        public int DisciplineId { get; set; }

        public Discipline? Discipline { get; set; } 

        [Required]
        [Range(1, 1000)] // Ограничение на разумное количество часов
        public int Hours { get; set; }
    }
}