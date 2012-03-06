using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MindHarbor.GenClassLib.Configuration
{
    public class SettingElement
    {
        private readonly IList<Property> properties = new List<Property>();
        public SettingElement() {}

        public SettingElement(XPathNavigator navigator)
        {
              Parse(navigator);
        }


        protected internal IList<Property> Properties
        {
            get { return properties; }
        }

        protected  void Parse(XPathNavigator navigator)
        {
            XPathNodeIterator xpni = navigator.SelectChildren(XmlConstants.Property, XmlConstants.SchemaXMLNS);
            while (xpni.MoveNext())
            {
                string propName = xpni.Current.GetAttribute(XmlConstants.Property_Name_Attribute, string.Empty);
                int propEditLevel = 0;
                Int32.TryParse(xpni.Current.GetAttribute(XmlConstants.Property_EditLevel_Attribute, string.Empty), out propEditLevel);
                string propOptionValues =
                    xpni.Current.GetAttribute(XmlConstants.Property_OptionValues_Attribute, string.Empty);
                string propValue = xpni.Current.Value;
                if (!string.IsNullOrEmpty(propName) && !string.IsNullOrEmpty(propValue))
                {
                    Property p = new Property(propName, propValue, propEditLevel, propOptionValues);
                    properties.Add(p);
                }
                    
                   
            }
        }
    }
}
