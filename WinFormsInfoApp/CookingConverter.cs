using AngleSharp.Text;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    internal class CookingConverter
    {
        // Conversion factors
        const double TeaspoonToGrams = 5;
        const double TablespoonToGrams = 15;
        const double OunceToGrams = 28;
        const double CupToGrams = 240;
        const double PintToGrams = 480;
        const double QuartToGrams = 960;
        const double GallonToGrams = 3840;
        private static string[] Measurements = new string[] { "tsp", "tbsp", "oz", "cup", "pint", "quart", "gallon" };
        public static List<Ingredient> ParseRecipeIngredientString(string ingredientString,
            IIngredientContext contextLookup)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string[] Lines = ingredientString.Split("\n");
            //Split by last comma and remove after that 
            foreach (var line in Lines)
            {
                string info = line.Split(",")[0];
                info = info.Trim();
                info = info.ToLower();
                string[] parts = info.Split(' ');
                string trimmedInfo = info;
                string removedMeasurement = "";
                foreach (var measurement in Measurements)
                {
                    if (!trimmedInfo.Contains(measurement))
                    {
                        continue;
                    }
                    else
                    {
                        trimmedInfo = trimmedInfo.Replace(measurement, "");
                        removedMeasurement = measurement;
                    }
                    //Remove any parts with numbers in 
                    foreach (var part in parts)
                    {
                        if (part.Contains("0") || part.Contains("1") || part.Contains("2") || part.Contains("3") || part.Contains("4") || part.Contains("5") || part.Contains("6") || part.Contains("7") || part.Contains("8") || part.Contains("9"))
                        {
                            trimmedInfo = trimmedInfo.Replace(part, "");
                        }
                    }
                }
                trimmedInfo = trimmedInfo.Trim();
                Debug.WriteLine("Measurement should be " + parts[0] + " " + removedMeasurement);
                Debug.WriteLine("Ingredient should be " + trimmedInfo);

                double grams = 0;
                switch (removedMeasurement.Trim().ToLower())
                {
                    case "tsp":
                        double tsp = double.Parse(parts[0]);
                        grams = TeaspoonsToGrams(tsp);
                        break;
                    case "tbsp":
                        double tbsp = double.Parse(parts[0]);
                        grams = TablespoonsToGrams(tbsp);
                        break;
                    case "oz":
                        double oz = double.Parse(parts[0]);
                        grams = OuncesToGrams(oz);
                        break;
                    case "cup":
                        double cup = double.Parse(parts[0]);
                        grams = CupsToGrams(cup);
                        break;
                    case "pint":
                        double pint = double.Parse(parts[0]);
                        grams = PintsToGrams(pint);
                        break;
                    case "quart":
                        double quart = double.Parse(parts[0]);
                        grams = QuartsToGrams(quart);
                        break;
                    case "gallon":
                        double gallon = double.Parse(parts[0]);
                        grams = GallonsToGrams(gallon);
                        break;
                    default:
                        Debug.WriteLine("No Measurement Detected");
                        break;
                }

                Debug.WriteLineIf(grams != 0, "Grams: " + grams);
                var ings = contextLookup.GetFirstIngredient(trimmedInfo);
                ingredients.Add(ings);

            }

            return ingredients;
        }

        // Convert teaspoons to grams
        public static double TeaspoonsToGrams(double teaspoons)
        {
            return teaspoons * TeaspoonToGrams;
        }

        // Convert tablespoons to grams
        public static double TablespoonsToGrams(double tablespoons)
        {
            return tablespoons * TablespoonToGrams;
        }

        // Convert ounces to grams
        public static double OuncesToGrams(double ounces)
        {
            return ounces * OunceToGrams;
        }

        // Convert cups to grams
        public static double CupsToGrams(double cups)
        {
            return cups * CupToGrams;
        }

        // Convert pints to grams
        public static double PintsToGrams(double pints)
        {
            return pints * PintToGrams;
        }

        // Convert quarts to grams
        public static double QuartsToGrams(double quarts)
        {
            return quarts * QuartToGrams;
        }

        // Convert gallons to grams
        public static double GallonsToGrams(double gallons)
        {
            return gallons * GallonToGrams;
        }
    }
}
