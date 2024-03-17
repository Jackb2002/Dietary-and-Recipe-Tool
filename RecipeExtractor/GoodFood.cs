using HtmlAgilityPack;
using Microsoft.VisualBasic;
using System;
using System.Linq;
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
                string rawHtml = doc.DocumentNode.OuterHtml;
                File.WriteAllText("goodfood.html", rawHtml);
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
            var nameNode = doc.DocumentNode.SelectSingleNode("//h1[@class='heading-1']");
            return nameNode?.InnerText.Trim();
        }

        private static string ExtractRecipeDescription(HtmlDocument doc)
        {
            var descriptionNode = doc.DocumentNode.SelectSingleNode("//div[@class='editor-content mt-sm pr-xxs hidden-print']/p");
            return descriptionNode?.InnerText.Trim();
        }

        private static double ExtractRecipeRating(HtmlDocument doc)
        {
            var ratingNode = doc.DocumentNode.SelectSingleNode("//div[@class='rating__values']");
            var stars = ratingNode?.SelectNodes(".//i[contains(@class, 'rating__icon')]") ?? new HtmlNodeCollection(null);

            // Calculate the average rating based on the filled stars
            double totalStars = stars.Count;
            double filledStars = stars.Count(icon => icon.Attributes["style"].Value.Contains("fill"));
            double rating = filledStars / totalStars * 5; // Assuming a 5-star rating system

            return rating;
        }

        private static string ExtractCookTime(HtmlDocument doc)
        {
            var timeNode = doc.DocumentNode.SelectSingleNode("//ul[@class='recipe__cook-and-prep list list--horizontal']/li/div/ul/li[2]/span/time");
            return timeNode?.InnerText.Trim();
        }

        private static string ExtractDifficulty(HtmlDocument doc)
        {
            var difficultyNode = doc.DocumentNode.SelectSingleNode("//li[@class='mt-sm mr-xl list-item']/div/div[contains(text(), 'Difficulty')]");
            return difficultyNode?.NextSibling.InnerText.Trim();
        }

        private static string ExtractAllergyInfo(HtmlDocument doc)
        {
            var allergyNodes = doc.DocumentNode.SelectNodes("//div[@class='allergy-info-container']/ul/li/span");
            if (allergyNodes != null)
            {
                return string.Join(", ", allergyNodes.Select(node => node.InnerText.Trim()));
            }
            return "Not specified";
        }

        private static string ExtractNutritionInfo(HtmlDocument doc)
        {
            var nutritionNodes = doc.DocumentNode.SelectNodes("//tbody[@class='key-value-blocks__batch body-copy-extra-small']");
            if (nutritionNodes != null)
            {
                var nutritionInfo = nutritionNodes.Select(node => $"{node.SelectSingleNode("./tr[1]/td[2]").InnerText.Trim()}: {node.SelectSingleNode("./tr[1]/td[3]").InnerText.Trim()}");
                return string.Join(", ", nutritionInfo);
            }
            return "Not specified";
        }

        private static string ExtractMethod(HtmlDocument doc)
        {
            var methodNodes = doc.DocumentNode.SelectNodes("//div[@class='js-piano-recipe-method col-12 pa-reset col-lg-6']//div[@class='grouped-list']//ul[@class='grouped-list__list list']//li[@class='pb-xs pt-xs list-item']");
            if (methodNodes != null)
            {
                return string.Join("\n", methodNodes.Select(node => $"{node.SelectSingleNode(".//span[@class='mb-xxs heading-6']")?.InnerText.Trim()}: {node.SelectSingleNode("./div[@class='editor-content']")?.InnerText.Trim()}"));
            }
            return "Not specified";
        }

        private static string ExtractIngredients(HtmlDocument doc)
        {
            var ingredientNodes = doc.DocumentNode.SelectNodes("//section[@class='recipe__ingredients col-12 mt-md col-lg-6']//ul[@class='list']//li[@class='pb-xxs pt-xxs list-item list-item--separator']");
            if (ingredientNodes != null)
            {
                return string.Join("\n", ingredientNodes.Select(node => node.InnerText.Trim()));
            }
            return "Not specified";
        }

    }
}
