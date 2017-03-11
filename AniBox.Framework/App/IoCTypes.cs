using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.App
{
    public class IoCTypes
    {
        public static IEnumerable<ProcessText> ProcessTypes { get; set; }

        public static IEnumerable<DataSource> DataSourceTypes { get; set; }

        public static IEnumerable<DataMatcher> DataMatcherTypes { get; set; }
    }
}
