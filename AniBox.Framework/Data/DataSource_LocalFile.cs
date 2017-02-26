using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    [Export(typeof(DataSource))]
    public class DataSource_LocalFile : DataSource
    {
        string _localPath = "";
        public override String QueryData()
        {
            return "";
        }

        public override string SourceTypeName
        {
            get { return "Local File"; }
        }


        public override string SourceSetting
        {
            get
            {
                return _localPath;
            }
            set
            {
                _localPath = value;
            }
        }
    }
}
