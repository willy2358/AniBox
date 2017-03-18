using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    public class ProcessText_InToOut : ProcessText
    {
        public override string Config
        {
            get { return "In as out"; }
        }

        [AniProperty]
        public Object Input
        {
            get;
            set;
        }
        public override string Name
        {
            get { return "In as out"; }
        }

        public override string Process()
        {
            return Input.ToString();
        }
    }
}
