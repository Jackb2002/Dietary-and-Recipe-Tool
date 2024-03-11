using static WinFormsInfoApp.IIngredientContext;

namespace WinFormsInfoApp
{
    public partial class MainWindow : Form
    {
        private readonly IIngredientContext _ingredientContext;
        public MainWindow(IIngredientContext ingredientContext)
        {
            _ingredientContext = ingredientContext;
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if(_ingredientContext.connectionType == ConnectionType.Local)
            {
                ConnectionStatus.Text = "Connected to local DB";
                MessageBox.Show("A local connection is being used, some functionality may be limited");
                ConnectionStatus.ForeColor = Color.Orange;
            }
            else
            {
                ConnectionStatus.Text = "Connected to API";
                ConnectionStatus.ForeColor = Color.Green;
            }
        }
    }
}
