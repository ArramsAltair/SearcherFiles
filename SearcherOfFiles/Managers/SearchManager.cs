using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualBasic.Logging;
using SearcherOfFiles.Helpers;
using SearcherOfFiles.Interfaces;
using SearcherOfFiles.Searchers;

namespace SearcherOfFiles.Managers
{
    internal class SearchManager : IRunnable
    {
        private Thread thread;

        private Searcher searcher;

        private string[] files;

        public string DefaultPath { get; private set;}

        public string FileName { get; private set;}

        private const string configFile = "def.xml";

        public SearchManager() 
        {
            searcher = new Searcher(DefaultPath);
        }

        public string[] GetFiles() 
        {
            return files;
        }

        public void Start() 
        {
            
        }

        public void Start(string path, string fileName) 
        {
            if (searcher == null) 
            {
                return;
            }
            this.DefaultPath = path;
            this.FileName = fileName;
            searcher.Path = path;
            searcher.FileName = fileName;
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
            searcher.Pause();
        }

        public void Stop() 
        {
            if (searcher == null)
            {
                return;
            }
            searcher.Stop();
        }
        
        public void SaveParameters() 
        {
            Element element = new Element();
            element.DirectoryPath = this.DefaultPath;
            element.FileName = this.FileName;
            List<Element> list = new List<Element>();
            list.Add(element);
            Serializer.Serialize(list, configFile);
        }

        public void LoadParameters() 
        {
            var element = Serializer.Deserialize(configFile).ElementAt(0);
            this.DefaultPath = element.DirectoryPath;
            this.FileName = element.FileName;
        }
    }
}
