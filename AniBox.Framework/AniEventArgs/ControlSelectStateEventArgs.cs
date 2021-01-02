using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.AniEventArgs
{
    public enum SelectState { Selected = 1, UnSelected =2};
    public class ControlSelectStateEventArgs : EventArgs
    {
        public SelectState OldState { get; internal set; }
        public SelectState NewState { get; internal set; }
    }
}
