using System.Text.Json;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.Models
{
    public class JsonSerializerHelper
    {
        public void SerializeRecipes(List<Recipe> recipes, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(recipes);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Recipe> DeserializeRecipes(string filePath)
        {
            if(!File.Exists(filePath))
            {
                return new List<Recipe>();
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Recipe>>(jsonString);
        }

        public void SerializeIngredients(List<Ingredient> ingredients, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(ingredients);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Ingredient> DeserializeIngredients(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Ingredient>();
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Ingredient>>(jsonString);
        }
    }
}
