namespace WinFormsInfoApp
{
    public partial class IngredientSelector : Form
    {
        private readonly List<string> ingredientList = [];
        private readonly bool forPositive;
        private readonly MainWindow mainWindow;
        public IngredientSelector(bool forPositive, MainWindow mainWindow)
        {
            InitializeComponent();
            if (forPositive)
            {
                Text = "Select ingredients to chose";
                ingredientList = mainWindow.good_ings;
                UpdateList();

            }
            else
            {
                Text = "Select ingredients to avoid";
                ingredientList = mainWindow.avoid_ings;
                UpdateList();
            }
            this.forPositive = forPositive;
            this.mainWindow = mainWindow;
        }

        private void addIngBtn_Click(object sender, EventArgs e)
        {
            string ing = ingText.Text.Trim().ToLower();
            ingredientList.Add(ing);
            UpdateList();
        }

        private void UpdateList()
        {
            IngredientsBox.Items.Clear();
            IngredientsBox.Items.AddRange(ingredientList.ToArray());
        }

        private void removeListBtn_Click(object sender, EventArgs e)
        {
            _ = ingredientList.Remove(IngredientsBox.SelectedItem.ToString());
            UpdateList();
        }

        private void saveList_Click(object sender, EventArgs e)
        {
            if (forPositive)
            {
                mainWindow.good_ings = ingredientList;
            }
            else
            {
                mainWindow.avoid_ings = ingredientList;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void unsaveList_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void IngredientSelector_Load(object sender, EventArgs e)
        {
            string msg = forPositive ? "try and include" : "try and avoid";
            _ = MessageBox.Show($"Add any text you want to {msg}. When pressing save a diet plan will look for this text when creating your diet plan");
        }
    }
}
