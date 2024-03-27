namespace WinFormsInfoApp
{
    partial class IngredientSelector
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
            IngredientsBox = new ListBox();
            ingText = new TextBox();
            addIngBtn = new Button();
            removeListBtn = new Button();
            unsaveList = new Button();
            saveList = new Button();
            SuspendLayout();
            // 
            // IngredientsBox
            // 
            IngredientsBox.Font = new Font("Verdana", 12F);
            IngredientsBox.FormattingEnabled = true;
            IngredientsBox.ItemHeight = 18;
            IngredientsBox.Location = new Point(12, 12);
            IngredientsBox.Name = "IngredientsBox";
            IngredientsBox.Size = new Size(383, 310);
            IngredientsBox.TabIndex = 0;
            // 
            // ingText
            // 
            ingText.Font = new Font("Verdana", 12F);
            ingText.Location = new Point(12, 342);
            ingText.Name = "ingText";
            ingText.Size = new Size(239, 27);
            ingText.TabIndex = 1;
            // 
            // addIngBtn
            // 
            addIngBtn.Font = new Font("Verdana", 12F);
            addIngBtn.Location = new Point(257, 342);
            addIngBtn.Name = "addIngBtn";
            addIngBtn.Size = new Size(138, 27);
            addIngBtn.TabIndex = 2;
            addIngBtn.Text = "Add to list";
            addIngBtn.UseVisualStyleBackColor = true;
            addIngBtn.Click += addIngBtn_Click;
            // 
            // removeListBtn
            // 
            removeListBtn.Font = new Font("Verdana", 9F);
            removeListBtn.Location = new Point(12, 396);
            removeListBtn.Name = "removeListBtn";
            removeListBtn.Size = new Size(133, 27);
            removeListBtn.TabIndex = 3;
            removeListBtn.Text = "Remove from list";
            removeListBtn.UseVisualStyleBackColor = true;
            removeListBtn.Click += removeListBtn_Click;
            // 
            // unsaveList
            // 
            unsaveList.Font = new Font("Verdana", 12F);
            unsaveList.Location = new Point(290, 396);
            unsaveList.Name = "unsaveList";
            unsaveList.Size = new Size(105, 27);
            unsaveList.TabIndex = 4;
            unsaveList.Text = "Don't save";
            unsaveList.UseVisualStyleBackColor = true;
            unsaveList.Click += unsaveList_Click;
            // 
            // saveList
            // 
            saveList.Font = new Font("Verdana", 12F);
            saveList.Location = new Point(179, 396);
            saveList.Name = "saveList";
            saveList.Size = new Size(105, 27);
            saveList.TabIndex = 5;
            saveList.Text = "Save";
            saveList.UseVisualStyleBackColor = true;
            saveList.Click += saveList_Click;
            // 
            // IngredientSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 450);
            Controls.Add(saveList);
            Controls.Add(unsaveList);
            Controls.Add(removeListBtn);
            Controls.Add(addIngBtn);
            Controls.Add(ingText);
            Controls.Add(IngredientsBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IngredientSelector";
            Text = "IngredientSelector";
            Load += IngredientSelector_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox IngredientsBox;
        private TextBox ingText;
        private Button addIngBtn;
        private Button removeListBtn;
        private Button unsaveList;
        private Button saveList;
    }
}