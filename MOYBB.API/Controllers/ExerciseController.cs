using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOYBB.Core.Interfaces;
using MOYBB.Core.Mapping;
using MOYBB.Core.Models.DTOs;

namespace MOYBB.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseController(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
        {
            var exercises = await _exerciseRepository.GetAllAsync();
            return Ok(exercises.Select(e => e.ToDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetById(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id);
            if (exercise == null)
                return NotFound();

            return Ok(exercise.ToDto());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> Search([FromQuery] string term)
        {
            var exercises = await _exerciseRepository.SearchExercisesAsync(term);
            return Ok(exercises.Select(e => e.ToDto()));
        }

        [HttpGet("muscle/{muscleId}")]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetByMuscle(int muscleId)
        {
            var exercises = await _exerciseRepository.GetExercisesByMuscleAsync(muscleId);
            return Ok(exercises.Select(e => e.ToDto()));
        }

        [HttpGet("main-muscle/{mainMuscle}")]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetByMainMuscle(string mainMuscle)
        {
            var exercises = await _exerciseRepository.GetExercisesByMainMuscleAsync(mainMuscle);
            return Ok(exercises.Select(e => e.ToDto()));
        }
    }
} 