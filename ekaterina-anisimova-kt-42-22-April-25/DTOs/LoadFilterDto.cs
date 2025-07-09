namespace ekaterina_anisimova_kt_42_22_April_25.DTOs
{
    public class LoadFilterDto
    {
        public int? TeacherId { get; set; } 
        public int? DisciplineId { get; set; } 
        public int? DepartmentId { get; set; } 
        public int? MinHours { get; set; } 
        public int? MaxHours { get; set; } 
    }
}