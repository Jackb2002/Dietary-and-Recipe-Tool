using HtmlAgilityPack;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RecipeExtractor
{
    public class GoodFood
    {
        public static string[]? ParseRecipeFromUrl(string url)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);

                string name = ExtractRecipeName(doc);
                string description = ExtractRecipeDescription(doc);
                double rating = ExtractRecipeRating(doc);
                string cookTime = ExtractCookTime(doc);
                string difficulty = ExtractDifficulty(doc);
                string allergyInfo = ExtractAllergyInfo(doc);
                string nutritionInfo = ExtractNutritionInfo(doc);
                string method = ExtractMethod(doc);
                string ingredients = ExtractIngredients(doc);
                bool dairyFree= allergyInfo.Contains("Dairy");
                bool glutenFree = allergyInfo.Contains("Gluten");
                bool vegetarian = allergyInfo.Contains("Vegetarian");
                bool keto = allergyInfo.Contains("Keto");
                bool vegan = allergyInfo.Contains("Vegan");

                string[] recipe = new string[]{
                    name,
                    difficulty,
                    "",
                    rating.ToString(),
                    "",
                    vegetarian.ToString(),
                    vegan.ToString(),
                    dairyFree.ToString(),
                    keto.ToString(),
                    glutenFree.ToString(),
                    "",
                    cookTime,
                    ingredients,
                    url,
                    method };

                return recipe;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private static string ExtractRecipeName(HtmlDocument doc)
        {
            try
            {
                var nameNode = doc.DocumentNode.SelectSingleNode("//h1[@class='heading-1']");
                return nameNode?.InnerText.Trim().Replace("&amp", "&") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ExtractRecipeDescription(HtmlDocument doc)
        {
            try
            {
                var descriptionNode = doc.DocumentNode.SelectSingleNode("//div[@class='editor-content mt-sm pr-xxs hidden-print']/p");
                return descriptionNode?.InnerText.Trim().Replace("&amp", "&") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static double ExtractRecipeRating(HtmlDocument doc)
        {
            try
            {
                var ratingNode = doc.DocumentNode.SelectSingleNode("//div[@class='rating__values']");
                var stars = ratingNode?.SelectNodes(".//i[contains(@class, 'rating__icon')]") ?? new HtmlNodeCollection(null);

                // Calculate the average rating based on the filled stars
                double totalStars = stars.Count;
                double filledStars = stars.Count(icon => icon.Attributes["style"].Value.Contains("fill"));
                double rating = filledStars / totalStars * 5; // Assuming a 5-star rating system

                return rating;
            }
            catch
            {
                return 0; // Or any other default value you prefer
            }
        }

        private static string ExtractCookTime(HtmlDocument doc)
        {
            try
            {
                var timeNode = doc.DocumentNode.SelectSingleNode("//ul[@class='recipe__cook-and-prep list list--horizontal']/li/div/ul/li[2]/span/time");
                return timeNode?.InnerText.Trim().Replace("&amp", "&") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ExtractDifficulty(HtmlDocument doc)
        {
            try
            {
                var difficultyNode = doc.DocumentNode.SelectSingleNode("//li[@class='mt-sm mr-xl list-item']/div/div[contains(text(), 'Difficulty')]");
                return difficultyNode?.NextSibling.InnerText.Trim().Replace("&amp", "&") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ExtractAllergyInfo(HtmlDocument doc)
        {
            try
            {
                var allergyNodes = doc.DocumentNode.SelectNodes("//div[@class='allergy-info-container']/ul/li/span");
                if (allergyNodes != null)
                {
                    return string.Join(", ", allergyNodes.Select(node => node.InnerText.Trim())).Replace("&amp", "&");
                }
                return "Not specified";
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ExtractNutritionInfo(HtmlDocument doc)
        {
            try
            {
                var nutritionNodes = doc.DocumentNode.SelectNodes("//tbody[@class='key-value-blocks__batch body-copy-extra-small']");
                if (nutritionNodes != null)
                {
                    var nutritionInfo = nutritionNodes.Select(node =>
                    {
                        var nutrientNode = node.SelectSingleNode("./tr[1]/td[2]");
                        var valueNode = node.SelectSingleNode("./tr[1]/td[3]");
                        if (nutrientNode != null && valueNode != null)
                        {
                            return $"{nutrientNode.InnerText.Trim()}: {valueNode.InnerText.Trim()}";
                        }
                        return null;
                    }).Where(info => info != null);

                    return string.Join(", ", nutritionInfo).Replace("&amp", "&");
                }
                return "Not specified";
            }
            catch
            {
                return string.Empty;
            }
        }


        private static string ExtractMethod(HtmlDocument doc)
        {
            try
            {
                var methodNodes = doc.DocumentNode.SelectNodes("//div[@class='js-piano-recipe-method col-12 pa-reset col-lg-6']//div[@class='grouped-list']//ul[@class='grouped-list__list list']//li[@class='pb-xs pt-xs list-item']");
                if (methodNodes != null)
                {
                    return string.Join("\n", methodNodes.Select(node => $"{node.SelectSingleNode(".//span[@class='mb-xxs heading-6']")?.InnerText.Trim()}: {node.SelectSingleNode("./div[@class='editor-content']")?.InnerText.Trim()}")).Replace("&amp", "&");
                }
                return "Not specified";
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ExtractIngredients(HtmlDocument doc)
        {
            try
            {
                var ingredientNodes = doc.DocumentNode.SelectNodes("//section[@class='recipe__ingredients col-12 mt-md col-lg-6']//ul[@class='list']//li[@class='pb-xxs pt-xxs list-item list-item--separator']");
                if (ingredientNodes != null)
                {
                    return string.Join("\n", ingredientNodes.Select(node => node.InnerText.Trim())).Replace("&amp", "&");
                }
                return "Not specified";
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
