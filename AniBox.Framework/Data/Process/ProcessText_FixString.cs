using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    [Export(typeof(ProcessText))]
    public class ProcessText_FixString : ProcessText
    {
        public override string Config
        {
            get { return "String:" + FixString; }
        }

        [AniProperty]
        public String FixString
        {
            get;
            set;
        }

        public override string Name
        {
            get { return "Fixed String"; }
        }

        public override string Process()
        {
            return FixString;
        }
    }
}
