using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework
{
    public interface IAniControl
    {


        string ControlName { get; }

        double X { get; set; }

        double Y { get; set; }

        double ControlWidth { get; set; }

        double ControlHeight { get; set; }
    }
}
