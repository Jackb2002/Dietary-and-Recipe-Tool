using WinFormsInfoApp;
using WinFormsInfoApp.OpenFood;

namespace UnitTests
{
    [TestClass]
    public class OpenFoodAPITests
    {
        private readonly OpenFoodAPI _api = new();

        [TestMethod]
        public void GetIngredientsByName_Empty_String_Returns_Null()
        {
            var result = _api.GetIngredientsByName("");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetIngredientsByName_Whitespace_Returns_Null()
        {
            var result = _api.GetIngredientsByName("   ");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AccessString_Points_To_OpenFoodFacts_API()
        {
            Assert.IsTrue(_api.AccessString.Contains("openfoodfacts.org"));
        }

        [TestMethod]
        public void ConnectionType_Is_Remote()
        {
            Assert.AreEqual(IIngredientContext.ConnectionType.Remote, _api.connectionType);
        }

        [TestMethod]
        public void GetIngredientList_Empty_Input_Returns_Empty_List()
        {
            var result = _api.GetIngredientList([]);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
