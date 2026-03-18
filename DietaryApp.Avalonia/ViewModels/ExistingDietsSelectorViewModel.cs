using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WinFormsInfoApp.Models;

namespace DietaryApp.Avalonia.ViewModels;

public partial class ExistingDietsSelectorViewModel : ObservableObject
{
    [ObservableProperty] private Diet? _selectedDiet;
    [ObservableProperty] private string _dietTitle = string.Empty;
    [ObservableProperty] private string _dietInfo = string.Empty;
    [ObservableProperty] private string _priorityText = string.Empty;

    public ObservableCollection<Diet> Diets { get; } = [];

    public ExistingDietsSelectorViewModel(List<Diet> userDiets)
    {
        // Remove cached default diets so we can replace with fresh defaults
        var customDiets = userDiets.Where(d => !d.DefaultDiet).ToList();
        foreach (var d in customDiets) Diets.Add(d);
        foreach (var d in Diet.ReturnDefaultDiets()) Diets.Add(d);
    }

    partial void OnSelectedDietChanged(Diet? value)
    {
        if (value == null) return;
        DietTitle = value.Name;
        DietInfo = value.Description;
        PriorityText = "This diet prioritises:\n" + string.Join("\n", value.PriorityPositive);
    }
}
