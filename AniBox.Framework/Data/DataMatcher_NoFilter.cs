using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
     [Export(typeof(DataMatcher))]
    class DataMatcher_NoFilter : DataMatcher
    {
        public override Object FilterData(string inData)
        {
            return inData;
        }

        public override string MatcherType
        {
            get { return "Pass Through"; }
        }
    }
}
