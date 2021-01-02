using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    public abstract class ExtractText : ProcessText
    {
        public abstract String Extract(string input);

        public String Input { get; set; }

        public override string Process()
        {
            string input = "";
            if (null != Parent && LinkType == Link_Type.AsInput)
            {
                input = Parent.Output;
            }
            else
            {
                input = this.Input;
            }

            return Extract(input);
        } 
    }
}
