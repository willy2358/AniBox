using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework
{
    public interface IAniControl
    {
        string ControlTypeName { get; }

        string ControlName { get; set; }

        double X { get; set; }

        double Y { get; set; }

        double ControlWidth { get; set; }

        double ControlHeight { get; set; }
    }
}
