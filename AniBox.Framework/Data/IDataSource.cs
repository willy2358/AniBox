using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public interface IDataSource
    {
        string DataPath { get; set; }

        Object QueryData();

        IMatchData DataMatcher { get; set; }
    }
}
