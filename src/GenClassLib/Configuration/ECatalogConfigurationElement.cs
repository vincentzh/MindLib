#region

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

#endregion

namespace MindHarbor.GenClassLib.Configuration
{
	public class ECatalogConfigurationElement
	{
		private readonly string CfgSchemaResource = typeof (ECatalogConfigurationElement).Namespace+
		                                            ".ecatalog-configuration.xsd";

		private readonly XmlSchema configSchema;
		private SettingElement element;

		public ECatalogConfigurationElement()
		{
			configSchema = ReadXmlSchemaFromEmbeddedResource(CfgSchemaResource);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref = "SettingElement" /> class.
		/// </summary>
		/// <param name = "configurationReader">The XML reader to parse.</param>
		/// <remarks>
		///     The nhs-configuration.xsd is applied to the XML.
		/// </remarks>
		/// <exception cref = "ECatalogConfigurationException">When nhs-configuration.xsd can't be applied.</exception>
		public ECatalogConfigurationElement(XmlReader configurationReader) : this()
		{
			XPathNavigator nav;
			try
			{
				nav = new XPathDocument(XmlReader.Create(configurationReader, GetSettings())).CreateNavigator();
			}
			catch (ECatalogConfigurationException)
			{
				throw;
			}
			catch (Exception e)
			{
				// Encapsule and reThrow
				throw new ECatalogConfigurationException(e);
			}

			Parse(nav);
		}

		public SettingElement Elment
		{
			get { return element; }
		}

		private XmlReaderSettings GetSettings()
		{
			XmlReaderSettings xmlrs = CreateConfigReaderSettings();
			return xmlrs;
		}

		private XmlReaderSettings CreateConfigReaderSettings()
		{
			XmlReaderSettings result = CreateXmlReaderSettings(configSchema);
			result.IgnoreComments = true;
			return result;
		}

		private static XmlSchema ReadXmlSchemaFromEmbeddedResource(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();

			using (Stream resourceStream = executingAssembly.GetManifestResourceStream(resourceName))
				return XmlSchema.Read(resourceStream, null);
		}

		private static XmlReaderSettings CreateXmlReaderSettings(XmlSchema xmlSchema)
		{
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.ValidationType = ValidationType.Schema;
			settings.Schemas.Add(xmlSchema);
			return settings;
		}

		private void Parse(XPathNavigator navigator)
		{
			XPathNavigator pNav = navigator.SelectSingleNode(Configuration.ConfigExpression);
			element = new SettingElement(pNav);
		}
	}
}