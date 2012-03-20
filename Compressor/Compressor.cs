using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Compressor
{
    public class Compressor
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Compressor - no arguments supplied");
                return;
            }

            string source = args[0];
            string outputFile = args[1];

            Compressor compressor = new Compressor(source, Path.Combine(source, outputFile));
            Console.WriteLine("Compressor - begin processing");
            compressor.Execute();
            Console.WriteLine("Compressor - ** complete **\n\n");        
        }

        public Compressor(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }

        public string Source { get; private set; }
        public string Destination { get; private set; }

        public void Execute()
        {
            IProcessor processor = null;
            string processExtension = Path.GetExtension(Destination).ToLower();

            if(processExtension == ".js")
            {
                processor = new JavaScriptProcessor(Source, Destination);
                Console.WriteLine("Compressor - compressing scripts");
            }
            else if (processExtension == ".css")
            {
                processor = new StyleSheetProcessor(Source, Destination);
                Console.WriteLine("Compressor - compressing css");
            }
                
            
            processor.Process();

            Console.WriteLine("Compressor - output dir: {0}", Destination);
        }

       
    }
}
