namespace DailyPlanner.Models;

public class Type
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string IconPath { get; set; }

    public static readonly Type Daily = new Type { Id = 1, Name = "Learning", Color = "Blue", IconPath = "/Assets/daily.png" };
    public static readonly Type Weekly = new Type { Id = 2, Name = "Health", Color = "Green", IconPath = "/Assets/weekly.png" };
}