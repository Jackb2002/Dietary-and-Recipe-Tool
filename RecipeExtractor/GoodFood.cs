using HtmlAgilityPack;

namespace RecipeExtractor
{
    public class GoodFood
    {
        public static List<KeyValuePair<string, object>>? ParseRecipeFromUrl(string url)
        {
            try
            {
                HtmlWeb web = new();
                HtmlDocument doc = web.Load(url);

                string name = ExtractRecipeName(doc);
                string description = ExtractRecipeDescription(doc);
                double rating = ExtractRecipeRating(doc);
                string cookTime = ExtractCookTime(doc);
                string difficulty = ExtractDifficulty(doc);
                string allergyInfo = ExtractAllergyInfo(doc);
                List<KeyValuePair<string, string>> nutritionInfo = ExtractNutritionInfo(doc);
                string method = ExtractMethod(doc);
                string ingredients = ExtractIngredients(doc);
                bool dairyFree = allergyInfo.Contains("Dairy");
                bool glutenFree = allergyInfo.Contains("Gluten");
                bool vegetarian = allergyInfo.Contains("Vegetarian");
                bool keto = allergyInfo.Contains("Keto");
                bool vegan = allergyInfo.Contains("Vegan");
                string? kcal = nutritionInfo.FirstOrDefault(x => x.Key.Contains("kcal")).Value;
                if (kcal != default)
                {
                    kcal = kcal.Replace("g", "") ?? "";
                }

                string? fat = nutritionInfo.FirstOrDefault(x => x.Key.Contains("fat")).Value;
                if (fat != default)
                {
                    fat = fat.Replace("g", "") ?? "";
                }

                string? saturates = nutritionInfo.FirstOrDefault(x => x.Key.Contains("saturates")).Value;
                if (saturates != default)
                {
                    saturates = saturates.Replace("g", "") ?? "";
                }

                string? carbs = nutritionInfo.FirstOrDefault(x => x.Key.Contains("carbs")).Value;
                if (carbs != default)
                {
                    carbs = carbs.Replace("g", "") ?? "";
                }

                string? sugars = nutritionInfo.FirstOrDefault(x => x.Key.Contains("sugars")).Value;
                if (sugars != default)
                {
                    sugars = sugars.Replace("g", "") ?? "";
                }

                string? fibre = nutritionInfo.FirstOrDefault(x => x.Key.Contains("fibre")).Value;
                if (fibre != default)
                {
                    fibre = fibre.Replace("g", "") ?? "";
                }

                string? protein = nutritionInfo.FirstOrDefault(x => x.Key.Contains("protein")).Value;
                if (protein != default)
                {
                    protein = protein.Replace("g", "") ?? "";
                }

                string? salt = nutritionInfo.FirstOrDefault(x => x.Key.Contains("salt")).Value;
                if (salt != default)
                {
                    salt = salt.Replace("g", "") ?? "";
                }

                List<KeyValuePair<string, object>> recipeData =
        [
            new KeyValuePair<string, object>("Name", name),
            new KeyValuePair<string, object>("Description", description),
            new KeyValuePair<string, object>("Rating", rating),
            new KeyValuePair<string, object>("CookTime", cookTime),
            new KeyValuePair<string, object>("Difficulty", difficulty),
            new KeyValuePair<string, object>("AllergyInfo", allergyInfo),
            new KeyValuePair<string, object>("DairyFree", dairyFree),
            new KeyValuePair<string, object>("GlutenFree", glutenFree),
            new KeyValuePair<string, object>("Vegetarian", vegetarian),
            new KeyValuePair<string, object>("Keto", keto),
            new KeyValuePair<string, object>("Vegan", vegan),
            new KeyValuePair<string, object>("Kcal", kcal),
            new KeyValuePair<string, object>("Fat", fat),
            new KeyValuePair<string, object>("Saturates", saturates),
            new KeyValuePair<string, object>("Carbs", carbs),
            new KeyValuePair<string, object>("Sugars", sugars),
            new KeyValuePair<string, object>("Fibre", fibre),
            new KeyValuePair<string, object>("Protein", protein),
            new KeyValuePair<string, object>("Salt", salt),
            new KeyValuePair<string, object>("Ingredients", ingredients),
            new KeyValuePair<string, object>("Url", url),
            new KeyValuePair<string, object>("Method", method)
        ];

                return recipeData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing recipe from URL: " + ex.Message);
                return null;
            }
        }

        private static string ExtractRecipeName(HtmlDocument doc)
        {
            try
            {
                HtmlNode nameNode = doc.DocumentNode.SelectSingleNode("//h1[@class='heading-1']");
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
                HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//div[@class='editor-content mt-sm pr-xxs hidden-print']/p");
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
                HtmlNode ratingNode = doc.DocumentNode.SelectSingleNode("//div[@class='rating__values']");
                HtmlNodeCollection stars = ratingNode?.SelectNodes(".//i[contains(@class, 'rating__icon')]") ?? new HtmlNodeCollection(null);

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
                HtmlNode timeNode = doc.DocumentNode.SelectSingleNode("//ul[@class='recipe__cook-and-prep list list--horizontal']/li/div/ul/li[2]/span/time");
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
                HtmlNode difficultyNode = doc.DocumentNode.SelectSingleNode("//li[@class='mt-sm mr-xl list-item']/div/div[contains(text(), 'Difficulty')]");
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
                HtmlNodeCollection allergyNodes = doc.DocumentNode.SelectNodes("//div[@class='allergy-info-container']/ul/li/span");
                return allergyNodes != null
                    ? string.Join(", ", allergyNodes.Select(node => node.InnerText.Trim())).Replace("&amp", "&")
                    : "Not specified";
            }
            catch
            {
                return string.Empty;
            }
        }

        private static List<KeyValuePair<string, string>> ExtractNutritionInfo(HtmlDocument doc)
        {
            List<KeyValuePair<string, string>> nutritionInfo = [];

            try
            {
                HtmlNodeCollection nutritionNodes = doc.DocumentNode.SelectNodes("//tbody[@class='key-value-blocks__batch body-copy-extra-small']");
                if (nutritionNodes != null)
                {
                    foreach (HtmlNode node in nutritionNodes)
                    {
                        HtmlNodeCollection rows = node.SelectNodes("./tr");
                        if (rows != null)
                        {
                            foreach (HtmlNode row in rows)
                            {
                                string nutrient = row.SelectSingleNode("./td[1]").InnerText.Trim();
                                string value = row.SelectSingleNode("./td[2]").InnerText.Trim();

                                // Remove any comments (e.g., <!-- -->) from the value
                                value = System.Text.RegularExpressions.Regex.Replace(value, "<!--(.*?)-->", "").Trim();

                                nutritionInfo.Add(new KeyValuePair<string, string>(nutrient, value));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while extracting nutrition info: " + ex.Message);
            }

            return nutritionInfo;
        }



        private static string ExtractMethod(HtmlDocument doc)
        {
            try
            {
                HtmlNodeCollection methodNodes = doc.DocumentNode.SelectNodes("//div[@class='js-piano-recipe-method col-12 pa-reset col-lg-6']//div[@class='grouped-list']//ul[@class='grouped-list__list list']//li[@class='pb-xs pt-xs list-item']");
                return methodNodes != null
                    ? string.Join("\n", methodNodes.Select(node => $"{node.SelectSingleNode(".//span[@class='mb-xxs heading-6']")?.InnerText.Trim()}: {node.SelectSingleNode("./div[@class='editor-content']")?.InnerText.Trim()}")).Replace("&amp", "&")
                    : "Not specified";
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
                HtmlNodeCollection ingredientNodes = doc.DocumentNode.SelectNodes("//section[@class='recipe__ingredients col-12 mt-md col-lg-6']//ul[@class='list']//li[@class='pb-xxs pt-xxs list-item list-item--separator']");
                return ingredientNodes != null
                    ? string.Join("\n", ingredientNodes.Select(node => node.InnerText.Trim())).Replace("&amp", "&")
                    : "Not specified";
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
