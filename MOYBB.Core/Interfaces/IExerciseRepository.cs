using System.Collections.Generic;
using System.Threading.Tasks;
using MOYBB.Core.Models;

namespace MOYBB.Core.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        Task<IEnumerable<Exercise>> GetExercisesByMuscleAsync(int muscleId);
        Task<IEnumerable<Exercise>> SearchExercisesAsync(string searchTerm);
        Task<IEnumerable<Exercise>> GetExercisesByMainMuscleAsync(string mainMuscle);
    }
} 