using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Region
{
    [Export(typeof(AniBox.Framework.Region.IAniRegion))]
    public class AniRegion_ColorBackground : AniRegion
    {

        public override string RegionTypeName
        {
            get { return "ColorBackground"; }
        }
    }
}
