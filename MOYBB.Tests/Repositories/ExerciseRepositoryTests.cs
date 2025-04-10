using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MOYBB.Core.Models;
using MOYBB.Infrastructure.Data;
using MOYBB.Infrastructure.Repositories;
using Xunit;

namespace MOYBB.Tests.Repositories
{
    public class ExerciseRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ExerciseRepository _repository;
        private readonly Mock<ILogger<ApplicationDbContext>> _mockLogger;

        public ExerciseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockLogger = new Mock<ILogger<ApplicationDbContext>>();
            _context = new ApplicationDbContext(options, _mockLogger.Object);
            _repository = new ExerciseRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsExercise_WhenExerciseExists()
        {
            // Arrange
            var exercise = new Exercise
            {
                Id = Guid.NewGuid(),
                Name = "Test Exercise",
                Description = "Test Description"
            };
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(exercise.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(exercise.Id, result.Id);
            Assert.Equal(exercise.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenExerciseDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
} 