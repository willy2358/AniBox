using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public abstract class DataSource : IDataSource
    {
        public abstract string SourceSetting{ get; set; }

        public abstract String SourceTypeName { get; }

        public String GetUpdate()
        {
            string data = this.QueryData();

            if (null != DataMatcher)
            {
                data = DataMatcher.FilterData(data);
            }

            return data;
        }

        public abstract String QueryData();

        public IMatchData DataMatcher { get; set; }

    }
}
