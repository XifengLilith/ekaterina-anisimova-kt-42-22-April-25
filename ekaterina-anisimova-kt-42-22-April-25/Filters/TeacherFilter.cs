namespace ekaterina_anisimova_kt_42_22_April_25.Filters
{
    public class TeacherFilter
    {
        // Фильтрация по названию кафедры
        public string? DepartmentName { get; set; }
        // Фильтрация по названию ученой степени
        public string? AcademicDegreeName { get; set; }
        // Фильтрация по названию должности
        public string? PositionName { get; set; }
    }
}