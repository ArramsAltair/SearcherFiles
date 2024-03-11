using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SearcherOfFiles.Interfaces;

namespace SearcherOfFiles.Searchers
{
    internal class Searcher : IDisposable
    {
        public string Path { get; set; }

        public string Catalog { get; set; }

        public string FileName { get; set; }

        public string DefaultPath { get; set; }

        

        public Searcher(string defaultPath)
        {
            this.DefaultPath = defaultPath;
        }

        public string[] Search() 
        {       
            if (Path == null || Path == "")
            {
                return Directory.GetFiles(DefaultPath);
            }
            try 
            {
                if(FileName == null ) 
                {
                    return Directory.GetFiles(Path);
                }
                return Directory.GetFiles(Path, FileName);
            }
            catch 
            {
                return null;
            }
            
        }

        public void Start() 
        {

        }

        public void Pause() 
        {
            
        }

        public void Stop() 
        {
            
        }
        public void Dispose() 
        {
        
        }
    }
}
