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
    public class MusclesController : ControllerBase
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly ILogger<MusclesController> _logger;

        public MusclesController(IMuscleRepository muscleRepository, ILogger<MusclesController> logger)
        {
            _muscleRepository = muscleRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Muscle>>> GetAllMuscles()
        {
            try
            {
                var muscles = await _muscleRepository.GetAllAsync();
                return Ok(muscles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all muscles");
                return StatusCode(500, "An error occurred while retrieving muscles");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Muscle>> GetMuscleById(Guid id)
        {
            try
            {
                var muscle = await _muscleRepository.GetByIdAsync(id);
                if (muscle == null)
                {
                    return NotFound();
                }
                return Ok(muscle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving muscle with ID {MuscleId}", id);
                return StatusCode(500, "An error occurred while retrieving the muscle");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Muscle>>> SearchMuscles([FromQuery] string term)
        {
            try
            {
                var muscles = await _muscleRepository.SearchMusclesAsync(term);
                return Ok(muscles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching muscles with term {SearchTerm}", term);
                return StatusCode(500, "An error occurred while searching muscles");
            }
        }

        [HttpGet("by-exercise/{exerciseId}")]
        public async Task<ActionResult<IEnumerable<Muscle>>> GetMusclesByExercise(Guid exerciseId)
        {
            try
            {
                var muscles = await _muscleRepository.GetMusclesByExerciseAsync(exerciseId);
                return Ok(muscles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving muscles for exercise {ExerciseId}", exerciseId);
                return StatusCode(500, "An error occurred while retrieving muscles for the exercise");
            }
        }
    }
} 