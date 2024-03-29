using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public partial class PremadeDietsSelector : Form
    {
        private MainWindow _mainWindow;
        private Diet[] diets;
        private Diet? currentDiet; // The diet that is currently selected
        public PremadeDietsSelector(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            // Load in the premade diets
            diets = Diet.ReturnDefaultDiets();
            dietList.Items.AddRange(diets);
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
