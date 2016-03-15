using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleCopRuleValidation
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class StyleCopSettings
    {

        private StyleCopSettingsGlobalSettings globalSettingsField;

        private StyleCopSettingsParsers parsersField;

        private StyleCopSettingsAnalyzer[] analyzersField;

        private byte versionField;

        /// <remarks/>
        public StyleCopSettingsGlobalSettings GlobalSettings
        {
            get
            {
                return this.globalSettingsField;
            }
            set
            {
                this.globalSettingsField = value;
            }
        }

        /// <remarks/>
        public StyleCopSettingsParsers Parsers
        {
            get
            {
                return this.parsersField;
            }
            set
            {
                this.parsersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Analyzer", IsNullable = false)]
        public StyleCopSettingsAnalyzer[] Analyzers
        {
            get
            {
                return this.analyzersField;
            }
            set
            {
                this.analyzersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsGlobalSettings
    {

        private StyleCopSettingsGlobalSettingsStringProperty stringPropertyField;

        private StyleCopSettingsGlobalSettingsBooleanProperty booleanPropertyField;

        /// <remarks/>
        public StyleCopSettingsGlobalSettingsStringProperty StringProperty
        {
            get
            {
                return this.stringPropertyField;
            }
            set
            {
                this.stringPropertyField = value;
            }
        }

        /// <remarks/>
        public StyleCopSettingsGlobalSettingsBooleanProperty BooleanProperty
        {
            get
            {
                return this.booleanPropertyField;
            }
            set
            {
                this.booleanPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsGlobalSettingsStringProperty
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsGlobalSettingsBooleanProperty
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsParsers
    {

        private StyleCopSettingsParsersParser parserField;

        /// <remarks/>
        public StyleCopSettingsParsersParser Parser
        {
            get
            {
                return this.parserField;
            }
            set
            {
                this.parserField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsParsersParser
    {

        private StyleCopSettingsParsersParserParserSettings parserSettingsField;

        private string parserIdField;

        /// <remarks/>
        public StyleCopSettingsParsersParserParserSettings ParserSettings
        {
            get
            {
                return this.parserSettingsField;
            }
            set
            {
                this.parserSettingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ParserId
        {
            get
            {
                return this.parserIdField;
            }
            set
            {
                this.parserIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsParsersParserParserSettings
    {

        private StyleCopSettingsParsersParserParserSettingsBooleanProperty booleanPropertyField;

        /// <remarks/>
        public StyleCopSettingsParsersParserParserSettingsBooleanProperty BooleanProperty
        {
            get
            {
                return this.booleanPropertyField;
            }
            set
            {
                this.booleanPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsParsersParserParserSettingsBooleanProperty
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsAnalyzer
    {

        private StyleCopSettingsAnalyzerRule[] rulesField;

        private object analyzerSettingsField;

        private string analyzerIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Rule", IsNullable = false)]
        public StyleCopSettingsAnalyzerRule[] Rules
        {
            get
            {
                return this.rulesField;
            }
            set
            {
                this.rulesField = value;
            }
        }

        /// <remarks/>
        public object AnalyzerSettings
        {
            get
            {
                return this.analyzerSettingsField;
            }
            set
            {
                this.analyzerSettingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AnalyzerId
        {
            get
            {
                return this.analyzerIdField;
            }
            set
            {
                this.analyzerIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsAnalyzerRule
    {

        private StyleCopSettingsAnalyzerRuleRuleSettings ruleSettingsField;

        private string nameField;

        /// <remarks/>
        public StyleCopSettingsAnalyzerRuleRuleSettings RuleSettings
        {
            get
            {
                return this.ruleSettingsField;
            }
            set
            {
                this.ruleSettingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsAnalyzerRuleRuleSettings
    {

        private StyleCopSettingsAnalyzerRuleRuleSettingsBooleanProperty booleanPropertyField;

        /// <remarks/>
        public StyleCopSettingsAnalyzerRuleRuleSettingsBooleanProperty BooleanProperty
        {
            get
            {
                return this.booleanPropertyField;
            }
            set
            {
                this.booleanPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class StyleCopSettingsAnalyzerRuleRuleSettingsBooleanProperty
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


}
