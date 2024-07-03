namespace DailyPlanner.Models;

public class Frequency
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public static readonly Frequency Daily = new Frequency { Id = 1, Name = "Daily" };
    public static readonly Frequency Weekly = new Frequency { Id = 2, Name = "Weekly" };
}