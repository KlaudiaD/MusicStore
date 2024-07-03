using System.Reactive;
using DailyPlanner.ViewModels;
using ReactiveUI;

public class MainWindowViewModel : ViewModelBase
{
    private bool _isVisibleAddHabitView;
    public bool IsVisibleAddHabitView
    {
        get => _isVisibleAddHabitView;
        set => this.RaiseAndSetIfChanged(ref _isVisibleAddHabitView, value);
    }

    public ReactiveCommand<Unit, Unit> ToggleAddHabitViewCommand { get; }

    public MainWindowViewModel()
    {
        ToggleAddHabitViewCommand = ReactiveCommand.Create(ToggleAddHabitView);
    }

    private void ToggleAddHabitView()
    {
        IsVisibleAddHabitView = !IsVisibleAddHabitView;
    }
}