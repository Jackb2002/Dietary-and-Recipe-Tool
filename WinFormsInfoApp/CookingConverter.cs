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
