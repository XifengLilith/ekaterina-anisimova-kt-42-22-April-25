namespace ekaterina_anisimova_kt_42_22_April_25.DTOs
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; } 
        public int? DepartmentId { get; set; } 
        public string? DepartmentName { get; set; } 

        public int? AcademicDegreeId { get; set; } 
        public string? AcademicDegreeName { get; set; } 

        public int? PositionId { get; set; } 
        public string? PositionName { get; set; } 
    }
}