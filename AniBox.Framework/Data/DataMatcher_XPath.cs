using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniBox.Framework.Data
{
     [Export(typeof(DataMatcher))]
    public class DataMatcher_XPath : DataMatcher
    {
        public override Object FilterData(String inData)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            try
            {
                xmlDoc.LoadXml(inData);

                XmlNodeList nodes =  xmlDoc.SelectNodes(Filter);

                return nodes;
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public override string MatcherType
        {
            get { return "XPath"; }
        }
    }
}
