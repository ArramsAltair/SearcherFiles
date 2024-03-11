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

            btnSearch.Enabled = false;
            btnPause.Enabled  = true;
            btnStop.Enabled   = true;
        }

        private void SearchManager_Pause()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(SearchManager_Pause));
            }

            btnSearch.Enabled = true;
            btnPause.Enabled = false;
        }

        public void SearchManager_Stop()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(SearchManager_Stop));
            }

            btnSearch.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }

        private void SearchManager_Searched(FileInfo file)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<FileInfo>(SearchManager_Searched), new object[] { file });
            }

            TreeNode node = GetNode(file.Directory);

            node.Nodes.Add(file.Name);
        }

        private void SearchManager_Progress(string currentDir, int totalCount, int searchCount)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, int, int>(SearchManager_Progress), new object[] { currentDir, totalCount, searchCount });
            }

            lbProgress.Text = $"Обработано: {totalCount}\nНайдено: {searchCount}\nПапка: {currentDir}";
        }

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

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            searchManager.Stop();

            ObjectHelper.Serialize(_settings, SettingPath);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Программа будет закрыта!\nПродолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

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

        private void tbPath_Leave(object sender, EventArgs e)
        {
            _settings.DefaultPath = tbPath.Text;
            tbSearchPattern.Text = _settings.SearchPattern;
        }

        private void tbSearchPattern_Leave(object sender, EventArgs e)
        {
            _settings.SearchPattern = tbSearchPattern.Text;
            tbSearchPattern.Text = _settings.SearchPattern;
        }

        private TreeNode? GetNode(DirectoryInfo dir)
        {
            if (dir == null)
            {
                return null;
            }

            TreeNode paeent = GetNode(dir.Parent);

            if (paeent == null)
            {
                return FindNode(tvMain.Nodes, dir);
            }

            return FindNode(paeent.Nodes, dir);
        }

        private TreeNode FindNode(TreeNodeCollection nodes, DirectoryInfo dir)
        {
            TreeNode node = nodes.Cast<TreeNode>().FirstOrDefault(w => w.Text == dir.Name);

            if (node == null)
            {
                node = new TreeNode(dir.Name);
                nodes.Add(node);
            }

            return node;
        }
    }
}