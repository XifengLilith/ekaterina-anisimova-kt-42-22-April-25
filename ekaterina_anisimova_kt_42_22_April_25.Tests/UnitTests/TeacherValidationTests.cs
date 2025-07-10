using Xunit;
using ekaterina_anisimova_kt_42_22_April_25.Models; // Добавляем using для доступа к классу Teacher
using System; // Добавляем using для DateTime

namespace ekaterina_anisimova_kt_42_22_April_25.Tests.UnitTests
{
    public class TeacherValidationTests
    {
        // Тест: Преподаватель в трудоспособном возрасте (25 лет)
        [Fact] // Атрибут [Fact] указывает, что это тестовый метод в xUnit
        public void IsWorkingAge_TeacherIs25YearsOld_ReturnsTrue()
        {
            // Arrange (Подготовка данных)
            var teacher = new Teacher
            {
                DateOfBirth = DateTime.Today.AddYears(-25) // Сегодняшняя дата минус 25 лет
            };

            // Act (Выполнение действия)
            var result = teacher.IsWorkingAge();

            // Assert (Проверка результата)
            Assert.True(result); // Ожидаем, что метод вернет true
        }

        // Тест: Преподаватель слишком молод (20 лет)
        [Fact]
        public void IsWorkingAge_TeacherIs20YearsOld_ReturnsFalse()
        {
            // Arrange
            var teacher = new Teacher
            {
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            // Act
            var result = teacher.IsWorkingAge();

            // Assert
            Assert.False(result); // Ожидаем, что метод вернет false
        }

        // Тест: Преподаватель слишком стар (70 лет)
        [Fact]
        public void IsWorkingAge_TeacherIs70YearsOld_ReturnsFalse()
        {
            // Arrange
            var teacher = new Teacher
            {
                DateOfBirth = DateTime.Today.AddYears(-70)
            };

            // Act
            var result = teacher.IsWorkingAge();

            // Assert
            Assert.False(result); // Ожидаем, что метод вернет false
        }
    }
}