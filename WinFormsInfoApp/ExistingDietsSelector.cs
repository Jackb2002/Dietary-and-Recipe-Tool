using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public partial class ExistingDietsSelector : Form
    {
        private readonly MainWindow _mainWindow;
        private readonly List<Diet> diets = new List<Diet>();
        private Diet? currentDiet; // The diet that is currently selected
        public ExistingDietsSelector(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            // Load in the premade diets
            mainWindow._dietCache.RemoveAll(d => d.DefaultDiet); // Remove any default diets that are already in the cache
            diets.AddRange(mainWindow._dietCache); // Add the diets that have been created by the user
            diets.AddRange(Diet.ReturnDefaultDiets()); // Add the default diets
            dietList.Items.AddRange(diets.ToArray()); // Add the diets to the list
        }

        private void dietList_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDiet = (Diet)dietList.SelectedItem;
            dietTitle.Text = currentDiet.Name;
            dietInfo.Text = currentDiet.Description;
            priorityLabel.Text = "This diet prioritises:\n" + string.Join("\n", currentDiet.PriorityPositive);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.currentDiet = currentDiet;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
