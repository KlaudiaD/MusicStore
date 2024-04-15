using DailyPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Data;

public class AppDbContext :DbContext
{
    public DbSet<Frequency> Frequencies { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=DailyPlannerApp.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>()
            .HasOne(h => h.Type)
            .WithMany()
            .HasForeignKey("TypeId")
            .IsRequired();

        modelBuilder.Entity<Habit>()
            .HasOne(h => h.Frequency)
            .WithMany()
            .HasForeignKey("FrequencyId")
            .IsRequired();

        modelBuilder.Entity<Note>()
            .HasOne(n => n.Type)
            .WithMany()
            .HasForeignKey("TypeId");

    }
}