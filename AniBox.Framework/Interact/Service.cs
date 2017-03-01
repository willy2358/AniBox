using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Interact
{
    public class Service
    {
        public enum OP_Type { SelectControl, MoveControl };

        public static OP_Type CurrentOperation { get; set; }
    }
}
