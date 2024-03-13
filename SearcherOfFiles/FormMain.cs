using SearcherOfFiles.Classes;
using SearcherOfFiles.Helpers;
using System.Configuration;

namespace SearcherOfFiles
{
    public partial class FormMain : Form
    {
        private AppSettings _settings;

        private string SettingPath => ConfigurationManager.AppSettings["SettingPath"] ?? "";

        Searcher searchManager;

        public FormMain()
        {
            InitializeComponent();
        }

        private void SearchManager_RunSearch(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    btnPause.Text = "Пауза";
                }));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchManager.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            searchManager.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            searchManager.Stop();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newSearch"></param>
        private void SearchManager_Start(bool newSearch)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SearchManager_Start), new object[] { newSearch });
            }

            if (newSearch)
            {
                tvMain.Nodes.Clear();
            }

            tbPath.Enabled = false;
            tbSearchPattern.Enabled = false;

            btnOpen.Enabled = false;
            btnSearch.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
        }


        /// <summary>
        /// 
        /// </summary>
        private void SearchManager_Pause()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(SearchManager_Pause));
            }

            btnSearch.Enabled = true;
            btnPause.Enabled = false;
        }


        /// <summary>
        /// 
        /// </summary>
        public void SearchManager_Stop()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(SearchManager_Stop));
            }

            tbPath.Enabled = true;
            tbSearchPattern.Enabled = true;

            btnOpen.Enabled = true;
            btnSearch.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        private void SearchManager_Searched(FileInfo file)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<FileInfo>(SearchManager_Searched), new object[] { file });
            }

            try
            {
                TreeNode node = GetNode(file.Directory);
                node.Nodes.Add(file.Name);
            }
            catch
            {
                this.Invoke(new Action<FileInfo>(SearchManager_Searched), new object[] { file });
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentDir"></param>
        /// <param name="totalCount"></param>
        /// <param name="searchCount"></param>
        /// <param name="timeCount"></param>
        private void SearchManager_Progress(string currentDir, int totalCount, int searchCount, TimeSpan timeCount)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, int, int, TimeSpan>(SearchManager_Progress), new object[] { currentDir, totalCount, searchCount, timeCount });
            }
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", timeCount.Hours, timeCount.Minutes, timeCount.Seconds);
            lbProgress.Text = $"Обработано: {totalCount}\nНайдено: {searchCount}\nВремя: {elapsedTime}\nПапка: {currentDir}";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            _settings = ObjectHelper.Deserialize<AppSettings>(SettingPath) ?? new AppSettings();

            searchManager = new Searcher(_settings);

            searchManager.OnStart += SearchManager_Start;
            searchManager.OnPause += SearchManager_Pause;
            searchManager.OnStop += SearchManager_Stop;

            searchManager.OnProgress += SearchManager_Progress;
            searchManager.OnSearched += SearchManager_Searched;

            tbPath.Text = _settings.DefaultPath;
            tbSearchPattern.Text = _settings.SearchPattern;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            searchManager.Stop();

            ObjectHelper.Serialize(_settings, SettingPath);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Программа будет закрыта!\nПродолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _settings.DefaultPath = dialog.SelectedPath;

                tbPath.Text = _settings.DefaultPath;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPath_Leave(object sender, EventArgs e)
        {
            _settings.DefaultPath = tbPath.Text;
            tbSearchPattern.Text = _settings.SearchPattern;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearchPattern_Leave(object sender, EventArgs e)
        {
            _settings.SearchPattern = tbSearchPattern.Text;
            tbSearchPattern.Text = _settings.SearchPattern;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private TreeNode? GetNode(DirectoryInfo dir)
        {
            if (dir == null)
            {
                return null;
            }

            TreeNode parent = GetNode(dir.Parent);

            if (parent == null)
            {
                return FindNode(tvMain.Nodes, dir);
            }

            return FindNode(parent.Nodes, dir);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        private TreeNode FindNode(TreeNodeCollection? nodes, DirectoryInfo dir)
        {
            TreeNode? node = nodes.Cast<TreeNode>().FirstOrDefault(w => w.Text == dir.Name);

            if (node == null)
            {
                node = new TreeNode(dir.Name);
                nodes.Add(node);
            }

            return node;
        }
    }
}