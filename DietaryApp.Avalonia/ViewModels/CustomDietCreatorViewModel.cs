using CommunityToolkit.Mvvm.ComponentModel;
using WinFormsInfoApp.Models;

namespace DietaryApp.Avalonia.ViewModels;

public partial class CustomDietCreatorViewModel : ObservableObject
{
    [ObservableProperty] private string _dietName = string.Empty;
    [ObservableProperty] private string _dietDescription = string.Empty;
    [ObservableProperty] private string? _validationMessage;

    // Calories
    [ObservableProperty] private bool _calPos;
    [ObservableProperty] private bool _calNeg;
    // Fat
    [ObservableProperty] private bool _fatPos;
    [ObservableProperty] private bool _fatNeg;
    // Saturates
    [ObservableProperty] private bool _satPos;
    [ObservableProperty] private bool _satNeg;
    // Fibre
    [ObservableProperty] private bool _fibPos;
    [ObservableProperty] private bool _fibNeg;
    // Carbs
    [ObservableProperty] private bool _carbPos;
    [ObservableProperty] private bool _carbNeg;
    // Salt
    [ObservableProperty] private bool _salPos;
    [ObservableProperty] private bool _salNeg;
    // Sugars
    [ObservableProperty] private bool _sugPos;
    [ObservableProperty] private bool _sugNeg;
    // Protein
    [ObservableProperty] private bool _proPos;
    [ObservableProperty] private bool _proNeg;

    // Mutual-exclusion: positive and negative can't both be set for the same nutrient
    partial void OnCalPosChanged(bool value) { if (value) CalNeg = false; }
    partial void OnCalNegChanged(bool value) { if (value) CalPos = false; }
    partial void OnFatPosChanged(bool value) { if (value) FatNeg = false; }
    partial void OnFatNegChanged(bool value) { if (value) FatPos = false; }
    partial void OnSatPosChanged(bool value) { if (value) SatNeg = false; }
    partial void OnSatNegChanged(bool value) { if (value) SatPos = false; }
    partial void OnFibPosChanged(bool value) { if (value) FibNeg = false; }
    partial void OnFibNegChanged(bool value) { if (value) FibPos = false; }
    partial void OnCarbPosChanged(bool value) { if (value) CarbNeg = false; }
    partial void OnCarbNegChanged(bool value) { if (value) CarbPos = false; }
    partial void OnSalPosChanged(bool value) { if (value) SalNeg = false; }
    partial void OnSalNegChanged(bool value) { if (value) SalPos = false; }
    partial void OnSugPosChanged(bool value) { if (value) SugNeg = false; }
    partial void OnSugNegChanged(bool value) { if (value) SugPos = false; }
    partial void OnProPosChanged(bool value) { if (value) ProNeg = false; }
    partial void OnProNegChanged(bool value) { if (value) ProPos = false; }

    public Diet? TryBuildDiet()
    {
        if (string.IsNullOrWhiteSpace(DietName))
        {
            ValidationMessage = "Diet name is required.";
            return null;
        }
        if (string.IsNullOrWhiteSpace(DietDescription))
        {
            ValidationMessage = "Diet description is required.";
            return null;
        }

        List<string> positives = [];
        List<string> negatives = [];
        if (CalPos) positives.Add("Kcal"); if (CalNeg) negatives.Add("Kcal");
        if (FatPos) positives.Add("Fat"); if (FatNeg) negatives.Add("Fat");
        if (SatPos) positives.Add("Saturates"); if (SatNeg) negatives.Add("Saturates");
        if (FibPos) positives.Add("Fibre"); if (FibNeg) negatives.Add("Fibre");
        if (CarbPos) positives.Add("Carbs"); if (CarbNeg) negatives.Add("Carbs");
        if (SalPos) positives.Add("Salt"); if (SalNeg) negatives.Add("Salt");
        if (SugPos) positives.Add("Sugars"); if (SugNeg) negatives.Add("Sugars");
        if (ProPos) positives.Add("Protein"); if (ProNeg) negatives.Add("Protein");

        if (positives.Count == 0 && negatives.Count == 0)
        {
            ValidationMessage = "Select at least one positive or negative attribute.";
            return null;
        }

        ValidationMessage = null;
        return new Diet(DietName, DietDescription, [.. positives], [.. negatives], DateTime.Now);
    }
}
