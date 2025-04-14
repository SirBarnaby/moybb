using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOYBB.Core.Interfaces;
using MOYBB.Core.Models;
using MOYBB.Infrastructure.Data;

namespace MOYBB.Infrastructure.Repositories
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.MuscleInExercises)
                .ToListAsync();
        }

        public override async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(e => e.MuscleInExercises)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetExercisesByMuscleAsync(int muscleId)
        {
            return await _context.MuscleInExercises
                .Include(mie => mie.Exercise)
                .ThenInclude(e => e.MuscleInExercises)
                .Where(mie => mie.MuscleId == muscleId)
                .Select(mie => mie.Exercise)
                .OrderByDescending(e => e.Popularity)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> SearchExercisesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await _dbSet
                .Include(e => e.MuscleInExercises)
                .Where(e => e.Name.ToLower().Contains(searchTerm.ToLower())) // Lowercase check
                .OrderByDescending(e => e.Popularity)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetExercisesByMainMuscleAsync(string mainMuscle)
        {
            if (string.IsNullOrWhiteSpace(mainMuscle))
                return await GetAllAsync();

            return await _dbSet
                .Include(e => e.MuscleInExercises)
                .Where(e => e.MainMuscle != null && e.MainMuscle.ToLower().Contains(mainMuscle.ToLower()))
                .OrderByDescending(e => e.Popularity)
                .ToListAsync();
        }
    }
} 