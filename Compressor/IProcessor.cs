using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compressor
{
    public interface IProcessor
    {
        string SourceDir { get; set; }
        string DestinationDir { get; set; }
        string ConfigPath { get; set; }
        void Process();
    }
}
