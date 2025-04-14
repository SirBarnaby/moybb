using System.Collections.Generic;
using System.Threading.Tasks;
using MOYBB.Core.Models;

namespace MOYBB.Core.Interfaces
{
    public interface IMuscleRepository : IRepository<Muscle>
    {
        Task<IEnumerable<Muscle>> GetMusclesByExerciseAsync(int exerciseId);
        Task<IEnumerable<Muscle>> SearchMusclesAsync(string searchTerm);
    }
} 