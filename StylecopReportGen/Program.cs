using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using XmlPrime;
using System.Linq;
using System.Threading.Tasks;

namespace StylecopReportGen
{
    internal class Program
    {
        /// <summary>
        /// Performs the transformation.
        /// </summary>
        /// <param name="inputPath">The filename of the context item document.</param>
        /// <param name="xsltPath">The filename of the XSLT transformation.</param>
        /// <param name="outputPath">The filename of the primary output.</param>
        private static void PerformTransformation(string inputPath, string xsltPath, string outputPath)
        {
            Debug.Assert(inputPath != null, "inputPath in null");
            Debug.Assert(xsltPath != null, "xsltPath in null");
            Debug.Assert(inputPath != null, "outputPath in null");

            // First, we create a new XmlNameTable instance. This will be used to share information such 
            // as element and attribute names between the XML documents and the transformation.
            var nameTable = new NameTable();

            // Next we create an XmlReaderSettings instance and set its NameTable property. 
            // In order for XmlPrime to work correctly all documents passed in to Xslt must be loaded
            // with the XmlNameTable used to compile the query. 
            var xmlReaderSettings = new XmlReaderSettings { NameTable = nameTable };

            // In order to transform  the document we load it into an XdmDocument.
            XdmDocument document;
            using (var reader = XmlReader.Create(inputPath, xmlReaderSettings))
            {
                document = new XdmDocument(reader);
            }

            // In order to describe how the transformation should be compiled we need set up an XsltSettings 
            // object.  This describes all the settings used for compilation.
            // In particular, we will set the name table used by the transformation to match the one we used 
            // earlier and we will set the context item type. 
            // By default the context item type is set to none, and so the context item cannot be set unless 
            // we override the type here. 
            var xsltSettings = new XsltSettings(nameTable) { ContextItemType = XdmType.Node };

            // We can then compile the transformation using the Compile method. 
            // This returns us an Xslt object encapsulating the transformation. 
            var xslt = Xslt.Compile(xsltPath, xsltSettings);

            // Now we have our transformation object we now just need to execute it. 
            // We use a DynamicContextSettings object which describes the parameters used to evaluate the query. 
            // In particular we will set the context item to be the document that we loaded earlier. 
            var contextItem = document.CreateNavigator();
            var settings = new DynamicContextSettings { ContextItem = contextItem };

            // We will use the ApplyTemplates method to initiate a transformation by applying templates 
            // in the default mode and serializing the primary result document to a stream.
            using (var outputStream = File.Create(outputPath))
            {
                xslt.ApplyTemplates(settings, outputStream);
            }

            // NOTE: We could just have used xslt.ApplyTemplates(contextItem, outputStream) in this simple case.
        }

        /// <summary>
        /// Performs a transformation on a specified file and serializes the result to a specified file.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>0 if the transformation was executed successfully; otherwise -1.</returns>
        public static int Main(string[] args)
        {
            const string stylecopReportFileName = "StyleCopViolations.xml";
            string stylecopXslt = "StyleCopViolations.xslt";

            Debug.Assert(args != null, "args in null");

            if (args.Length < 1)
            {
                var command = Environment.GetCommandLineArgs()[0];
                var name = Path.GetFileNameWithoutExtension(command);

                Console.Error.WriteLine("Usage: {0} <fullpath_solutionfile.sln> [optional]<Release/Debug/Build configuration name..> [optional]<stylecopTransform.xslt>", name);
                return -1;
            }

            string slnPath = args[0];
            string configuration = "Debug";

            if (args.Length >= 2)
            {
                configuration = args[1];
            }

            if (args.Length >= 3)
            {
                stylecopXslt = args[2];
            }

            var successcode = BuildStyleCopReport(stylecopReportFileName, stylecopXslt, slnPath, configuration);

            return successcode;
        }

        /// <summary>
        /// Parses the provided solution file and genrates stylecop report.
        /// </summary>
        private static int BuildStyleCopReport(string stylecopReportFileName, string stylecopXslt, string slnPath, string configuration)
        {
            var successCode = 0;

            List<ProjectReport> reports = new List<ProjectReport>();
            Solution sln = null;
            try
            {
                sln = new Solution(slnPath);
            }
            catch (Exception e)
            {
                WriteLineColored("======> An Error occurred while reading solution file.", ConsoleColor.Red);
                Console.Error.WriteLine(e.Message);
                return -1;
            }

            WriteLineColored("Scanning solution... ", ConsoleColor.White);

            // Filter out projects those have style cop violations generated.
            foreach (var item in sln.Projects.Where(project => project.ProjectType == "KnownToBeMSBuildFormat"))
            {
                var projectPath = Path.GetDirectoryName(item.RelativePath);

                item.AboslutePath = Path.Combine(Path.GetDirectoryName(slnPath), projectPath);

                // Add obj path and look for Style violation file. 
                var reportFilePath = Path.Combine(item.AboslutePath, "Obj", configuration, stylecopReportFileName);

                if (File.Exists(reportFilePath))
                {
                    reports.Add(new ProjectReport { ProjectName = item.ProjectName, StyleCopReportFile = reportFilePath });
                }
            }

            // No violations found!
            if (reports.Count == 0)
            {
                WriteLineColored("No style cop violation xml file found.", ConsoleColor.White);
                WriteLineColored("You have no violations in any projects.", ConsoleColor.Green);
                WriteLineColored("Note - If you were expecting violations try below actions:", ConsoleColor.White);
                WriteLineColored(" - Make sure projects are enabled to run style cop on build.", ConsoleColor.Cyan);
                WriteLineColored(" - Rebuild the solution to generate style cop violations file.", ConsoleColor.Cyan);

                return successCode;
            }

            // Print project names
            reports.ForEach(report => WriteLineColored(string.Format("{0}", report.ProjectName), ConsoleColor.DarkGray));
            WriteLineColored(string.Format("{0} project(s) found to have style cop rules violations.", reports.Count), ConsoleColor.White);

            // Start report generation
            WriteLineColored("Generating report...", ConsoleColor.DarkYellow);

            var outputPath = BuildOutputPath();

            // Notice execution time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string lastErrorMessage = "";

            var cancellationToken = new System.Threading.CancellationToken();

            // Process files in parallel mode
            Parallel.ForEach<ProjectReport>(reports, (rpt, state) =>
            {
                try
                {
                    PerformTransformation(rpt.StyleCopReportFile, stylecopXslt, string.Concat(Path.Combine(outputPath, rpt.ProjectName), ".html"));
                }
                catch (Exception e)
                {
                    lastErrorMessage = e.Message;
                    successCode = -1;
                    state.Break();
                }
            });

            stopwatch.Stop();

            // If any error occurred disply error and return immediately.
            if (successCode == -1)
            {
                WriteLineColored("======> An Error occurred.", ConsoleColor.Red);
                Console.Error.WriteLine(lastErrorMessage);
                WriteLineColored(string.Format("Processing time: {0} seconds", stopwatch.Elapsed.TotalSeconds), ConsoleColor.Cyan);

                return successCode;
            }

            // Report generated! Send message to console.
            WriteLineColored("Reports generated successfully!!", ConsoleColor.White);
            WriteColored(string.Format("at location: {0}\n", outputPath), ConsoleColor.White);
            WriteLineColored(string.Format("Processing time: {0} seconds", stopwatch.Elapsed.TotalSeconds), ConsoleColor.Cyan);
            Console.WriteLine("Press Any key to exit....");

            return successCode;
        }

        private static string BuildOutputPath()
        {
            string outDirName = string.Format("ReportGen_{0}", DateTime.Now.ToString("MM_dd_yyyy_HH-mm-ss"));
            var outDirPath = Path.Combine(Environment.CurrentDirectory, outDirName);
            if (Directory.Exists(outDirPath))
            {
                Directory.Delete(outDirPath);
            }

            Directory.CreateDirectory(outDirPath);

            return outDirPath;
        }

        private static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        private static void WriteLineColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    internal class ProjectReport
    {
        public string StyleCopReportFile { get; set; }

        public string ProjectName { get; set; }

    }
}
