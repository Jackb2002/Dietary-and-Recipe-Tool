# Meal Planner and Shopping List Generator

A cross-platform desktop application built with C# and Avalonia UI (.NET 10) that generates personalised meal plans and shopping lists based on family composition and dietary preferences. Runs on **Windows, macOS, and Linux**.

## Features

- **Recipe search** — searches [Spoonacular](https://spoonacular.com/food-api) for recipes with full nutrition, ingredients, and method; auto-enriches on selection
- **Dual ingredient sources** — switches automatically between a local SQLite database and the live [OpenFoodFacts API](https://world.openfoodfacts.org/) at startup
- **Family-aware nutrition** — model your household with age and gender-specific nutritional targets (child, teenager, adult, elderly — male/female)
- **Diet profiles** — built-in plans (Balanced, Low Carb, Low Fat, High Protein, Low Protein, High Fibre) plus custom diets with your own nutritional priorities
- **Recipe scoring** — recipes are ranked against diet priorities using min-max normalisation, then randomly sampled from the top 10% for variety
- **Nutrient traffic light** — each selected recipe shows an 8-cell nutrient grid colour-coded green/orange/red relative to your family's total daily recommended intake
- **Supermarket pricing** — "Get Prices" on the shopping list tab fetches live cross-supermarket prices via [Trolley.co.uk](https://trolley.co.uk) (no API key required)
- **Shopping list generation** — aggregates ingredients across your meal plan and saves to desktop
- **Kitchen converter** — converts between grams, oz, lbs, tsp, tbsp, ml, litres, fl oz, and cups

## Architecture

```
┌──────────────────────────────────────────┐
│           Avalonia UI (cross-platform)   │
│   LoadingWindow → MainWindow             │
│   FamilyEditor │ DietSelector            │
│   IngredientSelector │ KitchenConverter  │
└────────────────┬─────────────────────────┘
                 │  (MVVM / CommunityToolkit)
        ┌────────▼─────────┐
        │  DietaryApp.Core  │  ← net10.0, no platform deps
        │  Models, Family   │
        │  IIngredientContext
        │  SpoonacularAPI   │
        │  TrolleyAPI       │
        └────────┬──────────┘
       ┌─────────┴──────────┐
       │                    │
DatabaseManager         OpenFoodAPI
 (SQLite local)         (remote REST)
                 │
       ┌─────────┴──────────┐
       │                    │
 RecipeExtractor       SupermarketInfo
 (BBC GoodFood          (HttpClient +
  scraper)               HtmlAgilityPack)
```

**`IIngredientContext`** is the central abstraction — `DatabaseManager` and `OpenFoodAPI` both implement it, letting the UI layer swap data sources without touching any business logic.

## Tech Stack

| Layer | Technology |
|---|---|
| UI | [Avalonia UI](https://avaloniaui.net/) 11 — Windows, macOS, Linux |
| MVVM | CommunityToolkit.Mvvm 8 (`[ObservableProperty]`, `[RelayCommand]`) |
| Recipe data | [Spoonacular API](https://spoonacular.com/food-api) (JSON REST) |
| Supermarket pricing | [Trolley.co.uk](https://trolley.co.uk) (HttpClient + HtmlAgilityPack) |
| Database | SQLite via `Microsoft.Data.Sqlite` (cross-platform) |
| HTML parsing | HtmlAgilityPack, AngleSharp |
| Serialisation | `System.Text.Json`, Newtonsoft.Json |
| Testing | MSTest v3 (.NET 10) — 85 tests |

## Project Structure

```
DissProject.sln
├── DietaryApp.Avalonia/    # Cross-platform UI — Views, ViewModels (MVVM)
├── DietaryApp.Core/        # Shared business logic — Models, Family, APIs
│   ├── Http/               # Shared HttpClient wrapper
│   ├── Recipes/            # SpoonacularAPI, IRecipeSource
│   └── Supermarket/        # TrolleyAPI, ProductPrice
├── RecipeExtractor/        # BBC GoodFood scraper (class library)
├── SupermarketInfo/        # Supermarket scraping helpers
├── WinFormsInfoApp/        # Original Windows-only UI (legacy)
└── UnitTests/              # MSTest unit tests (85 tests)
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Works on Windows, macOS, and Linux
- A free [Spoonacular API key](https://spoonacular.com/food-api) for live recipe search

### API Key Setup

Set the Spoonacular key as an environment variable before running:

```bash
# macOS / Linux
export SPOONACULAR_API_KEY=your_key_here

# Windows (PowerShell)
$env:SPOONACULAR_API_KEY="your_key_here"
```

Or drop a `spoonacular.key` file containing the key in the project root — the app picks it up automatically.

The app works without a key (local cache and OpenFoodFacts only).

### Build & Run

```bash
# Clone the repo
git clone https://github.com/Jackb2002/Dietary-and-Recipe-Tool.git
cd Dietary-and-Recipe-Tool

# Run the cross-platform app
dotnet run --project DietaryApp.Avalonia/DietaryApp.Avalonia.csproj

# Or build the full solution
dotnet build DissProject.sln
```

### Running Tests

```bash
# Run all unit tests
dotnet test UnitTests/UnitTests.csproj

# Include live integration tests (requires API access)
dotnet test UnitTests/UnitTests.csproj --filter "TestCategory=Integration"

# Skip integration tests (default for CI)
dotnet test UnitTests/UnitTests.csproj --filter "TestCategory!=Integration"

# Run a specific test class
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~DietsTest"
```

## How It Works

1. **On launch**, the app tests the OpenFoodFacts API (5s timeout); if unavailable it falls back to the local SQLite database automatically
2. **Recipe search** — type a query in the Recipes tab to search Spoonacular; selecting a result fetches full ingredients and method steps in the background
3. **Family setup** — add family members by age group and gender; each carries pre-set maximum daily nutritional values used for the nutrient traffic light
4. **Diet selection** — choose a built-in diet or create a custom one by ticking which nutrients to prioritise or avoid
5. **Meal generation** — `Diet.GenerateMeals()` scores every recipe using min-max normalised values, subtracts negative-priority scores from positive ones, then randomly picks from the top-scoring 10% so the plan varies day to day
6. **Shopping list** — add any recipe's ingredients to the list; hit **Get Prices** to fetch live UK supermarket prices for each item via Trolley
7. **Save** — export the shopping list with prices as a `.txt` file to your desktop

## Licence

[MIT](LICENSE)
