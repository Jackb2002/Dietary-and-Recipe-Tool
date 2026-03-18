# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test Commands

```bash
# Build entire solution
dotnet build DissProject.sln

# Build in Release mode
dotnet build -c Release DissProject.sln

# Run all tests
dotnet test DissProject.sln

# Run specific test class
dotnet test UnitTests/UnitTests.csproj --filter "ClassName=DietsTest"

# Run specific test method
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~MethodName"
```

Note: This is a WinForms application targeting `net8.0-windows8.0`, so it can only be run/built on Windows.

## Architecture Overview

This is a **C# WinForms (.NET 8) desktop app** for meal planning and shopping list generation. The solution (`DissProject.sln`) has four projects:

### Projects & Dependencies

```
UnitTests → RecipeExtractor, WinFormsInfoApp
WinFormsInfoApp → RecipeExtractor
SupermarketInfo (standalone)
```

- **WinFormsInfoApp** — Main application (WinExe). Entry point: `Program.cs` → `LoadingWindow` → `MainWindow`.
- **RecipeExtractor** — Class library that scrapes recipe data from the BBC GoodFood website using HtmlAgilityPack/AngleSharp.
- **SupermarketInfo** — Class library (targets .NET Framework 4.7.2, not SDK-style) for scraping Tesco product data via Selenium WebDriver.
- **UnitTests** — MSTest project covering core models and the RecipeExtractor.

### Key Design Patterns

**Strategy pattern for ingredient data:** `IIngredientContext` (in `WinFormsInfoApp/`) is the central abstraction. Both `DatabaseManager` (SQLite local DB) and `OpenFoodAPI` (remote OpenFoodFacts API) implement this interface, allowing the UI to switch data sources transparently.

**Domain models** live in `WinFormsInfoApp/Models/` and `WinFormsInfoApp/Family/`:
- `Diet` — nutritional preferences with positive/negative priorities (Balanced, Low Carb, High Protein, etc.)
- `Recipe` — meal definition with full nutritional breakdown
- `Ingredient` — food item with fat/carbs/protein/calories/sugar/fiber/etc.
- `Family` / `Person` subclasses — family unit with age/gender-specific nutritional requirements (AdultMale, AdultFemale, ChildMale, ChildFemale, TeenMale, TeenFemale, ElderlyMale, ElderlyFemale)

### Notable Configuration

- `.editorconfig` suppresses nullable reference warnings (CS8600, CS8602, CS8604, CS8618) — nullable warnings are intentionally silenced project-wide.
- Recipe data is cached locally in `recipe_cache.json`.
- SupermarketInfo requires `chromedriver.exe` (bundled) for Selenium-based scraping.
