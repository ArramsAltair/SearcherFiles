using SearcherOfFiles.Helpers;
using SearcherOfFiles.Managers;
using System.Diagnostics;

namespace SearcherOfFiles
{
    public partial class Form1 : Form
    {
        SearchManager searchManager;
        public Form1()
        {
            InitializeComponent();
            searchManager = new SearchManager();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (searchManager == null)
            {
                return;
            }
            searchManager.Start(tBPath.Text, tBFileName.Text);
            string[] files = searchManager.GetFiles();
            lBResult.Items.Clear();
            if (files != null && files.Length > 0)
            {
                tV.Nodes.Clear();
                tV.Nodes.Add(new TreeNode(tBPath.Text));
                //tV.Nodes.Add(tBPath.Text);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    lBResult.Items.Add(fileInfo.Name);
                    tV.Nodes[0].Nodes.Add(fileInfo.Name);

                }
                /*for (int i = 0; i < files.Length; i++)
                {
                    tBResult.Text += files[i] + "\r\n";
                }*/
            }


        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (this.btnPause.Text.ToString() == "Пауза")
            {
                this.btnPause.Text = "Продолжить";
                searchManager.Pause();
                /*string path = "C:\\Pack\\first*";
                if (File.Exists(path))
                    Process.Start(path);
                else
                    MessageBox.Show("Файл не найден");*/

            }
            else
            {
                this.btnPause.Text = "Пауза";
                searchManager.Start();
            }
        }

        private void tV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string argument = "/start, ";

            //System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            searchManager.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            searchManager.LoadParameters();
            tBPath.Text = searchManager.DefaultPath;
            tBFileName.Text = searchManager.FileName;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            searchManager.SaveParameters();
        }
    }
}