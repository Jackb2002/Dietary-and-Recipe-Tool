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
            RecipeCookTime = new Label();
            SuspendLayout();
            // 
            // ConnectionStatus
            // 
            ConnectionStatus.AutoSize = true;
            ConnectionStatus.Font = new Font("Verdana", 15F);
            ConnectionStatus.Location = new Point(12, 416);
            ConnectionStatus.Name = "ConnectionStatus";
            ConnectionStatus.Size = new Size(152, 25);
            ConnectionStatus.TabIndex = 0;
            ConnectionStatus.Text = "Connected to ";
            // 
            // recipeList
            // 
            recipeList.FormattingEnabled = true;
            recipeList.ItemHeight = 15;
            recipeList.Location = new Point(12, 12);
            recipeList.Name = "recipeList";
            recipeList.Size = new Size(209, 394);
            recipeList.TabIndex = 1;
            recipeList.SelectedIndexChanged += recipeList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 15F);
            label1.Location = new Point(227, 12);
            label1.Name = "label1";
            label1.Size = new Size(78, 25);
            label1.TabIndex = 2;
            label1.Text = "Recipe";
            // 
            // recipeTitle
            // 
            recipeTitle.AutoSize = true;
            recipeTitle.Font = new Font("Verdana", 12F);
            recipeTitle.Location = new Point(227, 62);
            recipeTitle.Name = "recipeTitle";
            recipeTitle.Size = new Size(0, 18);
            recipeTitle.TabIndex = 3;
            // 
            // RecipeCookTime
            // 
            RecipeCookTime.AutoSize = true;
            RecipeCookTime.Font = new Font("Verdana", 12F);
            RecipeCookTime.Location = new Point(227, 80);
            RecipeCookTime.Name = "RecipeCookTime";
            RecipeCookTime.Size = new Size(0, 18);
            RecipeCookTime.TabIndex = 4;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RecipeCookTime);
            Controls.Add(recipeTitle);
            Controls.Add(label1);
            Controls.Add(recipeList);
            Controls.Add(ConnectionStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "MainWindow";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ConnectionStatus;
        private ListBox recipeList;
        private Label label1;
        private Label recipeTitle;
        private Label RecipeCookTime;
    }
}