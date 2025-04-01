using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many relationship between Users and Exercises
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Exercises)
                .WithMany(e => e.Users)
                .UsingEntity(j => j.ToTable("UserExercises"));

            // One-to-One relationship between User and TrainingPlan
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.TrainingPlan)
            .WithOne(tp => tp.User)
                .HasForeignKey<TrainingPlan>(tp => tp.UserId);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Progress> ProgressRecords { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }

    }
}
