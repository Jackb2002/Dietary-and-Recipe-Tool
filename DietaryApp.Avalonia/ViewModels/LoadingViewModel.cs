using CommunityToolkit.Mvvm.ComponentModel;
using WinFormsInfoApp;
using WinFormsInfoApp.LocalDatabase;
using WinFormsInfoApp.OpenFood;

namespace DietaryApp.Avalonia.ViewModels;

public partial class LoadingViewModel : ObservableObject
{
    [ObservableProperty] private int _progress;
    [ObservableProperty] private string _statusMessage = "Starting...";

    public IIngredientContext? ResolvedContext { get; private set; }
    public event EventHandler? LoadingComplete;

    public async Task StartAsync()
    {
        await Task.Run(async () =>
        {
            SetProgress(20, "Locating database...");
            string path = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);

            SetProgress(50, "Checking API connection...");
            OpenFoodAPI apiConnection = new();

            // TestConnection already fires a real search request with a 5s timeout,
            // so no second verification call is needed.
            if (apiConnection.TestConnection())
            {
                SetProgress(90, "Connected to OpenFoodFacts API");
                ResolvedContext = apiConnection;
            }
            else
            {
                SetProgress(80, "API unavailable — using local database");
                ResolvedContext = new DatabaseFileOpener(path).CreateOrOpen();
            }

            SetProgress(100, "Loading complete");
            await Task.Delay(200);
        });

        await global::Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            LoadingComplete?.Invoke(this, EventArgs.Empty));
    }

    private void SetProgress(int value, string message)
    {
        global::Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            Progress = value;
            StatusMessage = message;
        });
    }
}
