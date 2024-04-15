using System;

namespace DailyPlanner.Models;

public class Habit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Type Type { get; set; }
    public Frequency Frequency { get; set; }
    public string Goal { get; set; }
    public bool? Reminder { get; set; }
}