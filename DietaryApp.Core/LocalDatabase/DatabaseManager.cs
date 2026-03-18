using Microsoft.Data.Sqlite;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.LocalDatabase
{
    public class DatabaseManager : IIngredientContext
    {
        public string AccessString { get; private set; }

        public IIngredientContext.ConnectionType connectionType => IIngredientContext.ConnectionType.Local;

        public DatabaseManager(string path)
        {
            AccessString = path;
            CheckOrCreateIngredientTable();
        }

        #region Ingredient
        private void CheckOrCreateIngredientTable()
        {
            using SqliteConnection connection = new($"Data Source={AccessString}");
            connection.Open();
            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Ingredient (IngredientId INTEGER PRIMARY KEY, Name TEXT, Description TEXT, Fat REAL, Carbohydrates REAL, Protein REAL, Calories REAL, Sugar REAL, Fibre REAL, Product_Weight REAL)";
            command.ExecuteNonQuery();
        }

        public bool InsertIngredient(Ingredient ingredient)
        {
            int rows = 0;
            using (SqliteConnection connection = new($"Data Source={AccessString}"))
            {
                connection.Open();
                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Ingredient (Name, Description, Fat, Carbohydrates, Protein, Calories, Sugar, Fibre, Product_Weight) VALUES (@Name, @Description, @Fat, @Carbohydrates, @Protein, @Calories, @Sugar, @Fibre, @Product_Weight)";
                command.Parameters.AddWithValue("@Name", ingredient.Name);
                command.Parameters.AddWithValue("@Description", ingredient.Description);
                command.Parameters.AddWithValue("@Fat", ingredient.Fat);
                command.Parameters.AddWithValue("@Carbohydrates", ingredient.Carbohydrates);
                command.Parameters.AddWithValue("@Protein", ingredient.Protein);
                command.Parameters.AddWithValue("@Calories", ingredient.Calories);
                command.Parameters.AddWithValue("@Sugar", ingredient.Sugar);
                command.Parameters.AddWithValue("@Fibre", ingredient.Fibre);
                command.Parameters.AddWithValue("@Product_Weight", ingredient.Product_Weight);
                rows = command.ExecuteNonQuery();
            }
            return rows > 0;
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            int rows = 0;
            using (SqliteConnection connection = new($"Data Source={AccessString}"))
            {
                connection.Open();
                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Ingredient WHERE IngredientId = @Id";
                command.Parameters.AddWithValue("@Id", ingredient.IngredientId);
                rows = command.ExecuteNonQuery();
            }
            return rows > 0;
        }

        public KeyValuePair<int, string>[] GetIngredientNameIdPairs()
        {
            var pairs = Array.Empty<KeyValuePair<int, string>>();
            using (SqliteConnection connection = new($"Data Source={AccessString}"))
            {
                connection.Open();
                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT IngredientId, Name FROM Ingredient";
                using SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Array.Resize(ref pairs, pairs.Length + 1);
                    pairs[^1] = new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1));
                }
            }
            return pairs;
        }

        public bool TestConnection()
        {
            return File.Exists(AccessString);
        }

        public Ingredient?[] GetIngredientsByName(string name, string location = "All")
        {
            var ingredientList = new List<Ingredient>();
            using (SqliteConnection connection = new($"Data Source={AccessString}"))
            {
                connection.Open();
                using SqliteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Ingredient WHERE Name = @Name";
                command.Parameters.AddWithValue("@Name", name);
                using SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ingredientList.Add(new Ingredient(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDouble(4),
                        reader.GetDouble(5),
                        reader.GetDouble(3),
                        reader.GetDouble(6),
                        reader.GetDouble(7),
                        reader.GetDouble(8),
                        reader.GetDouble(9)
                    ));
                }
            }
            return ingredientList.ToArray();
        }

        public List<Ingredient?[]> GetIngredientList(string[] ingredients, string location = "All")
        {
            List<Ingredient?[]> ings = [];
            foreach (string ingName in ingredients)
            {
                ings.Add(GetIngredientsByName(ingName));
            }
            return ings;
        }
        #endregion
    }
}
