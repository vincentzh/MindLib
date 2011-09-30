using System.Xml;
using System.Xml.XPath;

namespace MindHarbor.GenClassLib.Configuration
{
    public class PropertyManager
    {
        public static void Save(Property p)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(Configuration.File);
            XPathNavigator navigator = xd.CreateNavigator();
            XPathNavigator pNav = navigator.SelectSingleNode(Configuration.ConfigExpression);
            string editLevel = string.Empty;
            if (p.EditLevel != 0)
                editLevel = " " + XmlConstants.Property_EditLevel_Attribute + string.Format("='{0}'", p.EditLevel);
            string optionValues = string.Empty;
            if (!string.IsNullOrEmpty(p.OptionValues))
                optionValues = " " + XmlConstants.Property_OptionValues_Attribute + string.Format("='{0}'", p.OptionValues);
            pNav.AppendChild(string.Format("<{4} {5}='{0}'{1}{2}>{3}</{4}>", p.Name, editLevel, optionValues, p.Value, XmlConstants.Property, XmlConstants.Property_Name_Attribute));
            xd.Save(Configuration.File);
        }

        public static void Update(Property p)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(Configuration.File);
            XmlNode rootNode = xd.GetElementsByTagName(XmlConstants.EcatalogConfiguration)[0];
            XmlNode settingNode = GetElementBy(rootNode, XmlConstants.Setting);
            foreach (XmlNode xn in settingNode.ChildNodes)
            {
                if (xn.Name != XmlConstants.Property)
                    continue;
                if (xn.Attributes[XmlConstants.Property_Name_Attribute].Value == p.Name)
                {
                    UpdateEditLevel(p,xn,xd);
                    UpdateOptionValues(p,xn,xd);
                    xn.InnerXml = p.Value;
                }
            }
            xd.Save(Configuration.File);
        }

        private static void UpdateEditLevel(Property p,XmlNode xn,XmlDocument xd)
        {
            if (p.EditLevel != 0)
            {
                if (xn.Attributes[XmlConstants.Property_EditLevel_Attribute] != null)
                {
                    xn.Attributes[XmlConstants.Property_EditLevel_Attribute].Value = p.EditLevel.ToString();
                }
                else
                {
                    XmlAttribute atrr = xd.CreateAttribute(XmlConstants.Property_EditLevel_Attribute);
                    atrr.Value = p.EditLevel.ToString();
                    xn.Attributes.Append(atrr);
                }
            }
            else
            {
                xn.Attributes.Remove(xn.Attributes[XmlConstants.Property_EditLevel_Attribute]);
            }
        }

        private static void UpdateOptionValues(Property p, XmlNode xn, XmlDocument xd)
        {
            if (!string.IsNullOrEmpty(p.OptionValues))
            {
                if (xn.Attributes[XmlConstants.Property_OptionValues_Attribute] != null)
                {
                    xn.Attributes[XmlConstants.Property_OptionValues_Attribute].Value = p.OptionValues;
                }
                else
                {
                    XmlAttribute atrr = xd.CreateAttribute(XmlConstants.Property_OptionValues_Attribute);
                    atrr.Value = p.OptionValues;
                    xn.Attributes.Append(atrr);
                }
            }
            else
            {
                xn.Attributes.Remove(xn.Attributes[XmlConstants.Property_OptionValues_Attribute]);
            }
        }

        private static XmlNode GetElementBy(XmlNode rootNode, string elementName)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name == elementName)
                {
                    return node;
                }
            }
            throw new ECatalogConfigurationException("The element must be set in " +Configuration.File);
        }
    }
}
