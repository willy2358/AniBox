using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public abstract class DataMatcher : IMatchData
    {
        public abstract object FilterData(object inData);

        public string Filter { get; set; }
    }
}
