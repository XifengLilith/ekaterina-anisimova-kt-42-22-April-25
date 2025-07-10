using Microsoft.EntityFrameworkCore;
using System;
using ekaterina_anisimova_kt_42_22_April_25.Database; // <-- Убедитесь, что это ваш корректный namespace к TeacherDbContext

namespace ekaterina_anisimova_kt_42_22_April_25.Tests.IntegrationTests
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        protected readonly DbContextOptions<TeacherDbContext> _dbContextOptions;

        protected BaseIntegrationTest()
        {
            // Настройка In-Memory базы данных для каждого теста
            // Используем уникальное имя базы данных для каждого теста,
            // чтобы они не мешали друг другу.
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<TeacherDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            // Создаем контекст и убеждаемся, что база данных создана
            // Это гарантирует, что схемы таблиц будут созданы в памяти
            using (var context = new TeacherDbContext(_dbContextOptions))
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            // Очистка ресурсов после каждого теста (удаление In-Memory базы данных)
            using (var context = new TeacherDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted(); // Удаляем базу данных в памяти
            }
        }
    }
}