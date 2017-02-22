using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public interface IDataSource
    {
        string SourceSetting { get; set; }

        String QueryData();

        IMatchData DataMatcher { get; set; }
    }
}
