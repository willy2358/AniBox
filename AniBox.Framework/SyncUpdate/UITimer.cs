using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.SyncUpdate
{
    public class UITimer
    {
        private List<AniControl> _updateControls = new List<AniControl>();
        public String Name { get; set; }

        
        public void AddUptateControl(AniControl control)
        {
            if (null != control)
            {
                _updateControls.Add(control);
            }
            
        }
    }
}
