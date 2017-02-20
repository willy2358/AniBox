using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.DataSource
{
    public abstract class DataSource : IDataSource
    {
        [AniProperty]
        public string DataPath
        {
            get;
            set;
        }

        public Object GetUpdate()
        {
            Object data = this.QueryData();

            if (null != DataMatcher)
            {
                data = DataMatcher.FilterData(data);
            }

            return data;
        }

        public abstract object QueryData();

        public IMatchData DataMatcher
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
