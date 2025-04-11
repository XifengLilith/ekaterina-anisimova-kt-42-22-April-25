using System.ComponentModel.DataAnnotations;

namespace ekaterina_anisimova_kt_42_22_April_25.Models
{
    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Load> Loads { get; set; }
    }
}
