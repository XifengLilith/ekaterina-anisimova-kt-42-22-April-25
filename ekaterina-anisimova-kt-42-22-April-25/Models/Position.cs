using System.ComponentModel.DataAnnotations;

namespace ekaterina_anisimova_kt_42_22_April_25.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}
