using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public class DataMatcher_JsonPath : DataMatcher
    {
        public override String FilterData(String inData)
        {
            throw new NotImplementedException();
        }

        public override string MatcherType
        {
            get { return "Json Path"; }
        }
    }
}
