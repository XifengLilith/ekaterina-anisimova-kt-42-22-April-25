using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using ekaterina_anisimova_kt_42_22_April_25.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly TeacherDbContext _dbContext;

        public DisciplineService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DisciplineDto>> GetAllDisciplinesAsync()
        {
            return await _dbContext.Disciplines
                .Select(d => new DisciplineDto
                {
                    DisciplineId = d.DisciplineId,
                    Name = d.Name
                })
                .ToListAsync();
        }

        public async Task<DisciplineDto?> GetDisciplineByIdAsync(int id)
        {
            return await _dbContext.Disciplines
                .Where(d => d.DisciplineId == id)
                .Select(d => new DisciplineDto
                {
                    DisciplineId = d.DisciplineId,
                    Name = d.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DisciplineDto> AddDisciplineAsync(Discipline discipline)
        {
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync();

            return new DisciplineDto
            {
                DisciplineId = discipline.DisciplineId,
                Name = discipline.Name
            };
        }

        public async Task<DisciplineDto?> UpdateDisciplineAsync(Discipline discipline)
        {
            var existingDiscipline = await _dbContext.Disciplines.FindAsync(discipline.DisciplineId);

            if (existingDiscipline == null)
            {
                return null;
            }

            existingDiscipline.Name = discipline.Name;

            await _dbContext.SaveChangesAsync();

            return new DisciplineDto
            {
                DisciplineId = existingDiscipline.DisciplineId,
                Name = existingDiscipline.Name
            };
        }

        public async Task<bool> DeleteDisciplineAsync(int id)
        {
            var discipline = await _dbContext.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return false;
            }

            _dbContext.Disciplines.Remove(discipline);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DisciplineDto>> GetFilteredDisciplinesAsync(DisciplineFilterDto filter)
        {
            var query = _dbContext.Disciplines.AsQueryable();

            if (filter.TeacherId.HasValue)
            {
                query = query.Where(d => _dbContext.Loads.Any(l => l.DisciplineId == d.DisciplineId && l.TeacherId == filter.TeacherId.Value));
            }

            if (filter.MinHours.HasValue || filter.MaxHours.HasValue)
            {
                query = query.Where(d => _dbContext.Loads.Any(l =>
                    l.DisciplineId == d.DisciplineId &&
                    (!filter.MinHours.HasValue || l.Hours >= filter.MinHours.Value) &&
                    (!filter.MaxHours.HasValue || l.Hours <= filter.MaxHours.Value)
                ));
            }

            return await query.Select(d => new DisciplineDto
            {
                DisciplineId = d.DisciplineId,
                Name = d.Name
            }).ToListAsync();
        }
    }
}