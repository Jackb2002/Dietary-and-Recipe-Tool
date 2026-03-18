using Avalonia.Controls;
using Avalonia.Interactivity;
using DietaryApp.Avalonia.ViewModels;

namespace DietaryApp.Avalonia.Views;

public partial class CustomDietCreatorDialog : Window
{
    public CustomDietCreatorDialog()
    {
        InitializeComponent();
        DataContext = new CustomDietCreatorViewModel();
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var vm = (CustomDietCreatorViewModel)DataContext!;
        var diet = vm.TryBuildDiet();
        if (diet != null) Close(diet);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e) => Close(null);
}
