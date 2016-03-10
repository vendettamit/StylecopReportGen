using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace StylecopReportGen
{
    class StyleCopResultsFileMerge
    {
        static void MainTest(string[] args)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "testData");

            var mergedFile = MergeAllFiles(path);
        }

        public static string MergeAllFiles(string folder, string mergeFileName = "StyleCopViolations.xml")
        {
            string searchMask = "StyleCopViolations*.xml";
            string tempFileName = "StyleCopViolations_Merged.xml";

            // Get the files to merge                                       
            var files = Directory.GetFiles(folder, searchMask, SearchOption.TopDirectoryOnly);

            if (files == null)
            {
                Console.WriteLine("No stylecop violations results found.");
            }

            if (files.Length > 1)
            {
                const string startTag = "<StyleCopViolations>";
                const string endTag = "</StyleCopViolations>";
                var path = Path.GetDirectoryName(files.First());

                var outputFile = string.Concat(path, "\\", tempFileName);
                // Clear the file content                
                File.WriteAllText(outputFile, "");

                File.AppendAllText(outputFile, startTag);

                foreach (var file in files)
                {
                    var data = GetContent(startTag, endTag, file);

                    File.AppendAllText(outputFile, data);

                    File.Delete(file);
                }

                File.AppendAllText(outputFile, endTag);
                mergeFileName = path + "\\" + mergeFileName;
                File.Move(outputFile, mergeFileName);
            }
            else if (files.Count() == 1)
            {
                mergeFileName = string.Concat(Path.GetDirectoryName(files.First()), mergeFileName);
                File.Copy(files.First(), mergeFileName, overwrite: true);
            }

            return mergeFileName;
        }

        private static string GetContent(string startTag, string endTag, string file)
        {
            string content = File.ReadAllText(file);

            int pFrom = content.IndexOf(startTag) + startTag.Length;
            int pTo = content.LastIndexOf(endTag);

            string result = content.Substring(pFrom, pTo - pFrom);

            return result;
        }
    }
}
