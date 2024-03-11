namespace SearcherOfFiles
{
    partial class Form1
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
            tBPath = new TextBox();
            label1 = new Label();
            label2 = new Label();
            tBFileName = new TextBox();
            tBResult = new TextBox();
            lBResult = new ListBox();
            tV = new TreeView();
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
            btnStop.Location = new Point(680, 82);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(108, 29);
            btnStop.TabIndex = 2;
            btnStop.Text = "Стоп";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // tBPath
            // 
            tBPath.Location = new Point(337, 14);
            tBPath.Name = "tBPath";
            tBPath.Size = new Size(328, 27);
            tBPath.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(283, 17);
            label1.Name = "label1";
            label1.Size = new Size(44, 20);
            label1.TabIndex = 4;
            label1.Text = "Путь:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(283, 52);
            label2.Name = "label2";
            label2.Size = new Size(48, 20);
            label2.TabIndex = 6;
            label2.Text = "Файл:";
            // 
            // tBFileName
            // 
            tBFileName.Location = new Point(337, 49);
            tBFileName.Name = "tBFileName";
            tBFileName.Size = new Size(328, 27);
            tBFileName.TabIndex = 5;
            // 
            // tBResult
            // 
            tBResult.AcceptsReturn = true;
            tBResult.Location = new Point(283, 149);
            tBResult.Multiline = true;
            tBResult.Name = "tBResult";
            tBResult.ScrollBars = ScrollBars.Vertical;
            tBResult.Size = new Size(159, 104);
            tBResult.TabIndex = 7;
            // 
            // lBResult
            // 
            lBResult.FormattingEnabled = true;
            lBResult.ItemHeight = 20;
            lBResult.Location = new Point(108, 149);
            lBResult.Name = "lBResult";
            lBResult.Size = new Size(150, 104);
            lBResult.TabIndex = 8;
            // 
            // tV
            // 
            tV.Location = new Point(109, 284);
            tV.Name = "tV";
            tV.Size = new Size(151, 121);
            tV.TabIndex = 9;
            tV.AfterSelect += tV_AfterSelect;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tV);
            Controls.Add(lBResult);
            Controls.Add(tBResult);
            Controls.Add(label2);
            Controls.Add(tBFileName);
            Controls.Add(label1);
            Controls.Add(tBPath);
            Controls.Add(btnStop);
            Controls.Add(btnPause);
            Controls.Add(btnSearch);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSearch;
        private Button btnPause;
        private Button btnStop;
        private TextBox tBPath;
        private Label label1;
        private Label label2;
        private TextBox tBFileName;
        private TextBox tBResult;
        private ListBox lBResult;
        private TreeView tV;
    }
}