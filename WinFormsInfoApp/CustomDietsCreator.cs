using WinFormsInfoApp.Models;

namespace WinFormsInfoApp
{
    public partial class CustomDietsCreator : Form
    {
        private readonly MainWindow mainWindow;
        public CustomDietsCreator(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void calPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == calPos && calPos.Checked)
            {
                calNeg.Checked = false;
            }
        }

        private void calNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == calNeg && calNeg.Checked)
            {
                calPos.Checked = false;
            }
        }

        private void fatPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == fatPos && fatPos.Checked)
            {
                fatNeg.Checked = false;
            }
        }

        private void fatNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == fatNeg && fatNeg.Checked)
            {
                fatPos.Checked = false;
            }
        }

        private void satPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == satPos && satPos.Checked)
            {
                satNeg.Checked = false;
            }
        }

        private void satNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == satNeg && satNeg.Checked)
            {
                satPos.Checked = false;
            }
        }

        private void fibPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == fibPos && fibPos.Checked)
            {
                fibNeg.Checked = false;
            }
        }

        private void fibNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == fibNeg && fibNeg.Checked)
            {
                fibPos.Checked = false;
            }
        }

        private void carbPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == carbPos && carbPos.Checked)
            {
                carbNeg.Checked = false;
            }
        }

        private void carbNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == carbNeg && carbNeg.Checked)
            {
                carbPos.Checked = false;
            }
        }

        private void salPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == salPos && salPos.Checked)
            {
                salNeg.Checked = false;
            }
        }

        private void salNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == salNeg && salNeg.Checked)
            {
                salPos.Checked = false;
            }
        }

        private void sugPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == sugPos && sugPos.Checked)
            {
                sugNeg.Checked = false;
            }
        }

        private void sugNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == sugNeg && sugNeg.Checked)
            {
                sugPos.Checked = false;
            }
        }

        private void proPos_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == proPos && proPos.Checked)
            {
                proNeg.Checked = false;
            }
        }

        private void proNeg_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == proNeg && proNeg.Checked)
            {
                proPos.Checked = false;
            }
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dietName.Text))
            {
                _ = MessageBox.Show("Diet Name was empty!");
                return;
            }
            if (string.IsNullOrWhiteSpace(dietDesc.Text))
            {
                _ = MessageBox.Show("Diet Description was empty!");
                return;
            }

            List<string> positives = [];
            List<string> negatives = [];
            AddToLists(positives, negatives);

            if (positives.Count == 0 && negatives.Count == 0)
            {
                _ = MessageBox.Show("You must select at least one positive or negative attribute!");
                return;
            }

            Diet customDiet = new(dietName.Text, dietDesc.Text, positives.ToArray(), negatives.ToArray(), DateTime.Now);
            mainWindow.currentDiet = customDiet;
            DialogResult = DialogResult.OK;
            Close();
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
