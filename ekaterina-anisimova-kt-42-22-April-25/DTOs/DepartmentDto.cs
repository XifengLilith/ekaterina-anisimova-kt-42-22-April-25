namespace ekaterina_anisimova_kt_42_22_April_25.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public int? HeadOfDepartmentId { get; set; }
        public string? HeadOfDepartmentFirstName { get; set; }
        public string? HeadOfDepartmentLastName { get; set; }
        public int TeachersCount { get; set; }
    }
}
