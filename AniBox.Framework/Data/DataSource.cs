using AniBox.Framework.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniBox.Framework.Data
{
    public abstract class DataSource : IDataSource
    {
        public abstract string SourceSetting{ get; set; }

        public abstract String SourceTypeName { get; }

        public String Encoding { get; set; }

        public Object GetUpdateObject()
        {
            string text = this.QueryData();
            if (null != DataMatcher)
            {
                Object data = DataMatcher.FilterData(text);
                return data;
            }
            else
            {
                return text;
            }
        }

        public String GetUpdateString()
        {
            Object obj = GetUpdateObject();
            if (obj is string)
            {
                return obj.ToString();
            }
            else if (obj is XmlNodeList)
            {
                string txt = "";
                foreach (XmlNode n in obj as XmlNodeList)
                {
                    txt += n.OuterXml;
                }
                return txt;
            }
            else if (obj is List<JToken>)
            {
                string jsons = "";
                List<JToken> tokens = obj as List<JToken>;
                for(int i = 0; i < tokens.Count; i++)
                {
                    jsons += tokens[i].ToString() + "\r\n";
                }
                return jsons;
            }
            else if (null != obj)
            {
                return obj.ToString();
            }

            return "";
        }

        public abstract String QueryData();

        public IMatchData DataMatcher { get; set; }

        protected System.Text.Encoding GetEncoding()
        {
            System.Text.Encoding encode = System.Text.Encoding.UTF8;
            try
            {
                encode = System.Text.Encoding.GetEncoding(Encoding);
            }
            catch (Exception ex)
            {

            }

            return encode;
        }


    }
}
