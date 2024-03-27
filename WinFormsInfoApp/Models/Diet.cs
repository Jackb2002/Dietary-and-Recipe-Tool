using System.Text.Json.Serialization;

namespace WinFormsInfoApp.Models
{
    [JsonSerializable(typeof(Diet))]
    public class Diet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] PriorityPositive { get; set; }
        public string[] PriorityNegative { get; set; }

        public Diet(string name, string description, string[] priorityPos, string[] priorityNeg)
        {
            Name = name;
            Description = description;
            PriorityPositive = priorityPos;
            PriorityNegative = priorityNeg;
        }

        public static Diet[] ReturnDefaultDiets()
        {
            string[] balancedPositive = { "Kcal", "Protein", "Fibre" };
            string[] balancedNegative = { "Saturates", "Sugars", "Salt" };

            string[] lowCarbPositive = { "Protein", "Fibre" };
            string[] lowCarbNegative = { "Carbs", "Sugars" };

            string[] lowFatPositive = { "Protein", "Fibre" };
            string[] lowFatNegative = { "Fat", "Saturates" };

            string[] highProteinPositive = { "Protein", "Fibre" };
            string[] highProteinNegative = { "Carbs", "Sugars", "Fat", "Saturates" };

            string[] lowProPositive = { "Fibre" };
            string[] lowProNegative = { "Protein", "Saturates" };

            string[] highFibrePositive = { "Fibre" };
            string[] highFibreNegative = { "Carbs" };

            // Create instances of the Diet class for each diet
            Diet balancedDiet = new Diet("Balanced Diet", "A diet with balanced nutritional values",
                                         balancedPositive, balancedNegative);

            Diet lowCarbDiet = new Diet("Low Carb Diet", "A diet low in carbohydrates",
                                         lowCarbPositive, lowCarbNegative);

            Diet lowFatDiet = new Diet("Low Fat Diet", "A diet low in fat",
                                       lowFatPositive, lowFatNegative);

            Diet highProteinDiet = new Diet("High Protein Diet", "A diet high in protein",
                                            highProteinPositive, highProteinNegative);

            Diet lowProteinDiet = new Diet("Low Protein Diet", "A diet consisting only of low prottein and saturated fats",
                                      lowProPositive, lowProNegative);

            Diet highFibreDiet = new Diet("High Fibre Diet", "A diet containing lots of fibre",
                                           highFibrePositive, highFibreNegative);

            // Return the diets in an array
            return new Diet[] { balancedDiet, lowCarbDiet, lowFatDiet, highProteinDiet, lowProteinDiet, highFibreDiet };
        }
    }
}
