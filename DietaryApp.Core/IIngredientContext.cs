using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public interface IIngredientContext
    {
        public string AccessString { get; }
        public Ingredient?[] GetIngredientsByName(string name, string location = "All");
        public List<Ingredient?[]> GetIngredientList(string[] ingredients, string location = "All");
        public bool TestConnection();
        public ConnectionType connectionType { get; }

        public enum ConnectionType
        {
            Local,
            Remote
        }
    }
}
