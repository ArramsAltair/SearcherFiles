using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Logging;
using SearcherOfFiles.Interfaces;
using SearcherOfFiles.Searchers;

namespace SearcherOfFiles.Managers
{
    internal class SearchManager : IRunnable
    {
        private Thread thread;

        private Searcher searcher;

        private string[] files;

        private string defaultPath = "C:";

        public SearchManager() 
        {
            searcher = new Searcher(defaultPath);
        }

        public string[] GetFiles() 
        {
            return files;
        }

        public void Start() 
        {
            if (searcher == null) 
            {
                return;
            }
            if (thread == null)
            {
                thread = new Thread(this.Run);
                //thread.IsBackground = true;
                thread.Name = "searcher";
                thread.Start();
                MessageBox.Show(thread.ThreadState.ToString());
                return;
            }
            else if(!thread.IsAlive)
            {
                thread.Join();
                MessageBox.Show(thread.ThreadState.ToString());
            }

            searcher.Start();
            this.Run();
        }

        public void Run() 
        {
            if (searcher == null)
            {
                return;
            }
            files = searcher.Search();
        }

        public void Pause() 
        {
            if (searcher == null)
            {
                return;
            }

            if (thread == null)
            {
                return;
            }
            thread.Join();
            MessageBox.Show(thread.ThreadState.ToString());
            searcher.Pause();
        }

        public void Stop() 
        {
            if (searcher == null)
            {
                return;
            }
            if (!thread.IsAlive)
            {
                MessageBox.Show(thread.ThreadState.ToString());
                return;
            }
            thread.Abort();
            MessageBox.Show(thread.ThreadState.ToString());
            searcher.Stop();
        }

        public void SetPuth(string path)
        {
            if (searcher == null)
            {
                return;
            }
            searcher.Path = path;
        }

        public void SetFindFileName(string filename)
        {
            searcher.FileName = filename;
        }
    }
}
