using System;

namespace MOYBB.Core.Models
{
    public class Muscle
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? NameLatin { get; set; }
        public string? Description { get; set; }
        public string? DominantFiberType { get; set; }
        public string? EnduranceRatingFactor { get; set; }
        public string? RecoveryTimeFactor { get; set; }
        public string? NeuralDriveSensitivityFactor { get; set; }
        public string? MotorUnitRecruitmentSpeedFactor { get; set; }
        public string? StretchSensitivityFactor { get; set; }
        public string? EccentricStrengthFactor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 