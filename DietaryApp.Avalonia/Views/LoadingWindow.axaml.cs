using Avalonia.Controls;
using DietaryApp.Avalonia.ViewModels;

namespace DietaryApp.Avalonia.Views;

public partial class LoadingWindow : Window
{
    public LoadingWindow()
    {
        InitializeComponent();
        var vm = new LoadingViewModel();
        DataContext = vm;
        vm.LoadingComplete += OnLoadingComplete;
        _ = vm.StartAsync();
    }

    private void OnLoadingComplete(object? sender, EventArgs e)
    {
        var vm = (LoadingViewModel)DataContext!;
        if (vm.ResolvedContext == null) return;

        var mainWindow = new MainWindow(vm.ResolvedContext);
        mainWindow.Show();
        Close();
    }
}
