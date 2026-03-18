using WinFormsInfoApp;
using WinFormsInfoApp.LocalDatabase;
using WinFormsInfoApp.Models;

namespace UnitTests
{
    [TestClass]
    public class DatabaseManagerTests
    {
        private string _dbPath = string.Empty;
        private DatabaseManager _db = null!;

        [TestInitialize]
        public void Setup()
        {
            _dbPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.db");
            _db = new DatabaseManager(_dbPath);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }

        private static Ingredient MakeIngredient(string name = "Oats") =>
            new("0", name, "Test", fat: 7, carbohydrates: 60, protein: 12,
                calories: 380, sugar: 1, fibre: 8, product_Weight: 500);

        [TestMethod]
        public void TestConnection_Returns_True_When_File_Exists()
        {
            Assert.IsTrue(_db.TestConnection());
        }

        [TestMethod]
        public void TestConnection_Returns_False_When_File_Missing()
        {
            var missing = new DatabaseManager(Path.Combine(Path.GetTempPath(), "does_not_exist.db"));
            // The constructor creates the file — delete it to simulate missing
            File.Delete(Path.Combine(Path.GetTempPath(), "does_not_exist.db"));
            Assert.IsFalse(missing.TestConnection());
        }

        [TestMethod]
        public void InsertIngredient_Returns_True()
        {
            bool result = _db.InsertIngredient(MakeIngredient());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetIngredientsByName_Returns_Inserted_Ingredient()
        {
            _db.InsertIngredient(MakeIngredient("Quinoa"));

            var results = _db.GetIngredientsByName("Quinoa");

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("Quinoa", results[0]!.Name);
        }

        [TestMethod]
        public void GetIngredientsByName_Returns_Empty_For_Unknown_Name()
        {
            var results = _db.GetIngredientsByName("NoSuchIngredient");

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void GetIngredientsByName_Returns_Multiple_Matching_Rows()
        {
            _db.InsertIngredient(MakeIngredient("Rice"));
            _db.InsertIngredient(MakeIngredient("Rice"));

            var results = _db.GetIngredientsByName("Rice");

            Assert.AreEqual(2, results.Length);
        }

        [TestMethod]
        public void DeleteIngredient_Returns_True_And_Removes_Row()
        {
            _db.InsertIngredient(MakeIngredient("Barley"));
            var pairs = _db.GetIngredientNameIdPairs();
            var inserted = new Ingredient(pairs[0].Key.ToString(), "Barley", "Test",
                7, 60, 12, 380, 1, 8, 500);

            bool deleted = _db.DeleteIngredient(inserted);

            Assert.IsTrue(deleted);
            Assert.AreEqual(0, _db.GetIngredientsByName("Barley").Length);
        }

        [TestMethod]
        public void GetIngredientNameIdPairs_Returns_All_Inserted_Rows()
        {
            _db.InsertIngredient(MakeIngredient("Millet"));
            _db.InsertIngredient(MakeIngredient("Spelt"));

            var pairs = _db.GetIngredientNameIdPairs();

            Assert.AreEqual(2, pairs.Length);
            CollectionAssert.Contains(pairs.Select(p => p.Value).ToArray(), "Millet");
            CollectionAssert.Contains(pairs.Select(p => p.Value).ToArray(), "Spelt");
        }

        [TestMethod]
        public void GetIngredientList_Returns_Results_For_Each_Name()
        {
            _db.InsertIngredient(MakeIngredient("Lentils"));
            _db.InsertIngredient(MakeIngredient("Chickpeas"));

            var list = _db.GetIngredientList(["Lentils", "Chickpeas"]);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0].Length);
            Assert.AreEqual(1, list[1].Length);
        }

        [TestMethod]
        public void GetIngredientList_Returns_Empty_List_For_No_Matches()
        {
            var list = _db.GetIngredientList(["GhostIngredient"]);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(0, list[0].Length);
        }

        [TestMethod]
        public void ConnectionType_Is_Local()
        {
            Assert.AreEqual(IIngredientContext.ConnectionType.Local, _db.connectionType);
        }
    }
}
