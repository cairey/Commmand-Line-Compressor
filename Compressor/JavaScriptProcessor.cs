using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Compressor
{
    public class JavaScriptProcessor : IProcessor
    {
        public JavaScriptProcessor(string sourceDir, string destinationDir)
        {
            SourceDir = sourceDir;
            DestinationDir = destinationDir;
            ConfigPath = Path.Combine(SourceDir, "CompressorConfig.txt");
        }

        public string SourceDir { get; set; }
        public string DestinationDir { get; set; }
        public string ConfigPath { get; set; }


        public void Process()
        {
            Console.WriteLine("Compressor - locating CompressorConfig.txt");
            string[] scriptPaths = File.ReadAllLines(ConfigPath);
            string combined = CombineScripts(scriptPaths);
            string combinedAndMinified = MinifyScript(combined);

            File.WriteAllText(DestinationDir, combinedAndMinified, Encoding.UTF8); 
        }

        private string CombineScripts(string[] scriptPaths)
        {
            StringBuilder scriptsCombined = new StringBuilder();
            foreach(string js in scriptPaths)
            {
                Console.WriteLine("Compressor - {0}", js);
                string scriptPath = Path.Combine(SourceDir, js);
                scriptsCombined.Append(File.ReadAllText(scriptPath));
            }

            return scriptsCombined.ToString();
        }

        private string MinifyScript(string script)
        {
            JavaScriptMinifier jsm = new JavaScriptMinifier();
            return jsm.Minify(script);
        }
    }
}
