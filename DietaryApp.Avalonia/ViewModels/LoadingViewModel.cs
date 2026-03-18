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
            SetProgress(15, "Locating database...");
            string path = Path.GetFullPath(GlobalSettings.LocalDatabaseFile);

            SetProgress(30, "Checking API connection...");
            OpenFoodAPI apiConnection = new();

            SetProgress(50, "Trying to connect to API...");
            if (apiConnection.TestConnection())
            {
                SetProgress(60, "Testing API response...");
                var result = apiConnection.GetIngredientsByName("chocolate");
                if (result != null && result.Length > 0)
                {
                    SetProgress(80, "Connected to API successfully");
                    await Task.Delay(300);
                    ResolvedContext = apiConnection;
                }
                else
                {
                    SetProgress(75, "API returned no data, using local DB...");
                    ResolvedContext = new DatabaseFileOpener(path).CreateOrOpen();
                }
            }
            else
            {
                SetProgress(65, "API unavailable, using local DB...");
                ResolvedContext = new DatabaseFileOpener(path).CreateOrOpen();
            }

            SetProgress(100, "Loading complete");
            await Task.Delay(300);
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
