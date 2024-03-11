using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    internal interface IIngredientContext
    {
        public string AccessString { get; }
        public Ingredient GetFirstIngredient(string name);
        public List<Ingredient> GetAllIngredients(string name);
        public bool TestConnection();
        public ConnectionType connectionType { get; }

        public enum ConnectionType
        {
            Local,
            Remote
        }
    }
}
