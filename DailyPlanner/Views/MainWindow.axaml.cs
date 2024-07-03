using System;
using Avalonia.Controls;
using DailyPlanner.ViewModels;

namespace DailyPlanner.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        /*Calendar.SelectedDate = DateTime.Today;*/
    }
}