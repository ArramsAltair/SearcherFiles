using SearcherOfFiles.Components;

namespace SearcherOfFiles
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSearch = new Button();
            btnPause = new Button();
            btnStop = new Button();
            tbPath = new TextBox();
            label1 = new Label();
            label2 = new Label();
            tbSearchPattern = new TextBox();
            tvMain = new TreeViewExt();
            btnOpen = new Button();
            lbProgress = new Label();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(680, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(108, 29);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Поиск";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnPause
            // 
            btnPause.Enabled = false;
            btnPause.Location = new Point(680, 47);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(108, 29);
            btnPause.TabIndex = 1;
            btnPause.Text = "Пауза";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(680, 82);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(108, 29);
            btnStop.TabIndex = 2;
            btnStop.Text = "Стоп";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // tbPath
            // 
            tbPath.Location = new Point(62, 12);
            tbPath.Name = "tbPath";
            tbPath.Size = new Size(565, 27);
            tbPath.TabIndex = 3;
            tbPath.Leave += tbPath_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(44, 20);
            label1.TabIndex = 4;
            label1.Text = "Путь:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(48, 20);
            label2.TabIndex = 6;
            label2.Text = "Файл:";
            // 
            // tbSearchPattern
            // 
            tbSearchPattern.Location = new Point(62, 47);
            tbSearchPattern.Name = "tbSearchPattern";
            tbSearchPattern.Size = new Size(609, 27);
            tbSearchPattern.TabIndex = 5;
            tbSearchPattern.Leave += tbSearchPattern_Leave;
            // 
            // tvMain
            // 
            tvMain.Location = new Point(12, 192);
            tvMain.Name = "tvMain";
            tvMain.Size = new Size(776, 246);
            tvMain.TabIndex = 9;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(633, 12);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(41, 29);
            btnOpen.TabIndex = 10;
            btnOpen.Text = "...";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // lbProgress
            // 
            lbProgress.Location = new Point(12, 91);
            lbProgress.Name = "lbProgress";
            lbProgress.Size = new Size(653, 89);
            lbProgress.TabIndex = 11;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lbProgress);
            Controls.Add(btnOpen);
            Controls.Add(tvMain);
            Controls.Add(label2);
            Controls.Add(tbSearchPattern);
            Controls.Add(label1);
            Controls.Add(tbPath);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnSearch);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Searcher";
            FormClosing += FormMain_FormClosing;
            FormClosed += FormMain_FormClosed;
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSearch;
        private Button btnPause;
        private Button btnStop;
        private TextBox tbPath;
        private Label label1;
        private Label label2;
        private TextBox tbSearchPattern;
        private TreeViewExt tvMain;
        private Button btnOpen;
        private Label lbProgress;
    }
}