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
            SuspendLayout();
            // 
            // ConnectionStatus
            // 
            ConnectionStatus.AutoSize = true;
            ConnectionStatus.Font = new Font("Verdana", 15F);
            ConnectionStatus.Location = new Point(14, 555);
            ConnectionStatus.Name = "ConnectionStatus";
            ConnectionStatus.Size = new Size(190, 31);
            ConnectionStatus.TabIndex = 0;
            ConnectionStatus.Text = "Connected to ";
            // 
            // recipeList
            // 
            recipeList.FormattingEnabled = true;
            recipeList.Location = new Point(14, 16);
            recipeList.Margin = new Padding(3, 4, 3, 4);
            recipeList.Name = "recipeList";
            recipeList.Size = new Size(238, 524);
            recipeList.TabIndex = 1;
            recipeList.SelectedIndexChanged += recipeList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 15F);
            label1.Location = new Point(259, 16);
            label1.Name = "label1";
            label1.Size = new Size(95, 31);
            label1.TabIndex = 2;
            label1.Text = "Recipe";
            // 
            // recipeTitle
            // 
            recipeTitle.AutoSize = true;
            recipeTitle.Font = new Font("Verdana", 12F);
            recipeTitle.Location = new Point(259, 83);
            recipeTitle.Name = "recipeTitle";
            recipeTitle.Size = new Size(143, 25);
            recipeTitle.TabIndex = 3;
            recipeTitle.Text = "Recipe Name";
            // 
            // recipeLink
            // 
            recipeLink.AutoSize = true;
            recipeLink.Font = new Font("Verdana", 12F);
            recipeLink.Location = new Point(259, 120);
            recipeLink.Name = "recipeLink";
            recipeLink.Size = new Size(143, 25);
            recipeLink.TabIndex = 4;
            recipeLink.Text = "Recipe Link: ";
            // 
            // recipeIng
            // 
            recipeIng.AutoSize = true;
            recipeIng.Font = new Font("Verdana", 12F);
            recipeIng.Location = new Point(259, 157);
            recipeIng.Name = "recipeIng";
            recipeIng.Size = new Size(223, 25);
            recipeIng.TabIndex = 5;
            recipeIng.Text = "Recipe Ingredients:  ";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(recipeIng);
            Controls.Add(recipeLink);
            Controls.Add(recipeTitle);
            Controls.Add(label1);
            Controls.Add(recipeList);
            Controls.Add(ConnectionStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
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
        private Label recipeLink;
        private Label recipeIng;
    }
}