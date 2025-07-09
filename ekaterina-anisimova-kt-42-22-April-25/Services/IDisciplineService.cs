using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public interface IDisciplineService
    {
        Task<IEnumerable<DisciplineDto>> GetAllDisciplinesAsync();
        Task<DisciplineDto?> GetDisciplineByIdAsync(int id);
        Task<DisciplineDto> AddDisciplineAsync(Discipline discipline);
        Task<DisciplineDto?> UpdateDisciplineAsync(Discipline discipline);
        Task<bool> DeleteDisciplineAsync(int id);
        Task<IEnumerable<DisciplineDto>> GetFilteredDisciplinesAsync(DisciplineFilterDto filter);
    }
}