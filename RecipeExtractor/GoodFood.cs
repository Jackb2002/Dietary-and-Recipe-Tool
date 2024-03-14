using HtmlAgilityPack;
using System.Text;

namespace RecipeExtractor
{
    public class GoodFood
    {
        public static string ParseRecipeFromUrl(string url)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                string name = ExtractRecipeName(doc);
                string description = ExtractRecipeDescription(doc);
                int rating = ExtractRecipeRating(doc);
                string cookTime = ExtractCookTime(doc);
                string difficulty = ExtractDifficulty(doc);
                string allergyInfo = ExtractAllergyInfo(doc);
                string nutritionInfo = ExtractNutritionInfo(doc);
                string method = ExtractMethod(doc);
                string ingredients = ExtractIngredients(doc);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Name: {name}");
                sb.AppendLine($"Description: {description}");
                sb.AppendLine($"Rating: {rating}");
                sb.AppendLine($"Cook time: {cookTime}");
                sb.AppendLine($"Difficulty: {difficulty}");
                sb.AppendLine($"Allergy info: {allergyInfo}");
                sb.AppendLine($"Nutrition info: {nutritionInfo}");
                sb.AppendLine($"Method: {method}");
                sb.AppendLine($"Ingredients: {ingredients}");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        private static string ExtractRecipeName(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//h1[@class='heading-1']").InnerText.Trim();
        }

        private static string ExtractRecipeDescription(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//div[@class='editor-content']").InnerText.Trim();
        }

        private static int ExtractRecipeRating(HtmlDocument doc)
        {
            var ratingNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'rating__count-text')]");
            if (ratingNode != null)
            {
                string ratingText = ratingNode.InnerText.Trim();
                if (int.TryParse(ratingText.Split(' ')[0], out int rating))
                {
                    return rating;
                }
            }
            return 0;
        }

        private static string ExtractCookTime(HtmlDocument doc)
        {
            var prepTimeNode = doc.DocumentNode.SelectSingleNode("//time[@datetime]");
            var cookTimeNode = prepTimeNode?.NextSibling;
            if (cookTimeNode != null)
            {
                return cookTimeNode.InnerText.Trim();
            }
            return string.Empty;
        }

        private static string? ExtractDifficulty(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'icon-with-text__children')][contains(text(),'Easy')]")?.InnerText;
        }

        private static string? ExtractAllergyInfo(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//span[contains(text(),'High-fibre')]")?.InnerText;
        }

        private static string? ExtractNutritionInfo(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//table[@class='key-value-blocks']//tbody")?.InnerText;
        }

        private static string? ExtractMethod(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//div[@class='editor-content']")?.InnerText;
        }

        private static string ExtractIngredients(HtmlDocument doc)
        {
            var ingredientNodes = doc.DocumentNode.SelectNodes("//ul[@class='recipe-ingredients__list']//li");
            if (ingredientNodes != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var ingredientNode in ingredientNodes)
                {
                    string ingredient = ingredientNode.InnerText.Trim();
                    sb.AppendLine(ingredient);
                }
                return sb.ToString();
            }
            return "Ingredients: not found";
        }

    }
}
