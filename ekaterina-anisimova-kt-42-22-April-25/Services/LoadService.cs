using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using ekaterina_anisimova_kt_42_22_April_25.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System; // Добавить для ArgumentException

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public class LoadService : ILoadService
    {
        private readonly TeacherDbContext _dbContext;

        public LoadService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LoadDto>> GetAllLoadsAsync()
        {
            return await _dbContext.Loads
                .Include(l => l.Teacher)
                    .ThenInclude(t => t.Department) // Включаем кафедру преподавателя
                .Include(l => l.Discipline)
                .Select(l => new LoadDto
                {
                    LoadId = l.LoadId,
                    TeacherId = l.TeacherId,
                    TeacherFirstName = l.Teacher != null ? l.Teacher.FirstName : "N/A",
                    TeacherLastName = l.Teacher != null ? l.Teacher.LastName : "N/A",
                    DisciplineId = l.DisciplineId,
                    DisciplineName = l.Discipline != null ? l.Discipline.Name : "N/A",
                    Hours = l.Hours
                })
                .ToListAsync();
        }

        public async Task<LoadDto?> GetLoadByIdAsync(int id)
        {
            return await _dbContext.Loads
                .Include(l => l.Teacher)
                    .ThenInclude(t => t.Department) // Включаем кафедру преподавателя
                .Include(l => l.Discipline)
                .Where(l => l.LoadId == id)
                .Select(l => new LoadDto
                {
                    LoadId = l.LoadId,
                    TeacherId = l.TeacherId,
                    TeacherFirstName = l.Teacher != null ? l.Teacher.FirstName : "N/A",
                    TeacherLastName = l.Teacher != null ? l.Teacher.LastName : "N/A",
                    DisciplineId = l.DisciplineId,
                    DisciplineName = l.Discipline != null ? l.Discipline.Name : "N/A",
                    Hours = l.Hours
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LoadDto> AddLoadAsync(Load load)
        {
            // Проверяем существование TeacherId и DisciplineId перед добавлением
            var teacherExists = await _dbContext.Teachers.AnyAsync(t => t.TeacherId == load.TeacherId);
            var disciplineExists = await _dbContext.Disciplines.AnyAsync(d => d.DisciplineId == load.DisciplineId);

            if (!teacherExists)
            {
                throw new ArgumentException($"Преподаватель с ID {load.TeacherId} не найден.");
            }
            if (!disciplineExists)
            {
                throw new ArgumentException($"Дисциплина с ID {load.DisciplineId} не найдена.");
            }

            _dbContext.Loads.Add(load);
            await _dbContext.SaveChangesAsync();

            // Для корректного возврата DTO после сохранения, нужно снова загрузить связанные сущности,
            // так как EF Core по умолчанию не загружает их после Add/SaveChanges.
            // Или же можно вручную заполнить DTO, зная, что Teacher и Discipline гарантированно существуют.
            // В этом случае, так как мы только что проверили их существование,
            // можем предположить, что можем их получить для DTO.

            var addedLoadDto = new LoadDto
            {
                LoadId = load.LoadId,
                TeacherId = load.TeacherId,
                DisciplineId = load.DisciplineId,
                Hours = load.Hours
            };

            // Загружаем имена для DTO
            var teacher = await _dbContext.Teachers.FindAsync(load.TeacherId);
            var discipline = await _dbContext.Disciplines.FindAsync(load.DisciplineId);

            addedLoadDto.TeacherFirstName = teacher?.FirstName ?? "N/A";
            addedLoadDto.TeacherLastName = teacher?.LastName ?? "N/A";
            addedLoadDto.DisciplineName = discipline?.Name ?? "N/A";

            return addedLoadDto;
        }


        public async Task<LoadDto?> UpdateLoadAsync(Load load)
        {
            var existingLoad = await _dbContext.Loads.FindAsync(load.LoadId);

            if (existingLoad == null)
            {
                return null; // Нагрузка не найдена
            }

            // Проверяем существование TeacherId и DisciplineId перед обновлением, если они меняются
            if (existingLoad.TeacherId != load.TeacherId)
            {
                var teacherExists = await _dbContext.Teachers.AnyAsync(t => t.TeacherId == load.TeacherId);
                if (!teacherExists)
                {
                    throw new ArgumentException($"Преподаватель с ID {load.TeacherId} не найден.");
                }
            }
            if (existingLoad.DisciplineId != load.DisciplineId)
            {
                var disciplineExists = await _dbContext.Disciplines.AnyAsync(d => d.DisciplineId == load.DisciplineId);
                if (!disciplineExists)
                {
                    throw new ArgumentException($"Дисциплина с ID {load.DisciplineId} не найдена.");
                }
            }

            existingLoad.TeacherId = load.TeacherId;
            existingLoad.DisciplineId = load.DisciplineId;
            existingLoad.Hours = load.Hours;

            await _dbContext.SaveChangesAsync();

            // Для возврата актуального DTO, необходимо загрузить связанные сущности
            var updatedLoad = await _dbContext.Loads
                .Include(l => l.Teacher)
                    .ThenInclude(t => t.Department) // Включаем кафедру преподавателя
                .Include(l => l.Discipline)
                .FirstOrDefaultAsync(l => l.LoadId == existingLoad.LoadId);

            if (updatedLoad == null) return null; // На всякий случай

            return new LoadDto
            {
                LoadId = updatedLoad.LoadId,
                TeacherId = updatedLoad.TeacherId,
                TeacherFirstName = updatedLoad.Teacher != null ? updatedLoad.Teacher.FirstName : "N/A",
                TeacherLastName = updatedLoad.Teacher != null ? updatedLoad.Teacher.LastName : "N/A",
                DisciplineId = updatedLoad.DisciplineId,
                DisciplineName = updatedLoad.Discipline != null ? updatedLoad.Discipline.Name : "N/A",
                Hours = updatedLoad.Hours
            };
        }

        public async Task<bool> DeleteLoadAsync(int id)
        {
            var load = await _dbContext.Loads.FindAsync(id);
            if (load == null)
            {
                return false; // Нагрузка не найдена
            }

            _dbContext.Loads.Remove(load);
            await _dbContext.SaveChangesAsync();
            return true; // Успешно удалено
        }

        // <-- ДОБАВЛЕН НОВЫЙ МЕТОД ФИЛЬТРАЦИИ
        public async Task<IEnumerable<LoadDto>> GetFilteredLoadsAsync(LoadFilterDto filter)
        {
            var query = _dbContext.Loads
                .Include(l => l.Teacher)
                    .ThenInclude(t => t.Department) // Включаем кафедру преподавателя
                .Include(l => l.Discipline)
                .AsQueryable(); // Начинаем построение запроса

            if (filter.TeacherId.HasValue)
            {
                query = query.Where(l => l.TeacherId == filter.TeacherId.Value);
            }

            if (filter.DisciplineId.HasValue)
            {
                query = query.Where(l => l.DisciplineId == filter.DisciplineId.Value);
            }

            // Фильтрация по DepartmentId требует перехода через Teacher
            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(l => l.Teacher != null && l.Teacher.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.MinHours.HasValue)
            {
                query = query.Where(l => l.Hours >= filter.MinHours.Value);
            }

            if (filter.MaxHours.HasValue)
            {
                query = query.Where(l => l.Hours <= filter.MaxHours.Value);
            }

            return await query.Select(l => new LoadDto
            {
                LoadId = l.LoadId,
                TeacherId = l.TeacherId,
                TeacherFirstName = l.Teacher != null ? l.Teacher.FirstName : "N/A",
                TeacherLastName = l.Teacher != null ? l.Teacher.LastName : "N/A",
                DisciplineId = l.DisciplineId,
                DisciplineName = l.Discipline != null ? l.Discipline.Name : "N/A",
                Hours = l.Hours
            }).ToListAsync();
        }
    }
}