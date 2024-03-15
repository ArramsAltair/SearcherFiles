using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SearcherOfFiles.Classes
{
    /// <summary>
    /// Описание класса Searcher
    /// </summary>
    internal class Searcher
    {
        #region private fields

        private AppSettings _setting;
        private ManualResetEvent _manualResetEvent = new ManualResetEvent(false);
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private Thread? _thread;
        private Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _timeCount;


        private int _totalCount;
        private int _searchCount;

        private bool _onStop;

        #endregion private fields

        #region events

        public event Action<bool>? OnStart;

        public event Action? OnPause;

        public event Action? OnStop;


        public event Action<string, int, int, TimeSpan>? OnProgress;

        public event Action<FileInfo>? OnSearched;

        #endregion events

        /// <summary>
        /// Реализация конструктора Searcher с предустановкой
        /// </summary>
        /// <param name="setting"></param>
        public Searcher(AppSettings setting)
        {
            _setting = setting;
        }

        /// <summary>
        /// Реализация метода Start
        /// </summary>
        public void Start()
        {
            if (!(_thread?.IsAlive ?? false))
            {
                _totalCount = 0;
                _searchCount = 0;
                _timeCount = new TimeSpan(0);
                _onStop = false;

                _tokenSource = new CancellationTokenSource();

                _stopwatch.Restart();

                _thread = new Thread(DoSearch);
                _thread.IsBackground = true;
                _thread.Start();

                OnStart?.Invoke(true);
            }
            else
            {
                OnStart?.Invoke(false);
            }
            _stopwatch.Start();
            _onStop = false;
            _manualResetEvent.Set();
        }

        /// <summary>
        /// Релизация метода Pause
        /// </summary>
        public void Pause()
        {
            _onStop = false;

            _manualResetEvent.Reset();

            _stopwatch.Stop();

            OnPause?.Invoke();
        }

        /// <summary>
        /// Реализация метода Stop
        /// </summary>
        public void Stop()
        {
            if (_thread?.IsAlive ?? false)
            {
                _tokenSource.Cancel();
            }
            _onStop = true;

            _manualResetEvent.Set();

            _stopwatch.Stop();

            OnStop?.Invoke();
        }

        /// <summary>
        /// Реализация метода DoSearch
        /// </summary>
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

        /// <summary>
        /// Реализация метода ScanFolder
        /// </summary>
        /// <param name="directoryInfo"></param>
        private void ScanFolder(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null)
            {
                return;
            }

            _manualResetEvent.WaitOne();

            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                if (_onStop)
                {
                    return;
                }

                try
                {
                    ScanFolder(directory);
                }
                catch (Exception ex) 
                {
                    
                }
            }

            if (_onStop)
            {
                return;
            }
            _totalCount++;

            _timeCount = _stopwatch.Elapsed;

            OnProgress?.Invoke(directoryInfo.FullName, _totalCount, _searchCount, _timeCount);

            foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (_onStop)
                {
                    return;
                }

                _manualResetEvent.WaitOne();

                try
                {
                    if (new Regex(_setting.SearchPattern).IsMatch(file.Name))
                    {
                        if (_onStop)
                        {
                            return;
                        }

                        _searchCount++;

                        _timeCount = _stopwatch.Elapsed;

                        OnSearched?.Invoke(file);
                    }
                }
                catch (Exception ex)
                {
                }

                _timeCount = _stopwatch.Elapsed;

                OnProgress?.Invoke(directoryInfo.FullName, _totalCount, _searchCount, _timeCount);
            }
        }
    }
}
