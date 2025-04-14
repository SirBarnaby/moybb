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
    public class MuscleRepository : BaseRepository<Muscle>, IMuscleRepository
    {
        public MuscleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Muscle>> GetMusclesByExerciseAsync(int exerciseId)
        {
            return await _context.MuscleInExercises
                .Where(mie => mie.ExerciseId == exerciseId)
                .Select(mie => mie.Muscle)
                .ToListAsync();
        }

        public async Task<IEnumerable<Muscle>> SearchMusclesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            searchTerm = searchTerm.ToLower();
            return await _dbSet
                .Where(m => m.Name.ToLower().Contains(searchTerm) || 
                           (m.NameLatin != null && m.NameLatin.ToLower().Contains(searchTerm)) ||
                           (m.Description != null && m.Description.ToLower().Contains(searchTerm)))
                .ToListAsync();
        }
    }
} 