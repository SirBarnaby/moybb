using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MOYBB.Core.Models;

namespace MOYBB.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<MuscleInExercise> MuscleInExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Exercise entity
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("exercise");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.EquipmentRequired).HasColumnName("equipment_required");
                entity.Property(e => e.MovementType).HasColumnName("movement_type");
                entity.Property(e => e.Popularity).HasColumnName("popularity");
                entity.Property(e => e.RangeOfMotion).HasColumnName("range_of_motion");
                entity.Property(e => e.InjuryRiskFactor).HasColumnName("injury_risk_factor");
                entity.Property(e => e.JointStressFactor).HasColumnName("joint_stress_factor");
                entity.Property(e => e.CnsFatigueFactor).HasColumnName("cns_fatigue_factor");
                entity.Property(e => e.IsUnilateral).HasColumnName("is_unilateral");
                entity.Property(e => e.IsHighSpinalLoad).HasColumnName("is_high_spinal_load");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");
                entity.Property(e => e.MainMuscle).HasColumnName("main_muscle");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            // Configure Muscle entity
            modelBuilder.Entity<Muscle>(entity =>
            {
                entity.ToTable("muscle");
                entity.Property(m => m.Id).HasColumnName("id");
                entity.Property(m => m.Name).HasColumnName("name");
                entity.Property(m => m.NameLatin).HasColumnName("name_latin");
                entity.Property(m => m.Description).HasColumnName("description");
                entity.Property(m => m.DominantFiberType).HasColumnName("dominant_fiber_type");
                entity.Property(m => m.EnduranceRatingFactor).HasColumnName("endurance_rating_factor");
                entity.Property(m => m.RecoveryTimeFactor).HasColumnName("recovery_time_factor");
                entity.Property(m => m.NeuralDriveSensitivityFactor).HasColumnName("neural_drive_sensitivity_factor");
                entity.Property(m => m.MotorUnitRecruitmentSpeedFactor).HasColumnName("motor_unit_recruitment_speed_factor");
                entity.Property(m => m.StretchSensitivityFactor).HasColumnName("stretch_sensitivity_factor");
                entity.Property(m => m.EccentricStrengthFactor).HasColumnName("eccentric_strength_factor");
                entity.Property(m => m.CreatedAt).HasColumnName("created_at");
                entity.Property(m => m.UpdatedAt).HasColumnName("updated_at");
            });

            // Configure MuscleInExercise entity
            modelBuilder.Entity<MuscleInExercise>(entity =>
            {
                entity.ToTable("muscle_in_exercise");
                entity.Property(mie => mie.ExerciseId).HasColumnName("exercise_id");
                entity.Property(mie => mie.MuscleId).HasColumnName("muscle_id");
                entity.Property(mie => mie.ContractionType).HasColumnName("contraction_type");
                entity.Property(mie => mie.FatigueAccumulationFactor).HasColumnName("fatigue_accumulation_factor");
                entity.Property(mie => mie.MuscleMovementCategory).HasColumnName("muscle_movement_category");
                entity.Property(mie => mie.CreatedAt).HasColumnName("created_at");
                
                entity.HasKey(mie => new { mie.ExerciseId, mie.MuscleId });

                entity.HasOne(mie => mie.Exercise)
                    .WithMany()
                    .HasForeignKey(mie => mie.ExerciseId);

                entity.HasOne(mie => mie.Muscle)
                    .WithMany()
                    .HasForeignKey(mie => mie.MuscleId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _logger?.LogWarning("DbContext is not configured. Make sure to provide connection string.");
            }
        }
    }
} 