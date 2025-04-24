using System.Collections.Generic;
using System.Linq;
using MOYBB.Core.Models;
using MOYBB.Core.Models.DTOs;

namespace MOYBB.Core.Mapping
{
    public static class ExerciseMapper
    {
        public static ExerciseDto ToDto(this Exercise exercise)
        {
            return new ExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                EquipmentRequired = exercise.EquipmentRequired,
                MovementType = exercise.MovementType,
                Popularity = exercise.Popularity,
                RangeOfMotion = exercise.RangeOfMotion,
                InjuryRiskFactor = exercise.InjuryRiskFactor,
                JointStressFactor = exercise.JointStressFactor,
                CnsFatigueFactor = exercise.CnsFatigueFactor,
                IsUnilateral = exercise.IsUnilateral,
                IsHighSpinalLoad = exercise.IsHighSpinalLoad,
                ImageUrl = exercise.ImageUrl,
                MainMuscle = exercise.MainMuscle,
                CreatedAt = exercise.CreatedAt,
                UpdatedAt = exercise.UpdatedAt,
                MuscleInExercises = exercise.MuscleInExercises.Select(mie => new MuscleInExerciseDto
                {
                    ExerciseId = mie.ExerciseId,
                    MuscleId = mie.MuscleId,
                    ContractionType = mie.ContractionType,
                    FatigueAccumulationFactor = mie.FatigueAccumulationFactor,
                    MuscleMovementCategory = mie.MuscleMovementCategory,
                    CreatedAt = mie.CreatedAt
                }).ToList()
            };
        }
    }
} 