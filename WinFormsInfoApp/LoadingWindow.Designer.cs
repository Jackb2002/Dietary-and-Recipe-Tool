namespace WinFormsInfoApp
{
    partial class LoadingWindow
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
            LoadingBar = new ProgressBar();
            label1 = new Label();
            progLog = new Label();
            SuspendLayout();
            // 
            // LoadingBar
            // 
            LoadingBar.Location = new Point(12, 152);
            LoadingBar.Name = "LoadingBar";
            LoadingBar.Size = new Size(461, 23);
            LoadingBar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 29);
            label1.Name = "label1";
            label1.Size = new Size(301, 35);
            label1.TabIndex = 1;
            label1.Text = "Loading Application";
            // 
            // progLog
            // 
            progLog.AutoSize = true;
            progLog.Font = new Font("Verdana", 13.75F);
            progLog.Location = new Point(12, 64);
            progLog.Name = "progLog";
            progLog.Size = new Size(89, 23);
            progLog.TabIndex = 2;
            progLog.Text = "progLog";
            // 
            // LoadingWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(485, 187);
            Controls.Add(progLog);
            Controls.Add(label1);
            Controls.Add(LoadingBar);
            Margin = new Padding(3, 2, 3, 2);
            Name = "LoadingWindow";
            Text = "LoadingWindow";
            Load += LoadingWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar LoadingBar;
        private Label label1;
        private Label progLog;
    }
}