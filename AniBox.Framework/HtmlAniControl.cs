using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework
{
    public abstract class HtmlAniControl : IAniControl
    {
        public abstract string GetHtmlText();
        public abstract string GetHtmlFile();

        public abstract string ControlName
        {
            get;
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
