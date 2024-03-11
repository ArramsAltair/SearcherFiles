using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherOfFiles.Interfaces
{
    internal interface IRunnable
    {
        void Run();

        void Pause();

        void Stop();

    }
}
