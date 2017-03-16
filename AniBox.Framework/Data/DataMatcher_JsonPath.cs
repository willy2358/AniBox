using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    [Export(typeof(DataMatcher))]
    public class DataMatcher_JsonPath : DataMatcher
    {
        /// <summary>
        /// exameples of JsonPath:
        /// $.Manufacturers[?(@.Name == 'Acme Co')]
        /// $..Products[?(@.Price >= 50)].Name
        /// ref:http://www.newtonsoft.com/json/help/html/QueryJsonSelectTokenJsonPath.htm
        /// downloaded local html:Querying JSON with JSONPath.html
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public override Object FilterData(String inData)
        {
            if (string.IsNullOrEmpty(inData) || string.IsNullOrEmpty(Filter))
            {
                return null;
            }

            string json = inData.Trim();
            JToken jToken = null;
            if (json.StartsWith("["))
            {
                jToken = ParseJsonArray(json);
            }
            else if (json.StartsWith("{"))
            {
                jToken = ParseJsonObject(json);
            }

            if (null != jToken)
            {
                List<JToken> items = jToken.SelectTokens(Filter).ToList<JToken>();
                return items;
            }
            return null;
        }

        private JArray ParseJsonArray(string json)
        {
            try
            {
                JArray items = JArray.Parse(json.Trim()); 
                return items;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private JObject ParseJsonObject(string json)
        {
            try
            {
                JObject jObj = JObject.Parse(json);
                return jObj;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public override string MatcherType
        {
            get { return "Json Path"; }
        }
    }
}
