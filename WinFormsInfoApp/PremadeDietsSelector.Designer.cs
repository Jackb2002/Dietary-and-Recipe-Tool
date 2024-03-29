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
            saveBtn = new Button();
            exitBtn = new Button();
            dietInfo = new Label();
            dietList = new ListBox();
            label1 = new Label();
            dietTitle = new Label();
            priorityLabel = new Label();
            recipeBox.SuspendLayout();
            SuspendLayout();
            // 
            // recipeBox
            // 
            recipeBox.Controls.Add(priorityLabel);
            recipeBox.Controls.Add(saveBtn);
            recipeBox.Controls.Add(exitBtn);
            recipeBox.Controls.Add(dietInfo);
            recipeBox.Controls.Add(dietList);
            recipeBox.Controls.Add(label1);
            recipeBox.Controls.Add(dietTitle);
            recipeBox.Font = new Font("Verdana", 12F);
            recipeBox.Location = new Point(12, 14);
            recipeBox.Name = "recipeBox";
            recipeBox.Size = new Size(568, 272);
            recipeBox.TabIndex = 7;
            recipeBox.TabStop = false;
            recipeBox.Text = "Diets";
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(221, 237);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(92, 23);
            saveBtn.TabIndex = 9;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // exitBtn
            // 
            exitBtn.Location = new Point(470, 237);
            exitBtn.Name = "exitBtn";
            exitBtn.Size = new Size(92, 23);
            exitBtn.TabIndex = 8;
            exitBtn.Text = "Exit";
            exitBtn.UseVisualStyleBackColor = true;
            exitBtn.Click += exitBtn_Click;
            // 
            // dietInfo
            // 
            dietInfo.AutoSize = true;
            dietInfo.Font = new Font("Verdana", 12F);
            dietInfo.Location = new Point(221, 95);
            dietInfo.Name = "dietInfo";
            dietInfo.Size = new Size(79, 18);
            dietInfo.TabIndex = 7;
            dietInfo.Text = "Diet info";
            // 
            // dietList
            // 
            dietList.Font = new Font("Verdana", 12F);
            dietList.FormattingEnabled = true;
            dietList.HorizontalScrollbar = true;
            dietList.ItemHeight = 18;
            dietList.Location = new Point(6, 22);
            dietList.Name = "dietList";
            dietList.Size = new Size(209, 238);
            dietList.TabIndex = 1;
            dietList.SelectedIndexChanged += dietList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 15F);
            label1.Location = new Point(300, 15);
            label1.Name = "label1";
            label1.Size = new Size(179, 25);
            label1.TabIndex = 2;
            label1.Text = "Diet Information";
            // 
            // dietTitle
            // 
            dietTitle.AutoSize = true;
            dietTitle.Font = new Font("Verdana", 12F);
            dietTitle.Location = new Point(221, 47);
            dietTitle.Name = "dietTitle";
            dietTitle.Size = new Size(93, 18);
            dietTitle.TabIndex = 3;
            dietTitle.Text = "Diet name";
            // 
            // priorityLabel
            // 
            priorityLabel.AutoSize = true;
            priorityLabel.Font = new Font("Verdana", 12F);
            priorityLabel.Location = new Point(221, 165);
            priorityLabel.Name = "priorityLabel";
            priorityLabel.Size = new Size(148, 18);
            priorityLabel.TabIndex = 10;
            priorityLabel.Text = "Tries to prioritise";
            // 
            // PremadeDietsSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 298);
            Controls.Add(recipeBox);
            Name = "PremadeDietsSelector";
            Text = "Premade Diets";
            recipeBox.ResumeLayout(false);
            recipeBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox recipeBox;
        private Label dietInfo;
        private ListBox dietList;
        private Label label1;
        private Label dietTitle;
        private Button saveBtn;
        private Button exitBtn;
        private Label priorityLabel;
    }
}