namespace WinFormsInfoApp
{
    public class KitchenConverter
    {
        // Convert grams to teaspoons
        public static double GramsToTeaspoons(double grams)
        {
            return grams * 0.2; // 1 gram ≈ 0.2 teaspoons
        }

        // Convert teaspoons to grams
        public static double TeaspoonsToGrams(double teaspoons)
        {
            return teaspoons / 0.2; // 1 teaspoon ≈ 5 grams
        }

        // Convert grams to tablespoons
        public static double GramsToTablespoons(double grams)
        {
            return grams * 0.067628; // 1 gram ≈ 0.067628 tablespoons
        }

        // Convert tablespoons to grams
        public static double TablespoonsToGrams(double tablespoons)
        {
            return tablespoons / 0.067628; // 1 tablespoon ≈ 14.7868 grams
        }

        // Convert grams to ounces
        public static double GramsToOunces(double grams)
        {
            return grams * 0.035274; // 1 gram ≈ 0.035274 ounces
        }

        // Convert ounces to grams
        public static double OuncesToGrams(double ounces)
        {
            return ounces / 0.035274; // 1 ounce ≈ 28.3495 grams
        }

        // Convert grams to pounds
        public static double GramsToPounds(double grams)
        {
            return grams * 0.00220462; // 1 gram ≈ 0.00220462 pounds
        }

        // Convert pounds to grams
        public static double PoundsToGrams(double pounds)
        {
            return pounds / 0.00220462; // 1 pound ≈ 453.592 grams
        }

        // Convert grams to kilograms
        public static double GramsToKilograms(double grams)
        {
            return grams * 0.001; // 1 gram = 0.001 kilograms
        }

        // Convert kilograms to grams
        public static double KilogramsToGrams(double kilograms)
        {
            return kilograms / 0.001; // 1 kilogram = 1000 grams
        }


        public static double MillilitersToFluidOunces(double milliliters)
        {
            return milliliters * 0.033814; // 1 milliliter = 0.033814 fluid ounces (approximation)
        }

        // Convert fluid ounces to milliliters
        public static double FluidOuncesToMilliliters(double fluidOunces)
        {
            return fluidOunces / 0.033814; // 1 fluid ounce ≈ 29.5735 milliliters
        }

        // Convert milliliters to cups
        public static double MillilitersToCups(double milliliters)
        {
            return milliliters * 0.00422675; // 1 milliliter = 0.00422675 cups (approximation)
        }

        // Convert cups to milliliters
        public static double CupsToMilliliters(double cups)
        {
            return cups / 0.00422675; // 1 cup ≈ 236.588 milliliters
        }

        // Convert milliliters to pints
        public static double MillilitersToPints(double milliliters)
        {
            return milliliters * 0.00211338; // 1 milliliter = 0.00211338 pints (approximation)
        }

        // Convert pints to milliliters
        public static double PintsToMilliliters(double pints)
        {
            return pints / 0.00211338; // 1 pint ≈ 473.176 milliliters
        }

        // Convert milliliters to litres
        public static double MillilitersToLitres(double milliliters)
        {
            return milliliters * 0.001; // 1 milliliter = 0.001 litres
        }

        // Convert litres to milliliters
        public static double LitresToMilliliters(double litres)
        {
            return litres / 0.001; // 1 litre = 1000 milliliters
        }
    }

}
