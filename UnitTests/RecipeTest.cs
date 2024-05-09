using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WinFormsInfoApp.Models;

namespace UnitTests
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void GenerateMaxValues_Sets_Max_Values_Correctly()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Kcal = 100, Fat = 10, Saturates = 5, Carbs = 20, Sugars = 15, Fibre = 3, Protein = 8, Salt = 2 },
                new Recipe { Kcal = 200, Fat = 15, Saturates = 6, Carbs = 25, Sugars = 20, Fibre = 5, Protein = 10, Salt = 3 },
                new Recipe { Kcal = 150, Fat = 12, Saturates = 4, Carbs = 22, Sugars = 18, Fibre = 4, Protein = 9, Salt = 2.5f }
            };

            // Act
            Recipe.GenerateMaxValues(recipes);

            // Assert
            Assert.AreEqual(200, Recipe.MAX_KCAL);
            Assert.AreEqual(15, Recipe.MAX_FAT);
            Assert.AreEqual(6, Recipe.MAX_SATURATES);
            Assert.AreEqual(25, Recipe.MAX_CARBS);
            Assert.AreEqual(20, Recipe.MAX_SUGARS);
            Assert.AreEqual(5, Recipe.MAX_FIBRE);
            Assert.AreEqual(10, Recipe.MAX_PROTEIN);
            Assert.AreEqual(3, Recipe.MAX_SALT);
        }

        // Add more test methods to cover other scenarios if needed
    }
}
