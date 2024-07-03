using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DailyPlanner.Models;
using ReactiveUI;
using Type = DailyPlanner.Models.Type;

namespace DailyPlanner.ViewModels;

public class HabitsViewModel : ViewModelBase
{
    public ObservableCollection<Habit> Habits { get; set; }
    public ObservableCollection<Type> HabitTypes { get; set; }
    public ObservableCollection<Frequency> Frequencies { get; set; }

    private string _newHabitName;
    public string NewHabitName
    {
        get => _newHabitName;
        set => this.RaiseAndSetIfChanged(ref _newHabitName, value);
    }

    private string _newHabitDescription;
    public string NewHabitDescription
    {
        get => _newHabitDescription;
        set => this.RaiseAndSetIfChanged(ref _newHabitDescription, value);
    }

    private Type _newHabitType;
    public Type NewHabitType
    {
        get => _newHabitType;
        set => this.RaiseAndSetIfChanged(ref _newHabitType, value);
    }

    private Frequency _newHabitFrequency;
    public Frequency NewHabitFrequency
    {
        get => _newHabitFrequency;
        set => this.RaiseAndSetIfChanged(ref _newHabitFrequency, value);
    }

    private string _newHabitGoal;
    public string NewHabitGoal
    {
        get => _newHabitGoal;
        set => this.RaiseAndSetIfChanged(ref _newHabitGoal, value);
    }

    private bool _newHabitReminder;
    public bool NewHabitReminder
    {
        get => _newHabitReminder;
        set => this.RaiseAndSetIfChanged(ref _newHabitReminder, value);
    }

    public ICommand AddHabitCommand { get; }

    public HabitsViewModel()
    {
        Habits = new ObservableCollection<Habit>();
        HabitTypes = new ObservableCollection<Type> { Type.Daily, Type.Weekly };
        Frequencies = new ObservableCollection<Frequency> { Frequency.Daily, Frequency.Weekly };

        AddHabitCommand = ReactiveCommand.Create(AddHabit);
    }

    private void AddHabit()
    {
        var newHabit = new Habit
        {
            Id = Guid.NewGuid(),
            Name = NewHabitName,
            Description = NewHabitDescription,
            Type = NewHabitType,
            Frequency = NewHabitFrequency,
            Goal = NewHabitGoal,
            Reminder = NewHabitReminder
        };

        Habits.Add(newHabit);
    }
}
