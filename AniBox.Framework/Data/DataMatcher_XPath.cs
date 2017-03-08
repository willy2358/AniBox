using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniBox.Framework.Data
{
    public class DataMatcher_XPath : DataMatcher
    {
        public override String FilterData(String inData)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            try
            {
                xmlDoc.LoadXml(inData);

                XmlNodeList nodes =  xmlDoc.SelectNodes(Filter);

                string txt = "";
                foreach(XmlNode n in nodes)
                {
                    txt += n.OuterXml;
                }

                return txt;
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
