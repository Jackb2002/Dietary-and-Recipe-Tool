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
            servingsLabel = new Label();
            recipeInfoPanel = new Panel();
            recipeBox.SuspendLayout();
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
            recipeList.FormattingEnabled = true;
            recipeList.ItemHeight = 15;
            recipeList.Location = new Point(6, 22);
            recipeList.Name = "recipeList";
            recipeList.Size = new Size(209, 529);
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
            // 
            // recipeIng
            // 
            recipeIng.Font = new Font("Verdana", 12F);
            recipeIng.Location = new Point(221, 128);
            recipeIng.Name = "recipeIng";
            recipeIng.Size = new Size(375, 296);
            recipeIng.TabIndex = 5;
            recipeIng.Text = "Recipe Ingredients:  ";
            // 
            // recipeBox
            // 
            recipeBox.Controls.Add(servingsLabel);
            recipeBox.Controls.Add(recipeInfoPanel);
            recipeBox.Controls.Add(recipeList);
            recipeBox.Controls.Add(recipeIng);
            recipeBox.Controls.Add(label1);
            recipeBox.Controls.Add(recipeLink);
            recipeBox.Controls.Add(recipeTitle);
            recipeBox.Location = new Point(12, 12);
            recipeBox.Name = "recipeBox";
            recipeBox.Size = new Size(602, 559);
            recipeBox.TabIndex = 6;
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
            // recipeInfoPanel
            // 
            recipeInfoPanel.Location = new Point(221, 427);
            recipeInfoPanel.Name = "recipeInfoPanel";
            recipeInfoPanel.Size = new Size(375, 126);
            recipeInfoPanel.TabIndex = 6;
            recipeInfoPanel.Paint += recipeInfoPanel_Paint;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 608);
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
    }
}