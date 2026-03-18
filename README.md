# Meal Planner and Shopping List Generator

A cross-platform desktop application built with C# and Avalonia UI (.NET 10) that generates personalised meal plans and shopping lists based on family composition and dietary preferences. Runs on **Windows, macOS, and Linux**.

## Features

- **Dual ingredient sources** — switches automatically between a local SQLite database and the live [OpenFoodFacts API](https://world.openfoodfacts.org/) at startup
- **Family-aware nutrition** — model your household with age and gender-specific nutritional targets (child, teenager, adult, elderly — male/female)
- **Diet profiles** — built-in plans (Balanced, Low Carb, Low Fat, High Protein, Low Protein, High Fibre) plus custom diets with your own nutritional priorities
- **Recipe scoring** — recipes are ranked against diet priorities using min-max normalisation, then randomly sampled from the top 10% for variety
- **Nutrient traffic light** — each selected recipe shows an 8-cell nutrient grid colour-coded green/orange/red relative to your family's total daily recommended intake
- **Recipe scraping** — fetches structured recipe data (ingredients, nutrition, cook time, allergens, method) from BBC Good Food
- **Shopping list generation** — aggregates ingredients across your meal plan and saves to desktop
- **Kitchen converter** — converts between grams, oz, lbs, tsp, tbsp, ml, litres, fl oz, and cups
- **Supermarket pricing** — scrapes live product prices from Tesco (Windows only, via Selenium)

## Architecture

```
┌──────────────────────────────────────────┐
│           Avalonia UI (cross-platform)   │
│   LoadingWindow → MainWindow             │
│   FamilyEditor │ DietSelector            │
│   IngredientSelector │ KitchenConverter  │
└────────────────┬─────────────────────────┘
                 │  (MVVM / CommunityToolkit)
        ┌────────▼────────┐
        │  DietaryApp.Core │  ← net10.0, no platform deps
        │  Models, Family  │
        │  IIngredientContext
        └────────┬─────────┘
       ┌─────────┴──────────┐
       │                    │
DatabaseManager         OpenFoodAPI
 (SQLite local)         (remote REST)
                 │
       ┌─────────┴──────────────┐
       │                        │
 RecipeExtractor           SupermarketInfo
 (BBC GoodFood              (Tesco scraper
  HTML scraper)              via Selenium)
```

**`IIngredientContext`** is the central abstraction — `DatabaseManager` and `OpenFoodAPI` both implement it, letting the UI layer swap data sources without touching any business logic.

## Tech Stack

| Layer | Technology |
|---|---|
| UI | [Avalonia UI](https://avaloniaui.net/) 11 — Windows, macOS, Linux |
| MVVM | CommunityToolkit.Mvvm 8 (`[ObservableProperty]`, `[RelayCommand]`) |
| Database | SQLite via `Microsoft.Data.Sqlite` (cross-platform) |
| HTML parsing | HtmlAgilityPack, AngleSharp |
| Web scraping | Selenium WebDriver 4 + ChromeDriver (Tesco, Windows only) |
| Serialisation | `System.Text.Json`, Newtonsoft.Json |
| Testing | MSTest v3 (.NET 10) |

## Project Structure

```
DissProject.sln
├── DietaryApp.Avalonia/    # Cross-platform UI — Views, ViewModels (MVVM)
├── DietaryApp.Core/        # Shared business logic — Models, Family, data access
├── RecipeExtractor/        # BBC GoodFood HTML scraper (class library)
├── SupermarketInfo/        # Tesco product price scraper (Windows, Selenium)
├── WinFormsInfoApp/        # Original Windows-only UI (legacy)
└── UnitTests/              # MSTest unit tests
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Works on Windows, macOS, and Linux
- Google Chrome + matching [ChromeDriver](https://chromedriver.chromium.org/) (for Tesco price scraping only)

### Build & Run

```bash
# Clone the repo
git clone https://github.com/your-username/Dietary-and-Recipe-Tool.git
cd Dietary-and-Recipe-Tool

# Run the cross-platform app
dotnet run --project DietaryApp.Avalonia/DietaryApp.Avalonia.csproj

# Or build the full solution
dotnet build DissProject.sln
```

### Running Tests

```bash
# Run all tests
dotnet test UnitTests/UnitTests.csproj

# Run a specific test class
dotnet test UnitTests/UnitTests.csproj --filter "ClassName=DietsTest"
```

## How It Works

1. **On launch**, the app tests the OpenFoodFacts API connection; if unavailable it falls back to the local SQLite database automatically
2. **Family setup** — add family members by age group and gender; each carries pre-set maximum daily nutritional values used for the nutrient traffic light
3. **Diet selection** — choose a built-in diet or create a custom one by ticking which nutrients to prioritise or avoid
4. **Meal generation** — `Diet.GenerateMeals()` scores every recipe using min-max normalised values, subtracts negative-priority scores from positive ones, then randomly picks from the top-scoring 10% so the plan varies day to day
5. **Shopping list** — add any recipe's ingredients to the list, then save it as a `.txt` file to your desktop

## Licence

[MIT](LICENSE)
