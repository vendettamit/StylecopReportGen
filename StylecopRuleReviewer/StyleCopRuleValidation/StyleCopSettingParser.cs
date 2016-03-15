using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StyleCopRuleValidation
{
    public static class StyleCopSettingParser
    {
        public static T Load<T>(string serializedData) where T: class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            var reader = new StringReader(serializedData);
            try
            {
                var obj = serializer.Deserialize(reader);

                if (obj == null)
                {
                    throw new InvalidDataException("Failed to load the settings in memory.");
                }

                return (T)obj;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);

                return default(T);
            }
        }
    }
}
