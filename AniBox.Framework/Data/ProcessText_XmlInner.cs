using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    [Export(typeof(IProcessText))]
    class ProcessText_XmlInner : IProcessText
    {
        public string Name
        {
            get { return "Extract Xml Tag Inner Text"; }
        }

        [AniProperty]
        public string TagName
        {
            get;
            set;
        }
    }
}
