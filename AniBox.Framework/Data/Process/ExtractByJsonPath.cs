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

        public override string Extract(String input)
        {
            return JsonHelper.SelectJsonObjsString(input, JsonPath);
        }

      
    }
}
