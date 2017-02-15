using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Region
{
    public interface IAniRegion
    {
        string RegionTypeName { get; }

        double XPos { get; set; }

        double YPos { get; set; }

        double RegionWidth { get; set; }

        double RegionHeight { get; set; }
    }
}
