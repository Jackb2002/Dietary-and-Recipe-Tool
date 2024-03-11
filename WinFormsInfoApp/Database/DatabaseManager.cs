using System;
using System.Data.SQLite;
using System.Diagnostics;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.Database
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
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS Ingredient (IngredientId INTEGER PRIMARY KEY, Name TEXT, Description TEXT, Fat REAL, Carbohydrates REAL, Protein REAL, Calories REAL, Sugar REAL, Fiber REAL, Product_Weight REAL)";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public bool InsertIngredient(Ingredient ingredient)
        {
            int Id = ingredient.IngredientId;
            string Name = ingredient.Name;
            string Description = ingredient.Description;
            double Fat = ingredient.Fat;
            double Carbohydrates = ingredient.Carbohydrates;
            double Protein = ingredient.Protein;
            double Calories = ingredient.Calories;
            double Sugar = ingredient.Sugar;
            double Fiber = ingredient.Fiber;
            double Product_Weight = ingredient.Product_Weight;
            int rows = 0;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Ingredient (Name, Description, Fat, Carbohydrates, Protein, Calories, Sugar, Fiber, Product_Weight) VALUES (@Name, @Description, @Fat, @Carbohydrates, @Protein, @Calories, @Sugar, @Fiber, @Product_Weight)";
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@Fat", Fat);
                    command.Parameters.AddWithValue("@Carbohydrates", Carbohydrates);
                    command.Parameters.AddWithValue("@Protein", Protein);
                    command.Parameters.AddWithValue("@Calories", Calories);
                    command.Parameters.AddWithValue("@Sugar", Sugar);
                    command.Parameters.AddWithValue("@Fiber", Fiber);
                    command.Parameters.AddWithValue("@Product_Weight", Product_Weight);
                    rows = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return rows > 0;
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            int Id = ingredient.IngredientId;
            int rows = 0;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DELETE FROM Ingredient WHERE IngredientId = @Id";
                    command.Parameters.AddWithValue("@Id", Id);
                    rows = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return rows > 0;
        }

        public List<Ingredient> GetAllIngredients(string name)
        {
            Ingredient[] ingredients = new Ingredient[0];
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Ingredient WHERE Name = @Name";
                    command.Parameters.AddWithValue("@Name", name);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Array.Resize(ref ingredients, ingredients.Length + 1);
                            ingredients[ingredients.Length - 1] = new Ingredient(
                            reader.GetInt32(0),
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
                }
                connection.Close();
            }
            return ingredients.ToList();
        }

        public Ingredient GetFirstIngredient(string name)
        {
            Ingredient ingredient = new Ingredient(0, "", "", 0, 0, 0, 0, 0, 0, 0);
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Ingredient WHERE Name = @Id";
                    command.Parameters.AddWithValue("@Id", name);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ingredient = new Ingredient(
                            reader.GetInt32(0),
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
                }
                connection.Close();
            }
            return ingredient;
        }

        public KeyValuePair<int, string>[] GetIngredientNameIdPairs()
        {
            KeyValuePair<int, string>[] pairs = new KeyValuePair<int, string>[0];
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AccessString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT IngredientId, Name FROM Ingredient";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Array.Resize(ref pairs, pairs.Length + 1);
                            pairs[pairs.Length - 1] = new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1));
                        }
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
        #endregion
    }
}