using System.Text.RegularExpressions;

namespace SearcherOfFiles.Classes
{
    internal class Searcher
    {
        #region private fields

        private AppSettings _setting;
        private ManualResetEvent _manualResetEvent = new ManualResetEvent(false);
        private Thread? _thread;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private int _totalCount;
        private int _searchCount;

        #endregion private fields

        #region events

        public event Action<bool>? OnStart;

        public event Action? OnPause;

        public event Action? OnStop;


        public event Action<string, int, int>? OnProgress;

        public event Action<FileInfo>? OnSearched;

        #endregion events

        public Searcher(AppSettings setting)
        {
            _setting = setting;
        }

        public void Start()
        {
            if (!(_thread?.IsAlive ?? false))
            {
                _totalCount = 0;
                _searchCount = 0;

                _tokenSource = new CancellationTokenSource();

                _thread = new Thread(DoSearch);
                _thread.Start();

                OnStart?.Invoke(true);
            }
            else
            {
                OnStart?.Invoke(false);
            }

            _manualResetEvent.Set();
        }

        public void Pause()
        {
            _manualResetEvent.Reset();

            OnPause?.Invoke();
        }

        public void Stop()
        {
            if (_thread?.IsAlive ?? false)
            {
                _tokenSource.Cancel();
            }

            _manualResetEvent.Set();

            OnStop?.Invoke();
        }

        public void DoSearch()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_setting.DefaultPath) || !Directory.Exists(_setting.DefaultPath))
                {
                    return;
                }

                _manualResetEvent.WaitOne();

                ScanFolder(new DirectoryInfo(_setting.DefaultPath));
            }
            finally
            {
                OnStop?.Invoke();
            }
        }

        private void ScanFolder(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null)
            {
                return;
            }

            _manualResetEvent.WaitOne();

            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                ScanFolder(directory);
            }

            _totalCount++;

            OnProgress?.Invoke(directoryInfo.FullName, _totalCount, _searchCount);

            foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                _manualResetEvent.WaitOne();

                if (new Regex(_setting.SearchPattern).IsMatch(file.Name))
                {
                    _searchCount++;

                    OnSearched?.Invoke(file);
                }

                OnProgress?.Invoke(directoryInfo.FullName, _totalCount, _searchCount);
            }
        }
    }
}
