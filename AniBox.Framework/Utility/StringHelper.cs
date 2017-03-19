using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniBox.Framework.Utility
{
    public class StringHelper
    {
        public static string ToString(XmlNode xmlNode)
        {
            if (null == xmlNode)
            {
                return null;
            }
            return xmlNode.OuterXml;
        }

        public static String ToString(List<JToken> tokens)
        {
            if (null == tokens)
            {
                return null;
            }

            string jsons = "";
            for (int i = 0; i < tokens.Count; i++)
            {
                jsons += tokens[i].ToString() + "\r\n";
            }
            return jsons;
        }

        //public String ToAniString(this List<JToken> tokens)
        //{

        //}

        public static String ToString(XmlNodeList nodes)
        {
            if (null == nodes)
            {
                return null;
            }
            string txt = "";
            foreach (XmlNode n in nodes)
            {
                txt += n.OuterXml;
            }
            return txt;
        }
    }
}
