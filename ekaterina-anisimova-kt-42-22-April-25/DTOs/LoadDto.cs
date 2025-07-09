namespace ekaterina_anisimova_kt_42_22_April_25.DTOs
{
    public class LoadDto
    {
        public int LoadId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; } 
        public string TeacherLastName { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }   
        public int Hours { get; set; }
    }
}