using System;
using System.Collections.Generic;

namespace MOYBB.Core.Models
{
    public class Exercise
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
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
        
        // Navigation property for many-to-many relationship
        public ICollection<MuscleInExercise> MuscleInExercises { get; set; } = new List<MuscleInExercise>();
    }
} 