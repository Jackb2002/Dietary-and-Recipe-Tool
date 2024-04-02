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
            ConnectionStatus.Location = new Point(14, 765);
            ConnectionStatus.Name = "ConnectionStatus";
            ConnectionStatus.Size = new Size(190, 31);
            ConnectionStatus.TabIndex = 0;
            ConnectionStatus.Text = "Connected to ";
            // 
            // recipeList
            // 
            recipeList.Font = new Font("Verdana", 8F);
            recipeList.FormattingEnabled = true;
            recipeList.HorizontalScrollbar = true;
            recipeList.ItemHeight = 16;
            recipeList.Location = new Point(7, 29);
            recipeList.Margin = new Padding(3, 4, 3, 4);
            recipeList.Name = "recipeList";
            recipeList.Size = new Size(238, 660);
            recipeList.TabIndex = 1;
            recipeList.SelectedIndexChanged += recipeList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 15F);
            label1.Location = new Point(343, 20);
            label1.Name = "label1";
            label1.Size = new Size(251, 31);
            label1.TabIndex = 2;
            label1.Text = "Recipe Information";
            // 
            // recipeTitle
            // 
            recipeTitle.AutoSize = true;
            recipeTitle.Font = new Font("Verdana", 12F);
            recipeTitle.Location = new Point(253, 63);
            recipeTitle.Name = "recipeTitle";
            recipeTitle.Size = new Size(152, 25);
            recipeTitle.TabIndex = 3;
            recipeTitle.Text = "Recipe Name:";
            // 
            // recipeLink
            // 
            recipeLink.AutoSize = true;
            recipeLink.Font = new Font("Verdana", 12F);
            recipeLink.Location = new Point(253, 133);
            recipeLink.Name = "recipeLink";
            recipeLink.Size = new Size(143, 25);
            recipeLink.TabIndex = 4;
            recipeLink.Text = "Recipe Link: ";
            recipeLink.Click += recipeLink_Click;
            // 
            // recipeIng
            // 
            recipeIng.Font = new Font("Verdana", 12F);
            recipeIng.Location = new Point(253, 171);
            recipeIng.Name = "recipeIng";
            recipeIng.Size = new Size(429, 335);
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
            recipeBox.Location = new Point(14, 37);
            recipeBox.Margin = new Padding(3, 4, 3, 4);
            recipeBox.Name = "recipeBox";
            recipeBox.Padding = new Padding(3, 4, 3, 4);
            recipeBox.Size = new Size(688, 724);
            recipeBox.TabIndex = 6;
            recipeBox.TabStop = false;
            recipeBox.Text = "Recipes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 10F);
            label2.Location = new Point(272, 543);
            label2.Name = "label2";
            label2.Size = new Size(426, 20);
            label2.TabIndex = 8;
            label2.Text = "Nutritional information if made for whole family ";
            // 
            // servingsLabel
            // 
            servingsLabel.AutoSize = true;
            servingsLabel.Font = new Font("Verdana", 12F);
            servingsLabel.Location = new Point(251, 101);
            servingsLabel.Name = "servingsLabel";
            servingsLabel.Size = new Size(182, 25);
            servingsLabel.TabIndex = 7;
            servingsLabel.Text = "Recipe Servings:";
            // 
            // recipeInfoPanel
            // 
            recipeInfoPanel.Location = new Point(253, 569);
            recipeInfoPanel.Margin = new Padding(3, 4, 3, 4);
            recipeInfoPanel.Name = "recipeInfoPanel";
            recipeInfoPanel.Size = new Size(429, 137);
            recipeInfoPanel.TabIndex = 6;
            recipeInfoPanel.Paint += recipeInfoPanel_Paint;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(1211, 30);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { editFamilyToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(75, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // editFamilyToolStripMenuItem
            // 
            editFamilyToolStripMenuItem.Name = "editFamilyToolStripMenuItem";
            editFamilyToolStripMenuItem.Size = new Size(224, 26);
            editFamilyToolStripMenuItem.Text = "Edit Family";
            editFamilyToolStripMenuItem.Click += editFamilyToolStripMenuItem_Click;
            // 
            // IngredientBox
            // 
            IngredientBox.Controls.Add(label3);
            IngredientBox.Controls.Add(ingOutputBox);
            IngredientBox.Controls.Add(ingSearch);
            IngredientBox.Controls.Add(ingName);
            IngredientBox.Font = new Font("Verdana", 12F);
            IngredientBox.Location = new Point(725, 37);
            IngredientBox.Margin = new Padding(3, 4, 3, 4);
            IngredientBox.Name = "IngredientBox";
            IngredientBox.Padding = new Padding(3, 4, 3, 4);
            IngredientBox.Size = new Size(473, 351);
            IngredientBox.TabIndex = 8;
            IngredientBox.TabStop = false;
            IngredientBox.Text = "Ingredients";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 12F);
            label3.Location = new Point(35, 63);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 5;
            label3.Text = "Name";
            // 
            // ingOutputBox
            // 
            ingOutputBox.Location = new Point(7, 171);
            ingOutputBox.Margin = new Padding(3, 4, 3, 4);
            ingOutputBox.Name = "ingOutputBox";
            ingOutputBox.ReadOnly = true;
            ingOutputBox.Size = new Size(459, 171);
            ingOutputBox.TabIndex = 2;
            ingOutputBox.TabStop = false;
            ingOutputBox.Text = "";
            // 
            // ingSearch
            // 
            ingSearch.Font = new Font("Verdana", 12F);
            ingSearch.Location = new Point(135, 115);
            ingSearch.Margin = new Padding(3, 4, 3, 4);
            ingSearch.Name = "ingSearch";
            ingSearch.Size = new Size(203, 43);
            ingSearch.TabIndex = 1;
            ingSearch.Text = "Find Ingredients";
            ingSearch.UseVisualStyleBackColor = true;
            ingSearch.Click += ingSearch_Click;
            // 
            // ingName
            // 
            ingName.Location = new Point(138, 56);
            ingName.Margin = new Padding(3, 4, 3, 4);
            ingName.Name = "ingName";
            ingName.Size = new Size(327, 32);
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
            DietBox.Location = new Point(725, 396);
            DietBox.Margin = new Padding(3, 4, 3, 4);
            DietBox.Name = "DietBox";
            DietBox.Padding = new Padding(3, 4, 3, 4);
            DietBox.Size = new Size(473, 365);
            DietBox.TabIndex = 9;
            DietBox.TabStop = false;
            DietBox.Text = "Diets";
            // 
            // nextMealLabel
            // 
            nextMealLabel.AutoSize = true;
            nextMealLabel.Font = new Font("Verdana", 10F);
            nextMealLabel.Location = new Point(7, 289);
            nextMealLabel.Name = "nextMealLabel";
            nextMealLabel.Size = new Size(129, 20);
            nextMealLabel.TabIndex = 15;
            nextMealLabel.Text = "Todays Meals:";
            // 
            // currentDietLabel
            // 
            currentDietLabel.AutoSize = true;
            currentDietLabel.Font = new Font("Verdana", 15F);
            currentDietLabel.Location = new Point(7, 229);
            currentDietLabel.Name = "currentDietLabel";
            currentDietLabel.Size = new Size(180, 31);
            currentDietLabel.TabIndex = 14;
            currentDietLabel.Text = "Current Diet:";
            // 
            // goodIngredients
            // 
            goodIngredients.Location = new Point(242, 132);
            goodIngredients.Margin = new Padding(3, 4, 3, 4);
            goodIngredients.Name = "goodIngredients";
            goodIngredients.Size = new Size(224, 63);
            goodIngredients.TabIndex = 13;
            goodIngredients.Text = "Add ingredients to include";
            goodIngredients.UseVisualStyleBackColor = true;
            goodIngredients.Click += goodIngredients_Click;
            // 
            // badIngredients
            // 
            badIngredients.Location = new Point(7, 132);
            badIngredients.Margin = new Padding(3, 4, 3, 4);
            badIngredients.Name = "badIngredients";
            badIngredients.Size = new Size(229, 63);
            badIngredients.TabIndex = 12;
            badIngredients.Text = "Add ingredients to not include";
            badIngredients.UseVisualStyleBackColor = true;
            badIngredients.Click += badIngredients_Click;
            // 
            // customDiet
            // 
            customDiet.Location = new Point(242, 61);
            customDiet.Margin = new Padding(3, 4, 3, 4);
            customDiet.Name = "customDiet";
            customDiet.Size = new Size(224, 63);
            customDiet.TabIndex = 11;
            customDiet.Text = "Generate a diet with customised values";
            customDiet.UseVisualStyleBackColor = true;
            customDiet.Click += customDiet_Click;
            // 
            // premadeDiet
            // 
            premadeDiet.Location = new Point(7, 61);
            premadeDiet.Margin = new Padding(3, 4, 3, 4);
            premadeDiet.Name = "premadeDiet";
            premadeDiet.Size = new Size(229, 63);
            premadeDiet.TabIndex = 10;
            premadeDiet.Text = "Use an existing diet plan";
            premadeDiet.UseVisualStyleBackColor = true;
            premadeDiet.Click += premadeDiet_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 811);
            Controls.Add(DietBox);
            Controls.Add(IngredientBox);
            Controls.Add(recipeBox);
            Controls.Add(ConnectionStatus);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
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