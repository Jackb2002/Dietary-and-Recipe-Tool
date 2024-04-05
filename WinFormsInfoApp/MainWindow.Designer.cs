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
            IngredientBox = new GroupBox();
            ingComboBox = new ComboBox();
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
            groupBox1 = new GroupBox();
            flOzTxtLabel = new Label();
            flOzTxt = new TextBox();
            cupTxtLabel = new Label();
            cupTxt = new TextBox();
            mlTxtLabel = new Label();
            mlTxt = new TextBox();
            lTxtLabel = new Label();
            lTxt = new TextBox();
            label8 = new Label();
            lbsTxt = new TextBox();
            label7 = new Label();
            tbspTxt = new TextBox();
            label6 = new Label();
            tspTxt = new TextBox();
            label5 = new Label();
            ozTxt = new TextBox();
            label4 = new Label();
            gTxt = new TextBox();
            groupBox2 = new GroupBox();
            recipeBox.SuspendLayout();
            IngredientBox.SuspendLayout();
            DietBox.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // ConnectionStatus
            // 
            ConnectionStatus.AutoSize = true;
            ConnectionStatus.Font = new Font("Verdana", 8F);
            ConnectionStatus.Location = new Point(12, 574);
            ConnectionStatus.Name = "ConnectionStatus";
            ConnectionStatus.Size = new Size(87, 13);
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
            recipeList.Size = new Size(209, 485);
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
            recipeBox.Location = new Point(12, 12);
            recipeBox.Name = "recipeBox";
            recipeBox.Size = new Size(602, 559);
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
            // IngredientBox
            // 
            IngredientBox.Controls.Add(ingComboBox);
            IngredientBox.Controls.Add(label3);
            IngredientBox.Controls.Add(ingOutputBox);
            IngredientBox.Controls.Add(ingSearch);
            IngredientBox.Controls.Add(ingName);
            IngredientBox.Font = new Font("Verdana", 12F);
            IngredientBox.Location = new Point(634, 12);
            IngredientBox.Name = "IngredientBox";
            IngredientBox.Size = new Size(414, 279);
            IngredientBox.TabIndex = 8;
            IngredientBox.TabStop = false;
            IngredientBox.Text = "Ingredients";
            // 
            // ingComboBox
            // 
            ingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ingComboBox.FormattingEnabled = true;
            ingComboBox.Location = new Point(190, 86);
            ingComboBox.Name = "ingComboBox";
            ingComboBox.Size = new Size(218, 26);
            ingComboBox.TabIndex = 9;
            ingComboBox.SelectedIndexChanged += ingComboBox_SelectedIndexChanged;
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
            ingOutputBox.Size = new Size(402, 145);
            ingOutputBox.TabIndex = 2;
            ingOutputBox.TabStop = false;
            ingOutputBox.Text = "";
            // 
            // ingSearch
            // 
            ingSearch.Font = new Font("Verdana", 12F);
            ingSearch.Location = new Point(6, 86);
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
            premadeDiet.Text = "Use an existing diet plan";
            premadeDiet.UseVisualStyleBackColor = true;
            premadeDiet.Click += premadeDiet_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(flOzTxtLabel);
            groupBox1.Controls.Add(flOzTxt);
            groupBox1.Controls.Add(cupTxtLabel);
            groupBox1.Controls.Add(cupTxt);
            groupBox1.Controls.Add(mlTxtLabel);
            groupBox1.Controls.Add(mlTxt);
            groupBox1.Controls.Add(lTxtLabel);
            groupBox1.Controls.Add(lTxt);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(lbsTxt);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(tbspTxt);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(tspTxt);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(ozTxt);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(gTxt);
            groupBox1.Font = new Font("Verdana", 12F);
            groupBox1.Location = new Point(1054, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(380, 279);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Measurement Converter";
            // 
            // flOzTxtLabel
            // 
            flOzTxtLabel.AutoSize = true;
            flOzTxtLabel.Location = new Point(191, 132);
            flOzTxtLabel.Name = "flOzTxtLabel";
            flOzTxtLabel.Size = new Size(50, 18);
            flOzTxtLabel.TabIndex = 19;
            flOzTxtLabel.Text = "Fl Oz";
            // 
            // flOzTxt
            // 
            flOzTxt.Location = new Point(265, 128);
            flOzTxt.Name = "flOzTxt";
            flOzTxt.Size = new Size(100, 27);
            flOzTxt.TabIndex = 18;
            flOzTxt.TextChanged += measurementLiquidChanged;
            // 
            // cupTxtLabel
            // 
            cupTxtLabel.AutoSize = true;
            cupTxtLabel.Location = new Point(193, 183);
            cupTxtLabel.Name = "cupTxtLabel";
            cupTxtLabel.Size = new Size(48, 18);
            cupTxtLabel.TabIndex = 15;
            cupTxtLabel.Text = "Cups";
            // 
            // cupTxt
            // 
            cupTxt.Location = new Point(265, 179);
            cupTxt.Name = "cupTxt";
            cupTxt.Size = new Size(100, 27);
            cupTxt.TabIndex = 14;
            cupTxt.TextChanged += measurementLiquidChanged;
            // 
            // mlTxtLabel
            // 
            mlTxtLabel.AutoSize = true;
            mlTxtLabel.Location = new Point(191, 81);
            mlTxtLabel.Name = "mlTxtLabel";
            mlTxtLabel.Size = new Size(26, 18);
            mlTxtLabel.TabIndex = 13;
            mlTxtLabel.Text = "Ml";
            // 
            // mlTxt
            // 
            mlTxt.Location = new Point(265, 78);
            mlTxt.Name = "mlTxt";
            mlTxt.Size = new Size(100, 27);
            mlTxt.TabIndex = 12;
            mlTxt.TextChanged += measurementLiquidChanged;
            // 
            // lTxtLabel
            // 
            lTxtLabel.AutoSize = true;
            lTxtLabel.Location = new Point(193, 30);
            lTxtLabel.Name = "lTxtLabel";
            lTxtLabel.Size = new Size(54, 18);
            lTxtLabel.TabIndex = 11;
            lTxtLabel.Text = "Litres";
            // 
            // lTxt
            // 
            lTxt.Location = new Point(265, 26);
            lTxt.Name = "lTxt";
            lTxt.Size = new Size(100, 27);
            lTxt.TabIndex = 10;
            lTxt.TextChanged += measurementLiquidChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(4, 132);
            label8.Name = "label8";
            label8.Size = new Size(36, 18);
            label8.TabIndex = 9;
            label8.Text = "Lbs";
            // 
            // lbsTxt
            // 
            lbsTxt.Location = new Point(78, 128);
            lbsTxt.Name = "lbsTxt";
            lbsTxt.Size = new Size(100, 27);
            lbsTxt.TabIndex = 8;
            lbsTxt.TextChanged += measurementWeightChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 234);
            label7.Name = "label7";
            label7.Size = new Size(47, 18);
            label7.TabIndex = 7;
            label7.Text = "Tbsp";
            // 
            // tbspTxt
            // 
            tbspTxt.Location = new Point(78, 230);
            tbspTxt.Name = "tbspTxt";
            tbspTxt.Size = new Size(100, 27);
            tbspTxt.TabIndex = 6;
            tbspTxt.TextChanged += measurementWeightChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 183);
            label6.Name = "label6";
            label6.Size = new Size(36, 18);
            label6.TabIndex = 5;
            label6.Text = "Tsp";
            // 
            // tspTxt
            // 
            tspTxt.Location = new Point(78, 179);
            tspTxt.Name = "tspTxt";
            tspTxt.Size = new Size(100, 27);
            tspTxt.TabIndex = 4;
            tspTxt.TextChanged += measurementWeightChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(4, 81);
            label5.Name = "label5";
            label5.Size = new Size(30, 18);
            label5.TabIndex = 3;
            label5.Text = "Oz";
            // 
            // ozTxt
            // 
            ozTxt.Location = new Point(78, 78);
            ozTxt.Name = "ozTxt";
            ozTxt.Size = new Size(100, 27);
            ozTxt.TabIndex = 2;
            ozTxt.TextChanged += measurementWeightChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 30);
            label4.Name = "label4";
            label4.Size = new Size(60, 18);
            label4.TabIndex = 1;
            label4.Text = "Grams";
            // 
            // gTxt
            // 
            gTxt.Location = new Point(78, 26);
            gTxt.Name = "gTxt";
            gTxt.Size = new Size(100, 27);
            gTxt.TabIndex = 0;
            gTxt.TextChanged += measurementWeightChanged;
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Verdana", 12F);
            groupBox2.Location = new Point(1054, 297);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(380, 274);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Settings";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1446, 608);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(DietBox);
            Controls.Add(IngredientBox);
            Controls.Add(recipeBox);
            Controls.Add(ConnectionStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "MainWindow";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            recipeBox.ResumeLayout(false);
            recipeBox.PerformLayout();
            IngredientBox.ResumeLayout(false);
            IngredientBox.PerformLayout();
            DietBox.ResumeLayout(false);
            DietBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label4;
        private TextBox gTxt;
        private Label label7;
        private TextBox tbspTxt;
        private Label label6;
        private TextBox tspTxt;
        private Label label5;
        private TextBox ozTxt;
        private Label label8;
        private TextBox lbsTxt;
        private Label flOzTxtLabel;
        private TextBox flOzTxt;
        private Label cupTxtLabel;
        private TextBox cupTxt;
        private Label mlTxtLabel;
        private TextBox mlTxt;
        private Label lTxtLabel;
        private TextBox lTxt;
        private ComboBox ingComboBox;
    }
}