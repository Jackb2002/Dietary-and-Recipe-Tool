using Avalonia.Controls;
using Avalonia.Interactivity;
using DietaryApp.Avalonia.ViewModels;
using WinFormsInfoApp.Family;

namespace DietaryApp.Avalonia.Views;

public partial class FamilyEditorDialog : Window
{
    public FamilyEditorDialog(Family family)
    {
        InitializeComponent();
        DataContext = new FamilyEditorViewModel(family);
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var vm = (FamilyEditorViewModel)DataContext!;
        Close(vm.BuildFamily());
    }
}
