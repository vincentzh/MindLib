using System.Configuration;
using System.Xml;

namespace MindHarbor.GenClassLib.Configuration
{
    public class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlTextReader reader = new XmlTextReader(section.OuterXml, XmlNodeType.Document, null);
            return new ECatalogConfigurationElement(reader);
        }

        #endregion
    }
}
