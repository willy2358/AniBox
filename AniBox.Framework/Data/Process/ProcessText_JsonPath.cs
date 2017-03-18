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
    class ProcessText_JsonPath : ProcessText
    {
        public override string Config
        {
            get { return "Json path:" + Path; }
        }

        [AniProperty]
        public String Path { get; set; }
        public override string Name
        {
            get { return "Json Path"; }
        }

        public override string Process()
        {
            throw new NotImplementedException();
        }
    }
}
