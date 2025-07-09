using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using ekaterina_anisimova_kt_42_22_April_25.Filters; 

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetFilteredDepartmentsAsync(DepartmentFilter filter);

        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> AddDepartmentAsync(Department department);
        Task<DepartmentDto?> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}