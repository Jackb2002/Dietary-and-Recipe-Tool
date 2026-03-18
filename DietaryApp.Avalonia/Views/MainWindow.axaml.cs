using Avalonia.Controls;
using Avalonia.Interactivity;
using DietaryApp.Avalonia.ViewModels;
using WinFormsInfoApp;

namespace DietaryApp.Avalonia.Views;

public partial class MainWindow : Window
{
    private MainViewModel Vm => (MainViewModel)DataContext!;

    public MainWindow(IIngredientContext context)
    {
        InitializeComponent();
        DataContext = new MainViewModel(context);
        Opened += async (_, _) => await Vm.InitializeAsync();
        Closing += (_, _) => Vm.SaveCaches();
    }

    // ── Converter tab: TextBox change handlers ──────────────────────
    private void WeightChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;
        string field = tb.Name switch
        {
            "ozTxt"   => "oz",
            "lbsTxt"  => "lbs",
            "tspTxt"  => "tsp",
            "tbspTxt" => "tbsp",
            _         => "g"
        };
        Vm.WeightFieldChanged(field, tb.Text ?? "");

        // Sync other boxes without re-triggering
        if (tb.Name != "gTxt")    SetText("gTxt",    Vm.GTxt);
        if (tb.Name != "ozTxt")   SetText("ozTxt",   Vm.OzTxt);
        if (tb.Name != "lbsTxt")  SetText("lbsTxt",  Vm.LbsTxt);
        if (tb.Name != "tspTxt")  SetText("tspTxt",  Vm.TspTxt);
        if (tb.Name != "tbspTxt") SetText("tbspTxt", Vm.TbspTxt);
    }

    private void LiquidChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox tb) return;
        string field = tb.Name switch
        {
            "lTxt"    => "l",
            "flOzTxt" => "floz",
            "cupTxt"  => "cup",
            _         => "ml"
        };
        Vm.LiquidFieldChanged(field, tb.Text ?? "");

        if (tb.Name != "mlTxt")   SetText("mlTxt",   Vm.MlTxt);
        if (tb.Name != "lTxt")    SetText("lTxt",    Vm.LTxt);
        if (tb.Name != "flOzTxt") SetText("flOzTxt", Vm.FlOzTxt);
        if (tb.Name != "cupTxt")  SetText("cupTxt",  Vm.CupTxt);
    }

    private void SetText(string name, string value)
    {
        if (this.FindControl<TextBox>(name) is TextBox tb && tb.Text != value)
            tb.Text = value;
    }

    // ── Diet tab button handlers ────────────────────────────────────
    private async void PremadeDiets_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new ExistingDietsSelectorDialog(Vm.CurrentDiet != null
            ? [Vm.CurrentDiet]
            : []);
        var result = await dialog.ShowDialog<WinFormsInfoApp.Models.Diet?>(this);
        if (result != null) Vm.ApplyNewDiet(result);
    }

    private async void CustomDiet_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new CustomDietCreatorDialog();
        var result = await dialog.ShowDialog<WinFormsInfoApp.Models.Diet?>(this);
        if (result != null) Vm.ApplyNewDiet(result);
    }

    private async void EditFamily_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new FamilyEditorDialog(Vm.CurrentFamily);
        var result = await dialog.ShowDialog<WinFormsInfoApp.Family.Family?>(this);
        if (result != null) Vm.UpdateFamily(result);
    }

    private async void BadIngredients_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new IngredientSelectorDialog(false, Vm.AvoidIngredients);
        var result = await dialog.ShowDialog<List<string>?>(this);
        if (result != null) Vm.AvoidIngredients = result;
    }

    private async void GoodIngredients_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new IngredientSelectorDialog(true, Vm.GoodIngredients);
        var result = await dialog.ShowDialog<List<string>?>(this);
        if (result != null) Vm.GoodIngredients = result;
    }
}
