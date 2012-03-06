using System;
using System.Xml;
using System.Xml.XPath;
using MindHarbor.GenClassLib.MiscUtil;

namespace MindHarbor.GenClassLib.Configuration{
    public class Configuration{
        public static XPathExpression ConfigExpression;
        public static string File = GetFile();

        public static void Init(){
            NameTable nt = new NameTable();
            XmlNamespaceManager NsMgr = new XmlNamespaceManager(nt);
            NsMgr.AddNamespace(XmlConstants.Namespace, XmlConstants.SchemaXMLNS);
            ConfigExpression = XPathExpression.Compile("//" + XmlConstants.Namespace + ":" + XmlConstants.Setting, NsMgr);
        }


        private static string GetFile(){
            string configFile = ConfigHelperBase.GetSetting("ConfigFile", XmlConstants.DefaultConfigFile);
            return configFile.Replace("~", AppDomain.CurrentDomain.BaseDirectory).Replace("/", "\\");
        }
    }
}