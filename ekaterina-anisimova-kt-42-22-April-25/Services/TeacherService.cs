using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Filters;
using ekaterina_anisimova_kt_42_22_April_25.Database;
using ekaterina_anisimova_kt_42_22_April_25.Models; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly TeacherDbContext _dbContext;

        public TeacherService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TeacherDto>> GetFilteredTeachersAsync(TeacherFilter filter)
        {
            var query = _dbContext.Teachers.AsQueryable();

            query = query
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Position);

            if (!string.IsNullOrEmpty(filter.DepartmentName))
            {
                query = query.Where(t => t.Department != null && t.Department.Name.ToLower().Contains(filter.DepartmentName.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(filter.AcademicDegreeName))
            {
                query = query.Where(t => t.AcademicDegree != null && t.AcademicDegree.Name.ToLower().Contains(filter.AcademicDegreeName.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(filter.PositionName))
            {
                query = query.Where(t => t.Position != null && t.Position.Name.ToLower().Contains(filter.PositionName.ToLower().Trim()));
            }

            return await query.Select(t => new TeacherDto
            {
                TeacherId = t.TeacherId,
                FirstName = t.FirstName,
                LastName = t.LastName,
                MiddleName = t.MiddleName,
                DepartmentId = t.DepartmentId,
                DepartmentName = t.Department != null ? t.Department.Name : null,
                AcademicDegreeName = t.AcademicDegree != null ? t.AcademicDegree.Name : null,
                PositionName = t.Position != null ? t.Position.Name : null
            }).ToListAsync();
        }
        public async Task<TeacherDto?> GetTeacherByIdAsync(int id)
        {
            return await _dbContext.Teachers
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Position)
                .Where(t => t.TeacherId == id)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MiddleName = t.MiddleName,
                    DepartmentId = t.DepartmentId,
                    DepartmentName = t.Department != null ? t.Department.Name : null,
                    AcademicDegreeName = t.AcademicDegree != null ? t.AcademicDegree.Name : null,
                    PositionName = t.Position != null ? t.Position.Name : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TeacherDto> AddTeacherAsync(Teacher teacher)
        {
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync();

            var newTeacher = await _dbContext.Teachers
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Position)
                .Where(t => t.TeacherId == teacher.TeacherId)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MiddleName = t.MiddleName,
                    DepartmentId = t.DepartmentId,
                    DepartmentName = t.Department != null ? t.Department.Name : null,
                    AcademicDegreeName = t.AcademicDegree != null ? t.AcademicDegree.Name : null,
                    PositionName = t.Position != null ? t.Position.Name : null
                })
                .FirstAsync();

            return newTeacher;
        }

        public async Task<TeacherDto?> UpdateTeacherAsync(Teacher teacher)
        {
            var existingTeacher = await _dbContext.Teachers.FindAsync(teacher.TeacherId);

            if (existingTeacher == null)
            {
                return null;
            }

            existingTeacher.FirstName = teacher.FirstName;
            existingTeacher.LastName = teacher.LastName;
            existingTeacher.MiddleName = teacher.MiddleName;
            existingTeacher.DepartmentId = teacher.DepartmentId;
            existingTeacher.AcademicDegreeId = teacher.AcademicDegreeId;
            existingTeacher.PositionId = teacher.PositionId;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Teachers
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Position)
                .Where(t => t.TeacherId == teacher.TeacherId)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.TeacherId,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MiddleName = t.MiddleName,
                    DepartmentId = t.DepartmentId,
                    DepartmentName = t.Department != null ? t.Department.Name : null,
                    AcademicDegreeName = t.AcademicDegree != null ? t.AcademicDegree.Name : null,
                    PositionName = t.Position != null ? t.Position.Name : null
                })
                .FirstAsync();
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }

            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return true; 
        }
    }
}