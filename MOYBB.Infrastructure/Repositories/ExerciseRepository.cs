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

        public async Task<IEnumerable<Exercise>> GetExercisesByMuscleAsync(Guid muscleId)
        {
            return await _context.MuscleInExercises
                .Where(mie => mie.MuscleId == muscleId)
                .Select(mie => mie.Exercise)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> SearchExercisesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await _dbSet
                .Where(e => e.Name.Contains(searchTerm) || 
                           (e.Description != null && e.Description.Contains(searchTerm)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetExercisesByMainMuscleAsync(string mainMuscle)
        {
            if (string.IsNullOrWhiteSpace(mainMuscle))
                return await GetAllAsync();

            return await _dbSet
                .Where(e => e.MainMuscle != null && e.MainMuscle.ToLower().Contains(mainMuscle.ToLower()))
                .ToListAsync();
        }
    }
} 