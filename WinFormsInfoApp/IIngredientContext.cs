using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public interface IIngredientContext
    {
        public string AccessString { get; }
        public Ingredient? GetFirstIngredient(string name);
        public List<Ingredient>? GetAllIngredients(params string[] ingredients);
        public bool TestConnection();
        public ConnectionType connectionType { get; }

        public enum ConnectionType
        {
            Local,
            Remote
        }
    }
}
