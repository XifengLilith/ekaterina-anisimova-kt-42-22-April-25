using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Filters;
using ekaterina_anisimova_kt_42_22_April_25.Models;

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetFilteredTeachersAsync(TeacherFilter filter);

        Task<TeacherDto?> GetTeacherByIdAsync(int id); 
        Task<TeacherDto> AddTeacherAsync(Teacher teacher); 
        Task<TeacherDto?> UpdateTeacherAsync(Teacher teacher); 
        Task<bool> DeleteTeacherAsync(int id);
    }
}