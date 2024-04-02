using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public partial class CustomDietsCreator : Form
    {
        private MainWindow mainWindow;
        public CustomDietsCreator(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void calPos_CheckedChanged(object sender, EventArgs e)
        {
            if (calNeg.Checked)
            {
                calNeg.Checked = false;
            }
        }

        private void fatPos_CheckedChanged(object sender, EventArgs e)
        {
            if (fatNeg.Checked)
            {
                fatNeg.Checked = false;
            }
        }

        private void fatNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (fatPos.Checked)
            {
                fatPos.Checked = false;
            }
        }

        private void calNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (calPos.Checked)
            {
                calPos.Checked = false;
            }
        }

        private void satNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (satPos.Checked)
            {
                satPos.Checked = false;
            }
        }

        private void fibNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (fibPos.Checked)
            {
                fibPos.Checked = false;
            }
        }

        private void carbNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (carbPos.Checked)
            {
                carbPos.Checked = false;
            }
        }

        private void salNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (salPos.Checked)
            {
                salPos.Checked = false;
            }
        }

        private void sugNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sugPos.Checked)
            {
                sugPos.Checked = false;
            }
        }

        private void proNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (proPos.Checked)
            {
                proPos.Checked = false;
            }
        }

        private void sugPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sugNeg.Checked)
            {
                sugNeg.Checked = false;
            }
        }

        private void proPos_CheckedChanged(object sender, EventArgs e)
        {
            if (proNeg.Checked)
            {
                proNeg.Checked = false;
            }
        }

        private void carbPos_CheckedChanged(object sender, EventArgs e)
        {
            if (carbNeg.Checked)
            {
                carbNeg.Checked = false;
            }
        }

        private void salPos_CheckedChanged(object sender, EventArgs e)
        {
            if (salNeg.Checked)
            {
                salNeg.Checked = false;
            }
        }

        private void satPos_CheckedChanged(object sender, EventArgs e)
        {
            if (satNeg.Checked)
            {
                satNeg.Checked = false;
            }
        }

        private void fibPos_CheckedChanged(object sender, EventArgs e)
        {
            if (fibNeg.Checked)
            {
                fibNeg.Checked = false;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dietName.Text))
            {
                MessageBox.Show("Diet Name was empty!");
                return;
            }
            if (string.IsNullOrWhiteSpace(dietDesc.Text))
            {
                MessageBox.Show("Diet Description was empty!");
                return;
            }

            List<string> positives = new List<string>();
            List<string> negatives = new List<string>();
            AddToLists(positives, negatives);

            if(positives.Count == 0 && negatives.Count == 0)
            {
                MessageBox.Show("You must select at least one positive or negative attribute!");
                return;
            }

            Diet customDiet = new Diet(dietName.Text, dietDesc.Text, positives.ToArray(), negatives.ToArray());
        }

        private void AddToLists(List<string> positives, List<string> negatives)
        {
            if (calPos.Checked)
            {
                positives.Add("Kcal");
            }
            if (calNeg.Checked)
            {
                negatives.Add("Kcal");
            }
            if (fatPos.Checked)
            {
                positives.Add("Fat");
            }
            if (fatNeg.Checked)
            {
                negatives.Add("Fat");
            }
            if (satPos.Checked)
            {
                positives.Add("Saturates");
            }
            if (satNeg.Checked)
            {
                negatives.Add("Saturates");
            }
            if (fibPos.Checked)
            {
                positives.Add("Fibre");
            }
            if (fibNeg.Checked)
            {
                negatives.Add("Fibre");
            }
            if (carbPos.Checked)
            {
                positives.Add("Carbs");
            }
            if (carbNeg.Checked)
            {
                negatives.Add("Carbs");
            }
            if (salPos.Checked)
            {
                positives.Add("Salt");
            }
            if (salNeg.Checked)
            {
                negatives.Add("Salt");
            }
            if (sugPos.Checked)
            {
                positives.Add("Sugars");
            }
            if (sugNeg.Checked)
            {
                negatives.Add("Sugars");
            }
            if (proPos.Checked)
            {
                positives.Add("Protein");
            }
            if (proNeg.Checked)
            {
                negatives.Add("Protein");
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
