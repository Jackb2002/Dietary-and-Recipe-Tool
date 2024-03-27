namespace WinFormsInfoApp
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ConnectionStatus = new Label();
            recipeList = new ListBox();
            label1 = new Label();
            recipeTitle = new Label();
            recipeLink = new Label();
            recipeIng = new Label();
            recipeBox = new GroupBox();
            label2 = new Label();
            servingsLabel = new Label();
            recipeInfoPanel = new Panel();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            editFamilyToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            IngredientBox = new GroupBox();
            label3 = new Label();
            ingOutputBox = new RichTextBox();
            ingSearch = new Button();
            ingName = new TextBox();
            DietBox = new GroupBox();
            nextMealLabel = new Label();
            currentDietLabel = new Label();
            goodIngredients = new Button();
            badIngredients = new Button();
            customDiet = new Button();
            premadeDiet = new Button();
            recipeBox.SuspendLayout();
            menuStrip1.SuspendLayout();
            IngredientBox.SuspendLayout();
            DietBox.SuspendLayout();
            SuspendLayout();
            // 
            // ConnectionStatus
            // 
            ConnectionStatus.AutoSize = true;
            ConnectionStatus.Font = new Font("Verdana", 15F);
            ConnectionStatus.Location = new Point(12, 574);
            ConnectionStatus.Name = "ConnectionStatus";
            ConnectionStatus.Size = new Size(152, 25);
            ConnectionStatus.TabIndex = 0;
            ConnectionStatus.Text = "Connected to ";
            // 
            // recipeList
            // 
            recipeList.Font = new Font("Verdana", 8F);
            recipeList.FormattingEnabled = true;
            recipeList.HorizontalScrollbar = true;
            recipeList.ItemHeight = 13;
            recipeList.Location = new Point(6, 22);
            recipeList.Name = "recipeList";
            recipeList.Size = new Size(209, 498);
            recipeList.TabIndex = 1;
            recipeList.SelectedIndexChanged += recipeList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 15F);
            label1.Location = new Point(300, 15);
            label1.Name = "label1";
            label1.Size = new Size(204, 25);
            label1.TabIndex = 2;
            label1.Text = "Recipe Information";
            // 
            // recipeTitle
            // 
            recipeTitle.AutoSize = true;
            recipeTitle.Font = new Font("Verdana", 12F);
            recipeTitle.Location = new Point(221, 47);
            recipeTitle.Name = "recipeTitle";
            recipeTitle.Size = new Size(122, 18);
            recipeTitle.TabIndex = 3;
            recipeTitle.Text = "Recipe Name:";
            // 
            // recipeLink
            // 
            recipeLink.AutoSize = true;
            recipeLink.Font = new Font("Verdana", 12F);
            recipeLink.Location = new Point(221, 100);
            recipeLink.Name = "recipeLink";
            recipeLink.Size = new Size(114, 18);
            recipeLink.TabIndex = 4;
            recipeLink.Text = "Recipe Link: ";
            recipeLink.Click += recipeLink_Click;
            // 
            // recipeIng
            // 
            recipeIng.Font = new Font("Verdana", 12F);
            recipeIng.Location = new Point(221, 128);
            recipeIng.Name = "recipeIng";
            recipeIng.Size = new Size(375, 251);
            recipeIng.TabIndex = 5;
            recipeIng.Text = "Recipe Ingredients:  ";
            // 
            // recipeBox
            // 
            recipeBox.Controls.Add(label2);
            recipeBox.Controls.Add(servingsLabel);
            recipeBox.Controls.Add(recipeInfoPanel);
            recipeBox.Controls.Add(recipeList);
            recipeBox.Controls.Add(recipeIng);
            recipeBox.Controls.Add(label1);
            recipeBox.Controls.Add(recipeLink);
            recipeBox.Controls.Add(recipeTitle);
            recipeBox.Font = new Font("Verdana", 12F);
            recipeBox.Location = new Point(12, 28);
            recipeBox.Name = "recipeBox";
            recipeBox.Size = new Size(602, 543);
            recipeBox.TabIndex = 6;
            recipeBox.TabStop = false;
            recipeBox.Text = "Recipes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 10F);
            label2.Location = new Point(238, 407);
            label2.Name = "label2";
            label2.Size = new Size(341, 17);
            label2.TabIndex = 8;
            label2.Text = "Nutritional information if made for whole family ";
            // 
            // servingsLabel
            // 
            servingsLabel.AutoSize = true;
            servingsLabel.Font = new Font("Verdana", 12F);
            servingsLabel.Location = new Point(220, 76);
            servingsLabel.Name = "servingsLabel";
            servingsLabel.Size = new Size(144, 18);
            servingsLabel.TabIndex = 7;
            servingsLabel.Text = "Recipe Servings:";
            // 
            // recipeInfoPanel
            // 
            recipeInfoPanel.Location = new Point(221, 427);
            recipeInfoPanel.Name = "recipeInfoPanel";
            recipeInfoPanel.Size = new Size(375, 103);
            recipeInfoPanel.TabIndex = 6;
            recipeInfoPanel.Paint += recipeInfoPanel_Paint;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1060, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { editFamilyToolStripMenuItem, settingsToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // editFamilyToolStripMenuItem
            // 
            editFamilyToolStripMenuItem.Name = "editFamilyToolStripMenuItem";
            editFamilyToolStripMenuItem.Size = new Size(132, 22);
            editFamilyToolStripMenuItem.Text = "Edit Family";
            editFamilyToolStripMenuItem.Click += editFamilyToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(132, 22);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // IngredientBox
            // 
            IngredientBox.Controls.Add(label3);
            IngredientBox.Controls.Add(ingOutputBox);
            IngredientBox.Controls.Add(ingSearch);
            IngredientBox.Controls.Add(ingName);
            IngredientBox.Font = new Font("Verdana", 12F);
            IngredientBox.Location = new Point(634, 28);
            IngredientBox.Name = "IngredientBox";
            IngredientBox.Size = new Size(414, 263);
            IngredientBox.TabIndex = 8;
            IngredientBox.TabStop = false;
            IngredientBox.Text = "Ingredients";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 12F);
            label3.Location = new Point(31, 47);
            label3.Name = "label3";
            label3.Size = new Size(55, 18);
            label3.TabIndex = 5;
            label3.Text = "Name";
            // 
            // ingOutputBox
            // 
            ingOutputBox.Location = new Point(6, 128);
            ingOutputBox.Name = "ingOutputBox";
            ingOutputBox.ReadOnly = true;
            ingOutputBox.Size = new Size(402, 129);
            ingOutputBox.TabIndex = 2;
            ingOutputBox.TabStop = false;
            ingOutputBox.Text = "";
            // 
            // ingSearch
            // 
            ingSearch.Font = new Font("Verdana", 12F);
            ingSearch.Location = new Point(118, 86);
            ingSearch.Name = "ingSearch";
            ingSearch.Size = new Size(178, 32);
            ingSearch.TabIndex = 1;
            ingSearch.Text = "Find Ingredients";
            ingSearch.UseVisualStyleBackColor = true;
            ingSearch.Click += ingSearch_Click;
            // 
            // ingName
            // 
            ingName.Location = new Point(121, 42);
            ingName.Name = "ingName";
            ingName.Size = new Size(287, 27);
            ingName.TabIndex = 0;
            // 
            // DietBox
            // 
            DietBox.Controls.Add(nextMealLabel);
            DietBox.Controls.Add(currentDietLabel);
            DietBox.Controls.Add(goodIngredients);
            DietBox.Controls.Add(badIngredients);
            DietBox.Controls.Add(customDiet);
            DietBox.Controls.Add(premadeDiet);
            DietBox.Font = new Font("Verdana", 12F);
            DietBox.Location = new Point(634, 297);
            DietBox.Name = "DietBox";
            DietBox.Size = new Size(414, 274);
            DietBox.TabIndex = 9;
            DietBox.TabStop = false;
            DietBox.Text = "Diets";
            // 
            // nextMealLabel
            // 
            nextMealLabel.AutoSize = true;
            nextMealLabel.Font = new Font("Verdana", 10F);
            nextMealLabel.Location = new Point(6, 217);
            nextMealLabel.Name = "nextMealLabel";
            nextMealLabel.Size = new Size(106, 17);
            nextMealLabel.TabIndex = 15;
            nextMealLabel.Text = "Todays Meals:";
            // 
            // currentDietLabel
            // 
            currentDietLabel.AutoSize = true;
            currentDietLabel.Font = new Font("Verdana", 15F);
            currentDietLabel.Location = new Point(6, 172);
            currentDietLabel.Name = "currentDietLabel";
            currentDietLabel.Size = new Size(147, 25);
            currentDietLabel.TabIndex = 14;
            currentDietLabel.Text = "Current Diet:";
            // 
            // goodIngredients
            // 
            goodIngredients.Location = new Point(212, 99);
            goodIngredients.Name = "goodIngredients";
            goodIngredients.Size = new Size(196, 47);
            goodIngredients.TabIndex = 13;
            goodIngredients.Text = "Add ingredients to include";
            goodIngredients.UseVisualStyleBackColor = true;
            goodIngredients.Click += goodIngredients_Click;
            // 
            // badIngredients
            // 
            badIngredients.Location = new Point(6, 99);
            badIngredients.Name = "badIngredients";
            badIngredients.Size = new Size(200, 47);
            badIngredients.TabIndex = 12;
            badIngredients.Text = "Add ingredients to not include";
            badIngredients.UseVisualStyleBackColor = true;
            badIngredients.Click += badIngredients_Click;
            // 
            // customDiet
            // 
            customDiet.Location = new Point(212, 46);
            customDiet.Name = "customDiet";
            customDiet.Size = new Size(196, 47);
            customDiet.TabIndex = 11;
            customDiet.Text = "Generate a diet with customised values";
            customDiet.UseVisualStyleBackColor = true;
            customDiet.Click += customDiet_Click;
            // 
            // premadeDiet
            // 
            premadeDiet.Location = new Point(6, 46);
            premadeDiet.Name = "premadeDiet";
            premadeDiet.Size = new Size(200, 47);
            premadeDiet.TabIndex = 10;
            premadeDiet.Text = "Generate a pre made diet plan";
            premadeDiet.UseVisualStyleBackColor = true;
            premadeDiet.Click += premadeDiet_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 608);
            Controls.Add(DietBox);
            Controls.Add(IngredientBox);
            Controls.Add(recipeBox);
            Controls.Add(ConnectionStatus);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "MainWindow";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            recipeBox.ResumeLayout(false);
            recipeBox.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            IngredientBox.ResumeLayout(false);
            IngredientBox.PerformLayout();
            DietBox.ResumeLayout(false);
            DietBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ConnectionStatus;
        private ListBox recipeList;
        private Label label1;
        private Label recipeTitle;
        private Label recipeLink;
        private Label recipeIng;
        private GroupBox recipeBox;
        private Panel recipeInfoPanel;
        private Label servingsLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem editFamilyToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private GroupBox IngredientBox;
        private GroupBox DietBox;
        private Label label2;
        private Button ingSearch;
        private TextBox ingName;
        private RichTextBox ingOutputBox;
        private Label label3;
        private Button customDiet;
        private Button premadeDiet;
        private Button badIngredients;
        private Button goodIngredients;
        private Label nextMealLabel;
        private Label currentDietLabel;
    }
}