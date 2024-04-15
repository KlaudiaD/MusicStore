using System;

namespace DailyPlanner.Models;

public class Note
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? Reminder { get; set; }
    public string Color { get; set; }
    public bool Pinned { get; set; }
    public string FontFamily { get; set; }
    public float FontSize { get; set; }
    public Type? Type { get; set; }
}