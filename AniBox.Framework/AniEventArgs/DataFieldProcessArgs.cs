using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.AniEventArgs
{
    public class DataFieldProcessArgs : EventArgs
    {
        public FieldEntry Field { get; set; }

        public Object SourceInput { get; set; }
    }
}
