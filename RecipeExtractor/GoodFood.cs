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
                var nutritionInfo = ExtractNutritionInfo(doc);
                string method = ExtractMethod(doc);
                string ingredients = ExtractIngredients(doc);
                bool dairyFree= allergyInfo.Contains("Dairy");
                bool glutenFree = allergyInfo.Contains("Gluten");
                bool vegetarian = allergyInfo.Contains("Vegetarian");
                bool keto = allergyInfo.Contains("Keto");
                bool vegan = allergyInfo.Contains("Vegan");
                int kcal = nutritionInfo.Where(x => x.Key.Contains("kcal")).Select(x => x.Value).FirstOrDefault();
                int fat = nutritionInfo.Where(x => x.Key.Contains("fat")).Select(x => x.Value).FirstOrDefault();
                int saturates = nutritionInfo.Where(x => x.Key.Contains("saturates")).Select(x => x.Value).FirstOrDefault();
                int carbs = nutritionInfo.Where(x => x.Key.Contains("carbs")).Select(x => x.Value).FirstOrDefault();
                int sugars = nutritionInfo.Where(x => x.Key.Contains("sugars")).Select(x => x.Value).FirstOrDefault();
                int fibre = nutritionInfo.Where(x => x.Key.Contains("fibre")).Select(x => x.Value).FirstOrDefault();
                int protein = nutritionInfo.Where(x => x.Key.Contains("protein")).Select(x => x.Value).FirstOrDefault();
                int salt = nutritionInfo.Where(x => x.Key.Contains("salt")).Select(x => x.Value).FirstOrDefault();

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
                    method 
                    
                };

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

        private static List<KeyValuePair<string,int>> ExtractNutritionInfo(HtmlDocument doc)
        {
            List<KeyValuePair<string, int>> nutritionInfo = new();
            try
            {
                var nutritionNodes = doc.DocumentNode.SelectNodes("//tbody[@class='key-value-blocks__batch body-copy-extra-small']");
                if (nutritionNodes != null)
                {
                    var nutInf = nutritionNodes.Select(node =>
                    {
                        string nutrientNode = node.SelectSingleNode("./tr[1]/td[2]").InnerText;
                        int.TryParse(node.SelectSingleNode("./tr[1]/td[3]").InnerText, out int valueNode);
                        try
                        {
                            return new KeyValuePair<string, int>(nutrientNode, valueNode);
                        }
                        catch
                        {
                            return new KeyValuePair<string, int>("Not specified", 0);
                        }
                    });
                    nutritionInfo.AddRange(nutInf);
                }
            return nutritionInfo;
            }
            catch
            {
                return nutritionInfo;
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
