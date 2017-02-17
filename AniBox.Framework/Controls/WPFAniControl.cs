using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AniBox.Framework
{
    public abstract class WPFAniControl : UserControl,IAniControl
    {
        public abstract string ControlTypeName
        {
            get;
        }

        [AniProperty]
        public string ControlName
        {
            get;
            set;
        }

        [AniProperty]
        public double X
        {
            get;
            set;
        }

        [AniProperty]
        public double Y
        {
            get;
            set;
        }

        [AniProperty]
        public double ControlWidth
        {
            get;
            set;
        }

        [AniProperty]
        public double ControlHeight
        {
            get;
            set;
        }



    }
}
