using System;
using System.Linq;
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
    public class MuscleRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly MuscleRepository _repository;
        private readonly Mock<ILogger<ApplicationDbContext>> _mockLogger;

        public MuscleRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockLogger = new Mock<ILogger<ApplicationDbContext>>();
            _context = new ApplicationDbContext(options, _mockLogger.Object);
            _repository = new MuscleRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMuscle_WhenMuscleExists()
        {
            // Arrange
            var muscle = new Muscle
            {
                Id = Guid.NewGuid(),
                Name = "Test Muscle",
                Description = "Test Description"
            };
            await _context.Muscles.AddAsync(muscle);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(muscle.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(muscle.Id, result.Id);
            Assert.Equal(muscle.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenMuscleDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SearchMusclesAsync_ReturnsMuscles_WhenSearchTermMatches()
        {
            // Arrange
            var muscle1 = new Muscle { Id = Guid.NewGuid(), Name = "Biceps", Description = "Arm muscle" };
            var muscle2 = new Muscle { Id = Guid.NewGuid(), Name = "Triceps", Description = "Arm muscle" };
            var muscle3 = new Muscle { Id = Guid.NewGuid(), Name = "Quadriceps", Description = "Leg muscle" };

            await _context.Muscles.AddRangeAsync(muscle1, muscle2, muscle3);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.SearchMusclesAsync("arm");

            // Assert
            var resultList = result.ToList(); // Materialize the query
            Assert.Equal(2, resultList.Count);
            Assert.Contains(resultList, m => m.Name == "Biceps");
            Assert.Contains(resultList, m => m.Name == "Triceps");
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
} 