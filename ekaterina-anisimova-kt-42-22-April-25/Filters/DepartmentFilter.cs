namespace ekaterina_anisimova_kt_42_22_April_25.Filters
{
    public class DepartmentFilter
    {
        // Фильтрация по дате основания: От (год)
        public int? MinFoundationYear { get; set; }
        // Фильтрация по дате основания: До (год)
        public int? MaxFoundationYear { get; set; }
        // Фильтрация по количеству преподавателей: Минимум
        public int? MinTeachersCount { get; set; }
        // Фильтрация по количеству преподавателей: Максимум
        public int? MaxTeachersCount { get; set; }
    }
}