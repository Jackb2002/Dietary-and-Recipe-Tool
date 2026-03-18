using Avalonia.Controls;
using Avalonia.Interactivity;
using DietaryApp.Avalonia.ViewModels;

namespace DietaryApp.Avalonia.Views;

public partial class IngredientSelectorDialog : Window
{
    public IngredientSelectorDialog(bool forPositive, IEnumerable<string> existing)
    {
        InitializeComponent();
        DataContext = new IngredientSelectorViewModel(forPositive, existing);
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var vm = (IngredientSelectorViewModel)DataContext!;
        Close(vm.GetResult());
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e) => Close(null);
}
