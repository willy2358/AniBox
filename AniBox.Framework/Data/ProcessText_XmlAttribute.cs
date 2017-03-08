using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    class ProcessText_XmlAttribute : IProcessText
    {
        public string Name
        {
            get { return "Extract Xml Tag Attribute"; }
        }
    }
}
