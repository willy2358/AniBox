using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public class DataMatcher_XPath : DataMatcher
    {
        public override String FilterData(String inData)
        {
            return inData;
        }

        public override string MatcherType
        {
            get { return "XPath"; }
        }
    }
}
