using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeExtractor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ExtractorTest
    {
        [TestMethod]
        public void ParseRecipeFromUrl_Returns_RecipeData()
        {
            // Arrange
            string url = @"https://www.bbcgoodfood.com/recipes/satay-sweet-potato-curry";

            // Act
            var recipeData = GoodFood.ParseRecipeFromUrl(url);

            // Assert
            Assert.IsNotNull(recipeData);
            Assert.IsTrue(recipeData.Count > 0);
            // Add more specific assertions based on the expected structure of recipe data
        }

        // Add more test methods to cover other scenarios if needed
    }
}
