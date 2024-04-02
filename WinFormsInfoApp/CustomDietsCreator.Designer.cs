
namespace WinFormsInfoApp
{
    partial class CustomDietsCreator
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
            groupBox1 = new GroupBox();
            proPos = new CheckBox();
            sugPos = new CheckBox();
            salPos = new CheckBox();
            carbPos = new CheckBox();
            fibPos = new CheckBox();
            satPos = new CheckBox();
            fatPos = new CheckBox();
            calPos = new CheckBox();
            groupBox2 = new GroupBox();
            proNeg = new CheckBox();
            calNeg = new CheckBox();
            sugNeg = new CheckBox();
            fatNeg = new CheckBox();
            salNeg = new CheckBox();
            satNeg = new CheckBox();
            carbNeg = new CheckBox();
            fibNeg = new CheckBox();
            saveBtn = new Button();
            closeBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            dietName = new TextBox();
            dietDesc = new RichTextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(proPos);
            groupBox1.Controls.Add(sugPos);
            groupBox1.Controls.Add(salPos);
            groupBox1.Controls.Add(carbPos);
            groupBox1.Controls.Add(fibPos);
            groupBox1.Controls.Add(satPos);
            groupBox1.Controls.Add(fatPos);
            groupBox1.Controls.Add(calPos);
            groupBox1.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(190, 251);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Include";
            // 
            // proPos
            // 
            proPos.AutoSize = true;
            proPos.Location = new Point(6, 222);
            proPos.Name = "proPos";
            proPos.Size = new Size(85, 22);
            proPos.TabIndex = 7;
            proPos.Text = "Protein";
            proPos.UseVisualStyleBackColor = true;
            proPos.CheckedChanged += proPos_CheckedChanged;
            // 
            // sugPos
            // 
            sugPos.AutoSize = true;
            sugPos.Location = new Point(6, 194);
            sugPos.Name = "sugPos";
            sugPos.Size = new Size(73, 22);
            sugPos.TabIndex = 6;
            sugPos.Text = "Sugar";
            sugPos.UseVisualStyleBackColor = true;
            sugPos.CheckedChanged += sugPos_CheckedChanged;
            // 
            // salPos
            // 
            salPos.AutoSize = true;
            salPos.Location = new Point(6, 166);
            salPos.Name = "salPos";
            salPos.Size = new Size(59, 22);
            salPos.TabIndex = 5;
            salPos.Text = "Salt";
            salPos.UseVisualStyleBackColor = true;
            salPos.CheckedChanged += salPos_CheckedChanged;
            // 
            // carbPos
            // 
            carbPos.AutoSize = true;
            carbPos.Location = new Point(6, 138);
            carbPos.Name = "carbPos";
            carbPos.Size = new Size(145, 22);
            carbPos.TabIndex = 4;
            carbPos.Text = "Carbohydrates";
            carbPos.UseVisualStyleBackColor = true;
            carbPos.CheckedChanged += carbPos_CheckedChanged;
            // 
            // fibPos
            // 
            fibPos.AutoSize = true;
            fibPos.Location = new Point(6, 110);
            fibPos.Name = "fibPos";
            fibPos.Size = new Size(67, 22);
            fibPos.TabIndex = 3;
            fibPos.Text = "Fibre";
            fibPos.UseVisualStyleBackColor = true;
            fibPos.CheckedChanged += fibPos_CheckedChanged;
            // 
            // satPos
            // 
            satPos.AutoSize = true;
            satPos.Location = new Point(6, 82);
            satPos.Name = "satPos";
            satPos.Size = new Size(145, 22);
            satPos.TabIndex = 2;
            satPos.Text = "Saturated fats";
            satPos.UseVisualStyleBackColor = true;
            satPos.CheckedChanged += satPos_CheckedChanged;
            // 
            // fatPos
            // 
            fatPos.AutoSize = true;
            fatPos.Location = new Point(6, 54);
            fatPos.Name = "fatPos";
            fatPos.Size = new Size(61, 22);
            fatPos.TabIndex = 1;
            fatPos.Text = "Fats";
            fatPos.UseVisualStyleBackColor = true;
            fatPos.CheckedChanged += fatPos_CheckedChanged;
            // 
            // calPos
            // 
            calPos.AutoSize = true;
            calPos.Location = new Point(6, 26);
            calPos.Name = "calPos";
            calPos.Size = new Size(93, 22);
            calPos.TabIndex = 0;
            calPos.Text = "Calories";
            calPos.UseVisualStyleBackColor = true;
            calPos.CheckedChanged += calPos_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(proNeg);
            groupBox2.Controls.Add(calNeg);
            groupBox2.Controls.Add(sugNeg);
            groupBox2.Controls.Add(fatNeg);
            groupBox2.Controls.Add(salNeg);
            groupBox2.Controls.Add(satNeg);
            groupBox2.Controls.Add(carbNeg);
            groupBox2.Controls.Add(fibNeg);
            groupBox2.Font = new Font("Verdana", 12F);
            groupBox2.Location = new Point(208, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(190, 251);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Avoid";
            // 
            // proNeg
            // 
            proNeg.AutoSize = true;
            proNeg.Location = new Point(6, 222);
            proNeg.Name = "proNeg";
            proNeg.Size = new Size(85, 22);
            proNeg.TabIndex = 15;
            proNeg.Text = "Protein";
            proNeg.UseVisualStyleBackColor = true;
            proNeg.CheckedChanged += proNeg_CheckedChanged;
            // 
            // calNeg
            // 
            calNeg.AutoSize = true;
            calNeg.Location = new Point(6, 26);
            calNeg.Name = "calNeg";
            calNeg.Size = new Size(93, 22);
            calNeg.TabIndex = 8;
            calNeg.Text = "Calories";
            calNeg.UseVisualStyleBackColor = true;
            calNeg.CheckedChanged += calNeg_CheckedChanged;
            // 
            // sugNeg
            // 
            sugNeg.AutoSize = true;
            sugNeg.Location = new Point(6, 194);
            sugNeg.Name = "sugNeg";
            sugNeg.Size = new Size(73, 22);
            sugNeg.TabIndex = 14;
            sugNeg.Text = "Sugar";
            sugNeg.UseVisualStyleBackColor = true;
            sugNeg.CheckedChanged += sugNeg_CheckedChanged;
            // 
            // fatNeg
            // 
            fatNeg.AutoSize = true;
            fatNeg.Location = new Point(6, 54);
            fatNeg.Name = "fatNeg";
            fatNeg.Size = new Size(61, 22);
            fatNeg.TabIndex = 9;
            fatNeg.Text = "Fats";
            fatNeg.UseVisualStyleBackColor = true;
            fatNeg.CheckedChanged += fatNeg_CheckedChanged;
            // 
            // salNeg
            // 
            salNeg.AutoSize = true;
            salNeg.Location = new Point(6, 166);
            salNeg.Name = "salNeg";
            salNeg.Size = new Size(59, 22);
            salNeg.TabIndex = 13;
            salNeg.Text = "Salt";
            salNeg.UseVisualStyleBackColor = true;
            salNeg.CheckedChanged += salNeg_CheckedChanged;
            // 
            // satNeg
            // 
            satNeg.AutoSize = true;
            satNeg.Location = new Point(6, 82);
            satNeg.Name = "satNeg";
            satNeg.Size = new Size(145, 22);
            satNeg.TabIndex = 10;
            satNeg.Text = "Saturated fats";
            satNeg.UseVisualStyleBackColor = true;
            satNeg.CheckedChanged += satNeg_CheckedChanged;
            // 
            // carbNeg
            // 
            carbNeg.AutoSize = true;
            carbNeg.Location = new Point(6, 138);
            carbNeg.Name = "carbNeg";
            carbNeg.Size = new Size(145, 22);
            carbNeg.TabIndex = 12;
            carbNeg.Text = "Carbohydrates";
            carbNeg.UseVisualStyleBackColor = true;
            carbNeg.CheckedChanged += carbNeg_CheckedChanged;
            // 
            // fibNeg
            // 
            fibNeg.AutoSize = true;
            fibNeg.Location = new Point(6, 110);
            fibNeg.Name = "fibNeg";
            fibNeg.Size = new Size(67, 22);
            fibNeg.TabIndex = 11;
            fibNeg.Text = "Fibre";
            fibNeg.UseVisualStyleBackColor = true;
            fibNeg.CheckedChanged += fibNeg_CheckedChanged;
            // 
            // saveBtn
            // 
            saveBtn.Font = new Font("Verdana", 12F);
            saveBtn.Location = new Point(267, 336);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(131, 23);
            saveBtn.TabIndex = 2;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // closeBtn
            // 
            closeBtn.Font = new Font("Verdana", 12F);
            closeBtn.Location = new Point(267, 365);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(131, 23);
            closeBtn.TabIndex = 3;
            closeBtn.Text = "Close";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += closeBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 12F);
            label1.Location = new Point(12, 275);
            label1.Name = "label1";
            label1.Size = new Size(95, 18);
            label1.TabIndex = 4;
            label1.Text = "Diet Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 12F);
            label2.Location = new Point(12, 305);
            label2.Name = "label2";
            label2.Size = new Size(140, 18);
            label2.TabIndex = 5;
            label2.Text = "Diet Description";
            // 
            // dietName
            // 
            dietName.Location = new Point(113, 275);
            dietName.Name = "dietName";
            dietName.Size = new Size(285, 23);
            dietName.TabIndex = 6;
            // 
            // dietDesc
            // 
            dietDesc.Location = new Point(12, 326);
            dietDesc.Name = "dietDesc";
            dietDesc.Size = new Size(244, 96);
            dietDesc.TabIndex = 7;
            dietDesc.Text = "";
            // 
            // CustomDietsCreator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 434);
            Controls.Add(dietDesc);
            Controls.Add(dietName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(closeBtn);
            Controls.Add(saveBtn);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "CustomDietsCreator";
            Text = "CustomDietsCreator";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox proPos;
        private CheckBox sugPos;
        private CheckBox salPos;
        private CheckBox carbPos;
        private CheckBox fibPos;
        private CheckBox satPos;
        private CheckBox fatPos;
        private CheckBox calPos;
        private CheckBox proNeg;
        private CheckBox calNeg;
        private CheckBox sugNeg;
        private CheckBox fatNeg;
        private CheckBox salNeg;
        private CheckBox satNeg;
        private CheckBox carbNeg;
        private CheckBox fibNeg;
        private Button saveBtn;
        private Button closeBtn;
        private Label label1;
        private Label label2;
        private TextBox dietName;
        private RichTextBox dietDesc;
    }
}