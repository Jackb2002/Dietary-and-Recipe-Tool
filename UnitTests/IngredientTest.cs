using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsInfoApp.Models;

namespace UnitTests
{
    [TestClass]
    public class IngredientTests
    {
        [TestMethod]
        public void Constructor_Initializes_Properties_Correctly()
        {
            // Arrange
            string ingredientId = "123";
            string name = "Test Ingredient";
            string description = "Test Description";
            double fat = 10.5;
            double carbohydrates = 20.3;
            double protein = 5.7;
            double calories = 150.2;
            double sugar = 15.1;
            double fibre = 3.8;
            double productWeight = 200;

            // Act
            var ingredient = new Ingredient(ingredientId, name, description, fat, carbohydrates, protein, calories, sugar, fibre, productWeight);

            // Assert
            Assert.AreEqual(ingredientId, ingredient.IngredientId);
            Assert.AreEqual(name, ingredient.Name);
            Assert.AreEqual(description, ingredient.Description);
            Assert.AreEqual(fat, ingredient.Fat);
            Assert.AreEqual(carbohydrates, ingredient.Carbohydrates);
            Assert.AreEqual(protein, ingredient.Protein);
            Assert.AreEqual(calories, ingredient.Calories);
            Assert.AreEqual(sugar, ingredient.Sugar);
            Assert.AreEqual(fibre, ingredient.Fibre);
            Assert.AreEqual(productWeight, ingredient.Product_Weight);
        }
    }
}
