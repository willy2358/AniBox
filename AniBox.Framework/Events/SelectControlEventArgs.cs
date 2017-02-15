using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Events
{
    public class SelectControlEventArgs :EventArgs
    {
        public SelectControlEventArgs(IAniControl control)
        {
            SelectedControl = control;
        }

        public IAniControl SelectedControl
        {
            get;
            private set;
        }
    }
}
