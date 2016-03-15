using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StyleCopRuleValidation
{
    class Program
    {
        public static List<SourceAnalyzer> cachedAnalyzer;

        static void Main(string[] args)
        {
            LoadAllCsharpRules();

            Dictionary<string, string> TotalRulesId = GetAllBaseRulesId();
            StringBuilder rules = new StringBuilder();

            foreach (var key in TotalRulesId.Keys)
            {
                rules.AppendFormat("{0} - {1}\r\n", TotalRulesId[key], key);
            }
            File.WriteAllText(@"c:\\temp\\AllRules.txt", rules.ToString());

            List<string> decidedIgnoredRulesId;
            List<string> currentIgnoredRulesId;

            var settingsFile = @"C:\Workspace_tfs\MAIN\Source\TestProject\Settings.StyleCop";
            var settingFileSource = @"C:\Workspace_tfs\MAIN\Source\StyleCop\Settings.StyleCop";

            var content = File.ReadAllText(settingsFile);
            var contentSource = File.ReadAllText(settingFileSource);


            var settings = StyleCopSettingParser.Load<StyleCopSettings>(content);
            currentIgnoredRulesId = GetRulesFromSettingsFiles(settings);

            var settingsSource = StyleCopSettingParser.Load<StyleCopSettings>(contentSource);
            decidedIgnoredRulesId = GetRulesFromSettingsFiles(settingsSource);

            rules.Clear();

            foreach(var key in TotalRulesId.Keys.Except(currentIgnoredRulesId))
            {
                rules.AppendFormat("{0} - {1}\r\n", TotalRulesId[key], key);
            }

            File.WriteAllText(@"c:\\temp\\currentIgnoredRules.txt", rules.ToString());

            rules.Clear();

            foreach (var key in TotalRulesId.Keys.Except(decidedIgnoredRulesId))
            {
                rules.AppendFormat("{0} - {1}\r\n", TotalRulesId[key], key);
            }

            File.WriteAllText(@"c:\\temp\\decidedIgnoredRules.txt", rules.ToString());

            Console.WriteLine("Finished generating report..");
            Console.Read();
        }

        public static List<SourceAnalyzer> LoadAllCsharpRules()
        {
            if (cachedAnalyzer != null)
                return cachedAnalyzer;

            cachedAnalyzer = new List<SourceAnalyzer>();

            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Rules");

            var rulesFiles = Directory.EnumerateFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var file in rulesFiles)
            {
                cachedAnalyzer.Add(StyleCopSettingParser.Load<SourceAnalyzer>(File.ReadAllText(file)));
            }

            return cachedAnalyzer;
        }

        public static Dictionary<string, string> GetAllBaseRulesId()
        {
            Dictionary<string, string> TotalRulesId = new Dictionary<string, string>();
            var rules = LoadAllCsharpRules();

            foreach (var ruleGroup in rules)
            {
                if (ruleGroup.Rule.Rule == null)
                {
                    foreach (var rule in ruleGroup.Rule.RuleGroups)
                    {
                        rule.Rule.ToList().ForEach(x => TotalRulesId.Add(x.Name, x.CheckId));
                    }
                }
                else
                {
                    foreach (var rule in ruleGroup.Rule.Rule)
                    {
                        TotalRulesId.Add(rule.Name, rule.CheckId);
                    }
                }
            }

            return TotalRulesId;
        }

        public static List<string> GetRulesFromSettingsFiles(StyleCopSettings settings)
        {
            List<string> configIgnoredRuleNames = new List<string>();

            foreach (var analyzer in settings.Analyzers)
            {
                foreach (var rule in analyzer.Rules)
                {
                    if(string.Equals(rule.RuleSettings.BooleanProperty.Value, "False", StringComparison.InvariantCultureIgnoreCase))
                    {
                        configIgnoredRuleNames.Add(rule.Name);
                    }
                }
            }

            return configIgnoredRuleNames;
        }
    }
}
