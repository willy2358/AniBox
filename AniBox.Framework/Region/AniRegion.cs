using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Region
{
    public abstract class AniRegion : IAniRegion
    {
        public abstract string RegionTypeName{ get;}

        public double XPos
        {
            get;
            set;
        }

        public double YPos
        {
            get;
            set;
        }

        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }
    }
}
