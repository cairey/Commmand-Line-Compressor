using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Compressor
{
    public class StyleSheetProcessor : IProcessor
    {
        public string SourceDir { get; set; }
        public string DestinationDir { get; set; }
        public string ConfigPath { get; set; }

        public StyleSheetProcessor(string sourceDir, string destinationDir)
        {
            SourceDir = sourceDir;
            DestinationDir = destinationDir;
            ConfigPath = Path.Combine(SourceDir, "CompressorConfig.txt");
        }

        public void Process()
        {
            Console.WriteLine("Compressor - locating CompressorConfig.txt");
            string[] stylesheetPaths = File.ReadAllLines(ConfigPath);
            string combined = CombineStyles(stylesheetPaths);
            string combinedAndMinified = MinifyStyle(combined);

            File.WriteAllText(DestinationDir, combinedAndMinified, Encoding.UTF8); 
        }

        private string CombineStyles(string[] stylePaths)
        {
            StringBuilder stylesheetsCombined = new StringBuilder();
            foreach(string stylesheet in stylePaths)
            {
                Console.WriteLine("Compressor - {0}", stylesheet);
                string stylePath = Path.Combine(SourceDir, stylesheet);
                stylesheetsCombined.Append(File.ReadAllText(stylePath));
            }


            return stylesheetsCombined.ToString();
        }

        private string MinifyStyle(string style)
        {
            StylesheetMinifier ssm = new StylesheetMinifier();
            return ssm.Compress(style, 0);
        }




    }
}
