using Avalonia.Media;

namespace DietaryApp.Avalonia.ViewModels;

public class NutrientDisplayItem
{
    public string Label { get; set; } = string.Empty;
    public string DisplayValue { get; set; } = string.Empty;
    public IBrush Color { get; set; } = Brushes.Green;

    private const float RedThreshold = 75f;
    private const float OrangeThreshold = 45f;

    public static IBrush BrushForPercentage(float pct) =>
        pct > RedThreshold ? Brushes.Red :
        pct > OrangeThreshold ? Brushes.Orange :
        Brushes.Green;
}
