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
        public override Object FilterData(String inData)
        {
            if (string.IsNullOrEmpty(inData) || string.IsNullOrEmpty(Filter))
            {
                return null;
            }

            string[] parts = Filter.Split('.');
            for(int i = 0; i < parts.Length; i++)
            {
                string p = parts[i];
                if (p == "[]" || p == "[ ]")
                {

                }
                else if (p == "{}" || p == "{ }")
                {

                }
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
