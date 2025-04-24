using System;
using System.Collections.Generic;

namespace MOYBB.Core.Models.DTOs
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? EquipmentRequired { get; set; }
        public string? MovementType { get; set; }
        public int? Popularity { get; set; }
        public string? RangeOfMotion { get; set; }
        public string? InjuryRiskFactor { get; set; }
        public string? JointStressFactor { get; set; }
        public string? CnsFatigueFactor { get; set; }
        public bool IsUnilateral { get; set; }
        public bool IsHighSpinalLoad { get; set; }
        public string? ImageUrl { get; set; }
        public string? MainMuscle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public ICollection<MuscleInExerciseDto> MuscleInExercises { get; set; } = new List<MuscleInExerciseDto>();
    }

    public class MuscleInExerciseDto
    {
        public int ExerciseId { get; set; }
        public int MuscleId { get; set; }
        public string? ContractionType { get; set; }
        public string? FatigueAccumulationFactor { get; set; }
        public string? MuscleMovementCategory { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 