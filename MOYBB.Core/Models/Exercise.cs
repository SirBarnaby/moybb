using System;

namespace MOYBB.Core.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? EquipmentRequired { get; set; }
        public string? MovementType { get; set; }
        public int? Popularity { get; set; }
        public int? RangeOfMotion { get; set; }
        public string? InjuryRiskFactor { get; set; }
        public string? JointStressFactor { get; set; }
        public string? CnsFatigueFactor { get; set; }
        public bool IsUnilateral { get; set; }
        public bool IsHighSpinalLoad { get; set; }
        public string? ImageUrl { get; set; }
        public string? MainMuscle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 