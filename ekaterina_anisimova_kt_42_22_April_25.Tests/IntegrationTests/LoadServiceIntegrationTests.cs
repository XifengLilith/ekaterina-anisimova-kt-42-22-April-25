using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ekaterina_anisimova_kt_42_22_April_25.Database; // Ваш DbContext
using ekaterina_anisimova_kt_42_22_April_25.Models; // Ваши модели
using ekaterina_anisimova_kt_42_22_April_25.Services; // Ваш LoadService
using System.Collections.Generic; // Для List

namespace ekaterina_anisimova_kt_42_22_April_25.Tests.IntegrationTests
{
    public class LoadServiceIntegrationTests : BaseIntegrationTest
    {
        // Тест: Проверка успешного добавления нагрузки
        [Fact]
        public async Task AddLoadAsync_AddsNewLoadSuccessfully()
        {
            // Arrange (Подготовка данных)
            using var context = new TeacherDbContext(_dbContextOptions);
            var loadService = new LoadService(context);

            // Добавляем необходимые связанные сущности, так как у Load есть внешние ключи
            var department = new Department { Name = "Тестовая Кафедра" }; // Уже исправлено
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            var teacher = new Teacher
            {
                FirstName = "Тест",
                LastName = "Преподаватель",
                DateOfBirth = new DateTime(1980, 1, 1),
                DepartmentId = department.DepartmentId
            };
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();

            var discipline = new Discipline { Name = "Тестовая Дисциплина" }; // <-- ИСПРАВЛЕНО ЗДЕСЬ
            await context.Disciplines.AddAsync(discipline);
            await context.SaveChangesAsync();

            var newLoad = new Load
            {
                TeacherId = teacher.TeacherId,
                DisciplineId = discipline.DisciplineId,
                Hours = 120
            };

            // Act
            var addedLoad = await loadService.AddLoadAsync(newLoad);

            // Assert
            Assert.NotNull(addedLoad);
            Assert.True(addedLoad.LoadId > 0);
            Assert.Equal(newLoad.Hours, addedLoad.Hours);
            Assert.Equal(newLoad.TeacherId, addedLoad.TeacherId);
            Assert.Equal(newLoad.DisciplineId, addedLoad.DisciplineId);

            var loadInDb = await context.Loads.FindAsync(addedLoad.LoadId);
            Assert.NotNull(loadInDb);
            Assert.Equal(newLoad.Hours, loadInDb.Hours);
        }

        // Тест: Проверка получения всех нагрузок
        [Fact]
        public async Task GetAllLoadsAsync_ReturnsAllLoads()
        {
            // Arrange
            using var context = new TeacherDbContext(_dbContextOptions);
            var loadService = new LoadService(context);

            // Добавляем необходимые связанные сущности
            var department = new Department { Name = "Тест Кафедра 2" }; // Уже исправлено
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            var teacher1 = new Teacher { FirstName = "Т2", LastName = "П2", DateOfBirth = new DateTime(1985, 5, 10), DepartmentId = department.DepartmentId };
            var teacher2 = new Teacher { FirstName = "Т3", LastName = "П3", DateOfBirth = new DateTime(1990, 8, 15), DepartmentId = department.DepartmentId };
            await context.Teachers.AddRangeAsync(teacher1, teacher2);
            await context.SaveChangesAsync();

            var discipline1 = new Discipline { Name = "Тест Дисциплина А" }; 
            var discipline2 = new Discipline { Name = "Тест Дисциплина Б" };
            await context.Disciplines.AddRangeAsync(discipline1, discipline2);
            await context.SaveChangesAsync();

            var loads = new List<Load>
            {
                new Load { TeacherId = teacher1.TeacherId, DisciplineId = discipline1.DisciplineId, Hours = 90 },
                new Load { TeacherId = teacher2.TeacherId, DisciplineId = discipline2.DisciplineId, Hours = 60 }
            };
            await context.Loads.AddRangeAsync(loads);
            await context.SaveChangesAsync();

            // Act
            var resultLoads = await loadService.GetAllLoadsAsync();

            // Assert
            Assert.NotNull(resultLoads);
            Assert.Equal(2, resultLoads.Count());
        }
    }
}