using System;
using System.Data.SQLite;
using System.Diagnostics;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp.Database
{
    internal class DatabaseManager
    {
        private string filePath;
        

        // Constructor
        public DatabaseManager(string path)
        {
            filePath = path;

            Debug.WriteLineIf(CheckOrCreateIngredientTable(), "Ingredient table created");
        }

        #region Ingredient
        private bool CheckOrCreateIngredientTable()
        {
            int rows = 0;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS Ingredient (IngredientId INTEGER PRIMARY KEY, Name TEXT, Description TEXT, Fat REAL, Carbohydrates REAL, Protein REAL, Calories REAL, Sugar REAL, Fiber REAL, Product_Weight REAL)";
                    rows = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return rows > 0;
        }

        public bool InsertIngredient(int Id, string Name, string Description, double Fat, double Carbohydrates, double Protein, double Calories, double Sugar, double Fiber, double Product_Weight)
        {
            int rows = 0;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Ingredient (IngredientId, Name, Description, Fat, Carbohydrates, Protein, Calories, Sugar, Fiber, Product_Weight) VALUES (@Id, @Name, @Description, @Fat, @Carbohydrates, @Protein, @Calories, @Sugar, @Fiber, @Product_Weight)";
                    command.Parameters.AddWithValue("@Id", Id);
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

        public bool DeleteIngredient(int Id)
        {
            int rows = 0;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath))
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

        public Ingredient[] GetIngredients(string name)
        {
            Ingredient[] ingredients = new Ingredient[0];
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath))
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
            return ingredients;
        }

        public Ingredient GetIngredient(int Id)
        {
            Ingredient ingredient = new Ingredient(0, "", "", 0, 0, 0, 0, 0, 0, 0);
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + filePath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Ingredient WHERE IngredientId = @Id";
                    command.Parameters.AddWithValue("@Id", Id);
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
        #endregion
    }
}