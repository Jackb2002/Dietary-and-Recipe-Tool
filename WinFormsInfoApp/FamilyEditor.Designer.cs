namespace WinFormsInfoApp
{
    partial class FamilyEditor
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
            addTeenMale = new Button();
            addAdultMale = new Button();
            addElderMale = new Button();
            removeLast = new Button();
            addChildMale = new Button();
            saveBtn = new Button();
            addElderFemale = new Button();
            addAdultFemale = new Button();
            addTeenFemale = new Button();
            addChildFemale = new Button();
            familyList = new ListBox();
            SuspendLayout();
            // 
            // addTeenMale
            // 
            addTeenMale.Location = new Point(249, 64);
            addTeenMale.Name = "addTeenMale";
            addTeenMale.Size = new Size(125, 36);
            addTeenMale.TabIndex = 1;
            addTeenMale.Text = "Add Teenager (Male)";
            addTeenMale.UseVisualStyleBackColor = true;
            addTeenMale.Click += addTeenMale_Click;
            // 
            // addAdultMale
            // 
            addAdultMale.Location = new Point(249, 116);
            addAdultMale.Name = "addAdultMale";
            addAdultMale.Size = new Size(125, 36);
            addAdultMale.TabIndex = 2;
            addAdultMale.Text = "Add Adult (Male)";
            addAdultMale.UseVisualStyleBackColor = true;
            addAdultMale.Click += addAdultMale_Click;
            // 
            // addElderMale
            // 
            addElderMale.Location = new Point(249, 168);
            addElderMale.Name = "addElderMale";
            addElderMale.Size = new Size(125, 36);
            addElderMale.TabIndex = 3;
            addElderMale.Text = "Add Elder (Male)";
            addElderMale.UseVisualStyleBackColor = true;
            addElderMale.Click += addElderMale_Click;
            // 
            // removeLast
            // 
            removeLast.Font = new Font("Segoe UI", 8F);
            removeLast.Location = new Point(249, 220);
            removeLast.Name = "removeLast";
            removeLast.Size = new Size(125, 36);
            removeLast.TabIndex = 4;
            removeLast.Text = "Remove Member";
            removeLast.UseVisualStyleBackColor = true;
            removeLast.Click += removeLast_Click;
            // 
            // addChildMale
            // 
            addChildMale.Location = new Point(249, 12);
            addChildMale.Name = "addChildMale";
            addChildMale.Size = new Size(125, 36);
            addChildMale.TabIndex = 0;
            addChildMale.Text = "Add Child (Male)";
            addChildMale.UseVisualStyleBackColor = true;
            addChildMale.Click += addChildMale_Click;
            // 
            // saveBtn
            // 
            saveBtn.Font = new Font("Segoe UI", 8F);
            saveBtn.Location = new Point(405, 220);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(125, 36);
            saveBtn.TabIndex = 9;
            saveBtn.Text = "Save Family";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // addElderFemale
            // 
            addElderFemale.Location = new Point(405, 168);
            addElderFemale.Name = "addElderFemale";
            addElderFemale.Size = new Size(125, 36);
            addElderFemale.TabIndex = 8;
            addElderFemale.Text = "Add Elder (Female)";
            addElderFemale.UseVisualStyleBackColor = true;
            addElderFemale.Click += addElderFemale_Click;
            // 
            // addAdultFemale
            // 
            addAdultFemale.Location = new Point(405, 116);
            addAdultFemale.Name = "addAdultFemale";
            addAdultFemale.Size = new Size(125, 36);
            addAdultFemale.TabIndex = 7;
            addAdultFemale.Text = "Add Adult (Female)";
            addAdultFemale.UseVisualStyleBackColor = true;
            addAdultFemale.Click += addAdultFemale_Click;
            // 
            // addTeenFemale
            // 
            addTeenFemale.Font = new Font("Segoe UI", 8F);
            addTeenFemale.Location = new Point(405, 64);
            addTeenFemale.Name = "addTeenFemale";
            addTeenFemale.Size = new Size(125, 36);
            addTeenFemale.TabIndex = 6;
            addTeenFemale.Text = "Add Teenager (Female)";
            addTeenFemale.UseVisualStyleBackColor = true;
            addTeenFemale.Click += addTeenFemale_Click;
            // 
            // addChildFemale
            // 
            addChildFemale.Location = new Point(405, 12);
            addChildFemale.Name = "addChildFemale";
            addChildFemale.Size = new Size(125, 36);
            addChildFemale.TabIndex = 5;
            addChildFemale.Text = "Add Child (Female)";
            addChildFemale.UseVisualStyleBackColor = true;
            addChildFemale.Click += addChildFemale_Click;
            // 
            // familyList
            // 
            familyList.FormattingEnabled = true;
            familyList.ItemHeight = 15;
            familyList.Location = new Point(12, 12);
            familyList.Name = "familyList";
            familyList.Size = new Size(205, 244);
            familyList.TabIndex = 11;
            // 
            // FamilyEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 269);
            Controls.Add(familyList);
            Controls.Add(saveBtn);
            Controls.Add(addElderFemale);
            Controls.Add(addAdultFemale);
            Controls.Add(addTeenFemale);
            Controls.Add(addChildFemale);
            Controls.Add(removeLast);
            Controls.Add(addElderMale);
            Controls.Add(addAdultMale);
            Controls.Add(addTeenMale);
            Controls.Add(addChildMale);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FamilyEditor";
            Text = "Family Editor";
            ResumeLayout(false);
        }

        #endregion
        private Button addTeenMale;
        private Button addAdultMale;
        private Button addElderMale;
        private Button removeLast;
        private Button addChildMale;
        private Button saveBtn;
        private Button addElderFemale;
        private Button addAdultFemale;
        private Button addTeenFemale;
        private Button addChildFemale;
        private ListBox familyList;
    }
}