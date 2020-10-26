using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Models;

namespace TrickingLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrickCategory>()
                        .HasKey(tc => new { tc.CategoryId, tc.TrickId });

            modelBuilder.Entity<TrickRelationship>()
                        .HasKey(tr => new { tr.PrerequisiteId, tr.ProgressionId });

            modelBuilder.Entity<TrickRelationship>()
                        .HasOne(tr => tr.Prerequisite)
                        .WithMany(t => t.Progressions)
                        .HasForeignKey(tr => tr.PrerequisiteId);

            modelBuilder.Entity<TrickRelationship>()
                        .HasOne(tr => tr.Progression)
                        .WithMany(t => t.Prerequisites)
                        .HasForeignKey(tr => tr.ProgressionId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Trick> Tricks { get; set; }
        public DbSet<TrickCategory> TrickCategories { get; set; }
        public DbSet<TrickRelationship> TrickRelationships { get; set; }
    }
}