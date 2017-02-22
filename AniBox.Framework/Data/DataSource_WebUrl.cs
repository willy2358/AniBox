using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    [Export(typeof(DataSource))]
    public class DataSource_WebUrl : DataSource
    {
        private string _address = "http://";
        public override String QueryData()
        {
            //throw new NotImplementedException();
            return "";
        }

        public override string SourceTypeName
        {
            get { return "Web Url"; }
        }


        public override string SourceSetting
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
    }
}
