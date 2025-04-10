using System;

namespace MOYBB.Core.Models
{
    public class MuscleInExercise
    {
        public Guid ExerciseId { get; set; }
        public Guid MuscleId { get; set; }
        public string? ContractionType { get; set; }
        public string? FatigueAccumulationFactor { get; set; }
        public string? MuscleMovementCategory { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties
        public required Exercise Exercise { get; set; }
        public required Muscle Muscle { get; set; }
    }
} 