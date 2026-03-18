using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DietaryApp.Avalonia.ViewModels;

public partial class IngredientSelectorViewModel : ObservableObject
{
    public string WindowTitle { get; }
    public string HintText { get; }

    [ObservableProperty] private string _newIngredientText = string.Empty;
    [ObservableProperty] private string? _selectedIngredient;

    public ObservableCollection<string> Ingredients { get; } = [];

    public IngredientSelectorViewModel(bool forPositive, IEnumerable<string> existing)
    {
        WindowTitle = forPositive ? "Ingredients to include" : "Ingredients to avoid";
        HintText = forPositive
            ? "Add ingredients you want the diet plan to include."
            : "Add ingredients you want the diet plan to avoid.";

        foreach (var ing in existing)
            Ingredients.Add(ing);
    }

    [RelayCommand]
    void AddIngredient()
    {
        var ing = NewIngredientText.Trim().ToLower();
        if (!string.IsNullOrEmpty(ing) && !Ingredients.Contains(ing))
            Ingredients.Add(ing);
        NewIngredientText = string.Empty;
    }

    [RelayCommand]
    void RemoveSelected()
    {
        if (SelectedIngredient != null)
            Ingredients.Remove(SelectedIngredient);
    }

    public List<string> GetResult() => [.. Ingredients];
}
