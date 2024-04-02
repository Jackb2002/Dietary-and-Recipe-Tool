using WinFormsInfoApp.Family;

namespace WinFormsInfoApp
{
    public partial class FamilyEditor : Form
    {
        internal Family.Family family { get; private set; }
        internal FamilyEditor(Family.Family family)
        {
            if (family is default(Family.Family?) or null)
            {
                family = new Family.Family();
            }
            this.family = family;
            InitializeComponent();
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addChildMale_Click(object sender, EventArgs e)
        {
            family.People.Add(new ChildMale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addTeenMale_Click(object sender, EventArgs e)
        {
            family.People.Add(new TeenMale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addAdultMale_Click(object sender, EventArgs e)
        {
            family.People.Add(new AdultMale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addElderMale_Click(object sender, EventArgs e)
        {
            family.People.Add(new ElderlyMale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addChildFemale_Click(object sender, EventArgs e)
        {
            family.People.Add(new ChildFemale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addTeenFemale_Click(object sender, EventArgs e)
        {
            family.People.Add(new TeenFemale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addAdultFemale_Click(object sender, EventArgs e)
        {
            family.People.Add(new AdultFemale());
            familyList.Items.Clear();
            familyList.Items.AddRange(family.People.ToArray());
        }

        private void addElderFemale_Click(object sender, EventArgs e)
        {
            family.People.Add(new ElderlyFemale());
        }

        private void removeLast_Click(object sender, EventArgs e)
        {
            if (familyList.SelectedIndex != -1)
            {
                family.People.RemoveAt(familyList.SelectedIndex);
                familyList.Items.Clear();
                familyList.Items.AddRange(family.People.ToArray());
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            //Return Family to the main form
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
