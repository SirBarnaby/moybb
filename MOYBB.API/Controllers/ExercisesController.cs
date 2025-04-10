using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOYBB.Core.Interfaces;
using MOYBB.Core.Models;

namespace MOYBB.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ILogger<ExercisesController> _logger;

        public ExercisesController(IExerciseRepository exerciseRepository, ILogger<ExercisesController> logger)
        {
            _exerciseRepository = exerciseRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAllExercises()
        {
            try
            {
                var exercises = await _exerciseRepository.GetAllAsync();
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all exercises");
                return StatusCode(500, "An error occurred while retrieving exercises");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExerciseById(Guid id)
        {
            try
            {
                var exercise = await _exerciseRepository.GetByIdAsync(id);
                if (exercise == null)
                {
                    return NotFound();
                }
                return Ok(exercise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving exercise with ID {ExerciseId}", id);
                return StatusCode(500, "An error occurred while retrieving the exercise");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Exercise>>> SearchExercises([FromQuery] string term)
        {
            try
            {
                var exercises = await _exerciseRepository.SearchExercisesAsync(term);
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching exercises with term {SearchTerm}", term);
                return StatusCode(500, "An error occurred while searching exercises");
            }
        }

        [HttpGet("by-muscle/{muscleId}")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercisesByMuscle(Guid muscleId)
        {
            try
            {
                var exercises = await _exerciseRepository.GetExercisesByMuscleAsync(muscleId);
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving exercises for muscle {MuscleId}", muscleId);
                return StatusCode(500, "An error occurred while retrieving exercises for the muscle");
            }
        }

        [HttpGet("by-main-muscle/{mainMuscle}")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercisesByMainMuscle(string mainMuscle)
        {
            try
            {
                var exercises = await _exerciseRepository.GetExercisesByMainMuscleAsync(mainMuscle);
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving exercises for main muscle {MainMuscle}", mainMuscle);
                return StatusCode(500, "An error occurred while retrieving exercises for the main muscle");
            }
        }
    }
}
