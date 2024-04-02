using System.Data.SQLite;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.LocalDatabase
{
    internal class DatabaseManager : IIngredientContext
    {
        public string AccessString { get; private set; }

        public IIngredientContext.ConnectionType connectionType => IIngredientContext.ConnectionType.Local;


        // Constructor
        public DatabaseManager(string path)
        {
            AccessString = path;

            CheckOrCreateIngredientTable();
        }

        #region Ingredient
        private void CheckOrCreateIngredientTable()
        {
            using SQLiteConnection connection = new("Data Source=" + AccessString);
            connection.Open();
            using (SQLiteCommand command = new(connection))
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Ingredient (IngredientId INTEGER PRIMARY KEY, Name TEXT, Description TEXT, Fat REAL, Carbohydrates REAL, Protein REAL, Calories REAL, Sugar REAL, Fibre REAL, Product_Weight REAL)";
                _ = command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public bool InsertIngredient(Ingredient ingredient)
        {
            _ = ingredient.IngredientId;
            string Name = ingredient.Name;
            string Description = ingredient.Description;
            double Fat = ingredient.Fat;
            double Carbohydrates = ingredient.Carbohydrates;
            double Protein = ingredient.Protein;
            double Calories = ingredient.Calories;
            double Sugar = ingredient.Sugar;
            double Fibre = ingredient.Fibre;
            double Product_Weight = ingredient.Product_Weight;
            int rows = 0;
            using (SQLiteConnection connection = new("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = "INSERT INTO Ingredient (Name, Description, Fat, Carbohydrates, Protein, Calories, Sugar, Fibre, Product_Weight) VALUES (@Name, @Description, @Fat, @Carbohydrates, @Protein, @Calories, @Sugar, @Fibre, @Product_Weight)";
                    _ = command.Parameters.AddWithValue("@Name", Name);
                    _ = command.Parameters.AddWithValue("@Description", Description);
                    _ = command.Parameters.AddWithValue("@Fat", Fat);
                    _ = command.Parameters.AddWithValue("@Carbohydrates", Carbohydrates);
                    _ = command.Parameters.AddWithValue("@Protein", Protein);
                    _ = command.Parameters.AddWithValue("@Calories", Calories);
                    _ = command.Parameters.AddWithValue("@Sugar", Sugar);
                    _ = command.Parameters.AddWithValue("@Fibre", Fibre);
                    _ = command.Parameters.AddWithValue("@Product_Weight", Product_Weight);
                    rows = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return rows > 0;
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            string Id = ingredient.IngredientId;
            int rows = 0;
            using (SQLiteConnection connection = new("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = "DELETE FROM Ingredient WHERE IngredientId = @Id";
                    _ = command.Parameters.AddWithValue("@Id", Id);
                    rows = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return rows > 0;
        }

        public KeyValuePair<int, string>[] GetIngredientNameIdPairs()
        {
            KeyValuePair<int, string>[] pairs = new KeyValuePair<int, string>[0];
            using (SQLiteConnection connection = new("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = "SELECT IngredientId, Name FROM Ingredient";
                    using SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Array.Resize(ref pairs, pairs.Length + 1);
                        pairs[^1] = new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1));
                    }
                }
                connection.Close();
            }
            return pairs;
        }

        public bool TestConnection()
        {
            return File.Exists(AccessString);
        }

        public Ingredient? GetFirstIngredient(string name, string location = "All")
        {
            Ingredient ingredient = new("", "", "", 0, 0, 0, 0, 0, 0, 0);
            using (SQLiteConnection connection = new("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new(connection))
                {
                    command.CommandText = "SELECT * FROM Ingredient WHERE Name = @Id";
                    _ = command.Parameters.AddWithValue("@Id", name);
                    using SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ingredient = new Ingredient(
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
                        );
                    }
                }
                connection.Close();
            }
            return ingredient;
        }

        public List<Ingredient>? GetAllIngredients(string[] ingredients, string location = "All")
        {
            List<Ingredient> ings = new List<Ingredient>();
            foreach (string name in ingredients)
            {
                ings.Add(GetFirstIngredient(name));
            }

            return ings;
        }
        #endregion
    }
}