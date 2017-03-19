using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    public abstract class ProcessText 
    {
        public enum Link_Type { None = 0, AppendEnd, AsInput, InsertAhead };

        private Link_Type _linkType = Link_Type.None;

        public abstract String Name { get; }

        public abstract String Process();

        public ProcessText Parent { get; set; }

  

        public Link_Type LinkType
        {
            get
            {
                return _linkType;
            }
            set
            {
                _linkType = value;
            }
        }

        public String Output
        {
            get
            {
                return Process();
            }
        }
    }
}
