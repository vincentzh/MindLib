using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace MindHarbor.GenClassLib.Test.DataTests
{
    [TestFixture]
    public class CountriesDataTest
    {
        [Test]
        public void ReadCountryNameAndCodeFromYahooAPI()
        {
            WebClient webClient = new WebClient();
            var stream= webClient.OpenRead(
                "http://where.yahooapis.com/v1/countries?select=long&appid=dj0yJmk9SWN0RnVxTzhESlhQJmQ9WVdrOVZuVnliRWRaTXpZbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD0xNA-");
            XmlDocument xd = new XmlDocument();

            
            xd.Load(stream);
         
            
            var nodes = xd.DocumentElement.ChildNodes;
            Dictionary<string,string> dic=new Dictionary<string, string>();
            for (int i = 0; i < nodes.Count; i++)
            {
                dic.Add(nodes[i].ChildNodes[2].InnerXml, nodes[i].ChildNodes[3].Attributes["code"].Value);
                
            }
            var key= dic.Keys.OrderBy(x => x);
            Console.WriteLine("Country Count:"+dic.Count);
            foreach (var countryName in key)
            {
                Console.WriteLine(dic[countryName] + " " + countryName);
                   // Console.WriteLine("dt.Rows.Add(CreateRow(\"{0}\", \"{1}\", dt));",dic[countryName],countryName);
            }

        }
    }
}
