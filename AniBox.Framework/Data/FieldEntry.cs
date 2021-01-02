using AniBox.Framework.AniEventArgs;
using AniBox.Framework.Data.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public class FieldEntry
    {
        public EventHandler<DataFieldProcessArgs> ResultListener;
        public String FieldName { get; set; }

        public Object SourceInput { get; set; }

        public String ProcessSource(Object source)
        {
            GenerateStrFromObj head = Processors[0] as GenerateStrFromObj;
            if (null == head)
            {
                return "";
            }
            else
            {
                return Processors[Processors.Count - 1].Output.ToString();
            }
            //Object input = source;

            //Object output = input;
            //for(int i = 0; i < Processors.Count; i++)
            //{
            //    ProcessText proc = Processors[i];
            //    output = proc.Process();

            //    input = output;
            //}

            //return output.ToString();
        }

        public List<ProcessText> Processors { get; set; }


        public String Result
        {
            get
            {
                return ProcessSource(SourceInput);
            }
        }
    }
}
