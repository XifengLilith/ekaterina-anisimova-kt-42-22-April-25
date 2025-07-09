using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public interface ILoadService
    {
        Task<IEnumerable<LoadDto>> GetAllLoadsAsync();
        Task<LoadDto?> GetLoadByIdAsync(int id);
        Task<LoadDto> AddLoadAsync(Load load);
        Task<LoadDto?> UpdateLoadAsync(Load load);
        Task<bool> DeleteLoadAsync(int id);
        Task<IEnumerable<LoadDto>> GetFilteredLoadsAsync(LoadFilterDto filter);
    }
}