using WinFormsInfoApp.Models;

namespace UnitTests
{
    [TestClass]
    public class DietsTest
    {
        [TestMethod]
        public void Constructor_Initializes_Properties_Correctly()
        {
            // Arrange
            string name = "Test Diet";
            string description = "Test Description";
            string[] priorityPositive = { "TestPriority1", "TestPriority2" };
            string[] priorityNegative = { "TestNegativePriority1", "TestNegativePriority2" };
            DateTime startDate = DateTime.Now;

            // Act
            Diet diet = new Diet(name, description, priorityPositive, priorityNegative, startDate);

            // Assert
            Assert.AreEqual(name, diet.Name);
            Assert.AreEqual(description, diet.Description);
            CollectionAssert.AreEqual(priorityPositive, diet.PriorityPositive);
            CollectionAssert.AreEqual(priorityNegative, diet.PriorityNegative);
            Assert.AreEqual(startDate, diet.StartDate);
            Assert.IsFalse(diet.InUse);
            Assert.IsFalse(diet.DefaultDiet);
        }

        [TestMethod]
        public void ToString_Returns_Expected_String()
        {
            // Arrange
            Diet diet = new Diet("Test Diet", "Test Description", new string[] { "TestPriority" }, new string[] { "TestNegativePriority" }, DateTime.Now);

            // Act
            string result = diet.ToString();

            // Assert
            Assert.AreEqual("Test Diet - Test Description", result);
        }

        [TestMethod]
        public void ReturnDefaultDiets_Returns_Expected_Diets()
        {
            // Arrange & Act
            Diet[] defaultDiets = Diet.ReturnDefaultDiets();

            // Assert
            Assert.IsNotNull(defaultDiets);
            Assert.AreEqual(6, defaultDiets.Length);
        }

        // Similarly, you can write tests for other methods like GenerateMeals, GenerateRankedRecipes, GetNormalizedPropertyValue, and Edge Cases tests.
    }
}
