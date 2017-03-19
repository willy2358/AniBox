using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Utility
{

    class JsonHelper
    {
        /// <summary>
        /// exameples of JsonPath:
        /// $.Manufacturers[?(@.Name == 'Acme Co')]
        /// $..Products[?(@.Price >= 50)].Name
        /// ref:http://www.newtonsoft.com/json/help/html/QueryJsonSelectTokenJsonPath.htm
        /// downloaded local html:Querying JSON with JSONPath.html
        /// </summary>
        /// <param name="json"></param>
        /// 
        /// <returns></returns>
        public static string SelectJsonObjsString(string json, string jsonPath)
        {
            List<JToken> jsonObj = SelectJsonObjs(json, jsonPath);
            if (null != jsonObj)
            {
                return StringHelper.ToString(jsonObj);
            }

            return null;
        }

        public static List<JToken> SelectJsonObjs(String json, string jsonPath)
        {
            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(jsonPath))
            {
                return null;
            }

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
                List<JToken> items = jToken.SelectTokens(jsonPath).ToList<JToken>();
                return items;
            }
            return null;
        }

        private static JArray ParseJsonArray(string json)
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

        private static JObject ParseJsonObject(string json)
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
