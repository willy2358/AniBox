using AniBox.Framework.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniBox.Framework.Utility;

namespace AniBox.Framework.Data.Process
{
     [Export(typeof(ProcessText))]
    class ExtractByJsonPath : ExtractText
    {
        //public override string Config
        //{
        //    get { return "Json path:" + Path; }
        //}

        [AniProperty]
        public String JsonPath { get; set; }
        public override string Name
        {
            get { return "Json Path"; }
        }

        //public override string Process()
        //{
        //    throw new NotImplementedException();
        //}

        //public override string Extract(string input)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// exameples of JsonPath:
        /// $.Manufacturers[?(@.Name == 'Acme Co')]
        /// $..Products[?(@.Price >= 50)].Name
        /// ref:http://www.newtonsoft.com/json/help/html/QueryJsonSelectTokenJsonPath.htm
        /// downloaded local html:Querying JSON with JSONPath.html
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public override string Extract(String input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(JsonPath))
            {
                return null;
            }

            string json = input.Trim();
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
                List<JToken> items = jToken.SelectTokens(JsonPath).ToList<JToken>();
                return StringHelper.ToString(items);
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
