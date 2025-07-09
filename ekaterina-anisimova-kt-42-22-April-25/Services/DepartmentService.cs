using ekaterina_anisimova_kt_42_22_April_25.DTOs;
using ekaterina_anisimova_kt_42_22_April_25.Models;
using ekaterina_anisimova_kt_42_22_April_25.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ekaterina_anisimova_kt_42_22_April_25.Filters;

namespace ekaterina_anisimova_kt_42_22_April_25.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly TeacherDbContext _dbContext;

        public DepartmentService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DepartmentDto>> GetFilteredDepartmentsAsync(DepartmentFilter filter)
        {
            var query = _dbContext.Departments.AsQueryable();

            query = query.Include(d => d.HeadOfDepartment);

            if (filter.MinFoundationYear.HasValue)
            {
                query = query.Where(d => d.FoundationYear >= filter.MinFoundationYear.Value);
            }
            if (filter.MaxFoundationYear.HasValue)
            {
                query = query.Where(d => d.FoundationYear <= filter.MaxFoundationYear.Value);
            }

            if (filter.MinTeachersCount.HasValue)
            {
                query = query.Where(d => d.Teachers.Count() >= filter.MinTeachersCount.Value);
            }
            if (filter.MaxTeachersCount.HasValue)
            {
                query = query.Where(d => d.Teachers.Count() <= filter.MaxTeachersCount.Value);
            }

            return await query.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                FoundationYear = d.FoundationYear,
                HeadOfDepartmentId = d.HeadOfDepartmentId,
                HeadOfDepartmentFirstName = d.HeadOfDepartment != null ? d.HeadOfDepartment.FirstName : null,
                HeadOfDepartmentLastName = d.HeadOfDepartment != null ? d.HeadOfDepartment.LastName : null,
                TeachersCount = d.Teachers.Count()
            }).ToListAsync();
        }
        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            return await _dbContext.Departments
                .Include(d => d.HeadOfDepartment)
                .Where(d => d.DepartmentId == id)
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    FoundationYear = d.FoundationYear,
                    HeadOfDepartmentId = d.HeadOfDepartmentId,
                    HeadOfDepartmentFirstName = d.HeadOfDepartment != null ? d.HeadOfDepartment.FirstName : null,
                    HeadOfDepartmentLastName = d.HeadOfDepartment != null ? d.HeadOfDepartment.LastName : null,
                    TeachersCount = d.Teachers.Count()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DepartmentDto> AddDepartmentAsync(Department department)
        {
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();

            var newDepartment = await _dbContext.Departments
                .Include(d => d.HeadOfDepartment)
                .Where(d => d.DepartmentId == department.DepartmentId)
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    FoundationYear = d.FoundationYear,
                    HeadOfDepartmentId = d.HeadOfDepartmentId,
                    HeadOfDepartmentFirstName = d.HeadOfDepartment != null ? d.HeadOfDepartment.FirstName : null,
                    HeadOfDepartmentLastName = d.HeadOfDepartment != null ? d.HeadOfDepartment.LastName : null,
                    TeachersCount = _dbContext.Teachers.Count(t => t.DepartmentId == d.DepartmentId)
                })
                .FirstAsync();

            return newDepartment;
        }

        public async Task<DepartmentDto?> UpdateDepartmentAsync(Department department)
        {
            var existingDepartment = await _dbContext.Departments.FindAsync(department.DepartmentId);

            if (existingDepartment == null)
            {
                return null;
            }

            existingDepartment.Name = department.Name;
            existingDepartment.FoundationYear = department.FoundationYear;
            existingDepartment.HeadOfDepartmentId = department.HeadOfDepartmentId;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Departments
                .Include(d => d.HeadOfDepartment)
                .Where(d => d.DepartmentId == department.DepartmentId)
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    FoundationYear = d.FoundationYear,
                    HeadOfDepartmentId = d.HeadOfDepartmentId,
                    HeadOfDepartmentFirstName = d.HeadOfDepartment != null ? d.HeadOfDepartment.FirstName : null,
                    HeadOfDepartmentLastName = d.HeadOfDepartment != null ? d.HeadOfDepartment.LastName : null,
                    TeachersCount = _dbContext.Teachers.Count(t => t.DepartmentId == d.DepartmentId)
                })
                .FirstAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return false;
            }

            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}