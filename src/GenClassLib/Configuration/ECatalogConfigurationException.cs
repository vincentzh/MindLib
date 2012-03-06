using System;

namespace MindHarbor.GenClassLib.Configuration
{
    [Serializable]
    public class ECatalogConfigurationException:Exception
    {
        private const string baseMessage = "An exception occurred during configuration.";
        public ECatalogConfigurationException(){}
        public ECatalogConfigurationException(Exception innerException)
            : base(baseMessage, innerException) {}

    	public ECatalogConfigurationException(string message) : base(message) {}

    }
}
