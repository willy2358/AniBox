using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public abstract class DataMatcher : IMatchData
    {
        public abstract Object FilterData(String inData);

        public string Filter { get; set; }

        public abstract String MatcherType { get; }
    }
}
