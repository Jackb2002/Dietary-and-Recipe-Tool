namespace WinFormsInfoApp
{
    partial class PremadeDietsSelector
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
            recipeBox = new GroupBox();
            servingsLabel = new Label();
            recipeList = new ListBox();
            recipeIng = new Label();
            label1 = new Label();
            recipeTitle = new Label();
            recipeBox.SuspendLayout();
            SuspendLayout();
            // 
            // recipeBox
            // 
            recipeBox.Controls.Add(servingsLabel);
            recipeBox.Controls.Add(recipeList);
            recipeBox.Controls.Add(recipeIng);
            recipeBox.Controls.Add(label1);
            recipeBox.Controls.Add(recipeTitle);
            recipeBox.Font = new Font("Verdana", 12F);
            recipeBox.Location = new Point(12, 14);
            recipeBox.Name = "recipeBox";
            recipeBox.Size = new Size(568, 272);
            recipeBox.TabIndex = 7;
            recipeBox.TabStop = false;
            recipeBox.Text = "Recipes";
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
            // recipeList
            // 
            recipeList.Font = new Font("Verdana", 12F);
            recipeList.FormattingEnabled = true;
            recipeList.HorizontalScrollbar = true;
            recipeList.ItemHeight = 18;
            recipeList.Location = new Point(6, 22);
            recipeList.Name = "recipeList";
            recipeList.Size = new Size(209, 238);
            recipeList.TabIndex = 1;
            // 
            // recipeIng
            // 
            recipeIng.Font = new Font("Verdana", 12F);
            recipeIng.Location = new Point(221, 128);
            recipeIng.Name = "recipeIng";
            recipeIng.Size = new Size(343, 143);
            recipeIng.TabIndex = 5;
            recipeIng.Text = "Recipe Ingredients:  ";
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
            // PremadeDietsSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 298);
            Controls.Add(recipeBox);
            Name = "PremadeDietsSelector";
            Text = "PremadeDietsSelector";
            recipeBox.ResumeLayout(false);
            recipeBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox recipeBox;
        private Label servingsLabel;
        private ListBox recipeList;
        private Label recipeIng;
        private Label label1;
        private Label recipeTitle;
    }
}