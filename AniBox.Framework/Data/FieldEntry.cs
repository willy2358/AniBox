using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public class FieldEntry
    {
        public EventHandler ResultListener;
        public String FieldName { get; set; }

        public List<ProcessEntry> Processors { get; set; }


        public String Result
        {
            get
            {
                if (null != Processors && Processors.Count > 0)
                {
                    return Processors[Processors.Count - 1].Output;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
