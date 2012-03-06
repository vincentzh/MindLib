using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;

namespace MindHarbor.GenClassLib.Configuration
{
    public class ConfigurationManager
    {
        static ConfigurationManager() {
            Configuration.Init();
        }

        public static IList<Property> properties
        {
            get { return LoadConfiguration().Elment.Properties; }
        }

        public static Property GetProperty(string key)
        {
            foreach (Property p in properties)
            {
                if (p.Name == key)
                {
                    return p;
                }
            }
            return null;
        }

       public static  string GetPropertyValue(string key)
       {
          Property p= GetProperty(key);
           if(p==null)
               return null;
           return p.Value;
       }


        public static ECatalogConfigurationElement LoadConfiguration()
        {
            ECatalogConfigurationElement gsc = System.Configuration.ConfigurationManager.GetSection(XmlConstants.EcatalogConfiguration) as ECatalogConfigurationElement;
            if (gsc != null)
                return gsc;
            else if (File.Exists(Configuration.File))
                using (XmlTextReader reader = new XmlTextReader(Configuration.File))
                    return new ECatalogConfigurationElement(reader);
            else
                throw new ConfigurationErrorsException(string.Format("\"{0}\" cannot found.", Configuration.File));

        }

        public static void AddProperty(Property p)
        {
            if(GetProperty(p.Name)!=null)
                return;
            PropertyManager.Save(p);
        }

        public static void UpdateProperty(Property p)
        {
            PropertyManager.Update(p);
        }

        public static T GetSetting<T>(string key, T defaultValue) {
            return GetSetting(key, defaultValue, false);
        }

        public static T GetSetting<T>(string key)
        {
            return GetSetting(key, default(T), true);
        }

        public static T GetSetting<T>(string key, T defaultValue, bool throwException) {
            string value = GetPropertyValue(key);
            if (string.IsNullOrEmpty(value)) {
                if (throwException)
                    throw new ECatalogConfigurationException("The setting with key \"" + key + "\" must be set in " +
                                      Configuration.File);
                return defaultValue;
            }
            return (T) Convert.ChangeType(value, typeof (T));
        }
    }
}
