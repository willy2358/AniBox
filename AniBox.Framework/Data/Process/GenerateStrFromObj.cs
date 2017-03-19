using AniBox.Framework.Attributes;
using AniBox.Framework.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniBox.Framework.Data.Process
{
    public class GenerateStrFromObj : GenerateText
    {
        //public override string Config
        //{
        //    get { return "In as out"; }
        //}

        public Object SourceObj
        {
            get;
            set;
        }
        public override string Name
        {
            get { return "In as out"; }
        }

        public override String Process()
        {
            if (SourceObj is XmlNode)
            {
                return (SourceObj as XmlNode).OuterXml;
            }
            else if (SourceObj is JToken)
            {
                return (SourceObj as JToken).ToString();
            }
            if (SourceObj is XmlNodeList)
            {

            }

            return SourceObj.ToString();
        }

        public override string Generate()
        {
            if (SourceObj is XmlNode)
            {
                return StringHelper.ToString(SourceObj as XmlNode);
            }
            else if (SourceObj is JToken)
            {
                return (SourceObj as JToken).ToString();
            }
            if (SourceObj is XmlNodeList)
            {
                return StringHelper.ToString(SourceObj as XmlNodeList);
            }

            return SourceObj.ToString();

        }
    }
}
