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
            Diet[] items = Diet.ReturnDefaultDiets();
            dietList.Items.AddRange(items);
            diets = items;
        }

        private void dietList_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDiet = (Diet)dietList.SelectedItem;
            dietTitle.Text = currentDiet.Name;
            dietInfo.Text = currentDiet.Description;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.currentDiet = currentDiet;
            Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
