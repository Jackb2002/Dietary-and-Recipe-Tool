using System.Text.Json;

namespace WinFormsInfoApp.Models
{
    public class JsonSerializerHelper
    {
        public void SerializeRecipes(List<Recipe> recipes, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(recipes);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Recipe>? DeserializeRecipes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return [];
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Recipe>>(jsonString);
        }

        public void SerializeIngredients(List<Ingredient> ingredients, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(ingredients);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Ingredient>? DeserializeIngredients(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return [];
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Ingredient>>(jsonString);
        }

        public void SerializeDiets(List<Diet> diets, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(diets);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Diet>? DeserializeDiets(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return [];
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Diet>>(jsonString);
        }
    }
}
