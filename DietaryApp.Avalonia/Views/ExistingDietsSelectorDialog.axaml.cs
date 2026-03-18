using Avalonia.Controls;
using Avalonia.Interactivity;
using DietaryApp.Avalonia.ViewModels;
using WinFormsInfoApp.Models;

namespace DietaryApp.Avalonia.Views;

public partial class ExistingDietsSelectorDialog : Window
{
    public ExistingDietsSelectorDialog(List<Diet> userDiets)
    {
        InitializeComponent();
        DataContext = new ExistingDietsSelectorViewModel(userDiets);
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var vm = (ExistingDietsSelectorViewModel)DataContext!;
        Close(vm.SelectedDiet);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e) => Close(null);
}
