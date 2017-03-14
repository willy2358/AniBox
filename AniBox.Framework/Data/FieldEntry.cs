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
            Object input = source;
            Object output = input;
            for(int i = 0; i < Processors.Count; i++)
            {
                IProcessText proc = Processors[i];
                output = proc.Process();

                input = output;
            }

            return output.ToString();
        }

        public List<IProcessText> Processors { get; set; }


        public String Result
        {
            get
            {
                return ProcessSource(SourceInput);
            }
        }
    }
}
