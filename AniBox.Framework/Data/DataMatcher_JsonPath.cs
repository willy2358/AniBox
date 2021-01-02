using AniBox.Framework.Utility;
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
            return JsonHelper.SelectJsonObjs(inData, Filter);
        }

        public override string MatcherType
        {
            get { return "Json Path"; }
        }
    }
}
