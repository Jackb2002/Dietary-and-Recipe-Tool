using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public interface IIngredientContext
    {
        public string AccessString { get; }
        public Ingredient? GetFirstIngredient(string name, string location = "All");
        public List<Ingredient>? GetAllIngredients(string[] ingredients, string location = "All");
        public bool TestConnection();
        public ConnectionType connectionType { get; }

        public enum ConnectionType
        {
            Local,
            Remote
        }
    }
}
