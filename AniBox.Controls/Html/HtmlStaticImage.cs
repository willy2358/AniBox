using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls
{
    [Export(typeof(AniControl))]
    class HtmlStaticImage : HtmlAniControl
    {
        private string _imageFile;
        public override string ControlTypeName
        {
            get { return "HtmlStaticImage"; }
        }
        

        [AniProperty]
        public string ImageFile
        {
            get
            {
                return _imageFile;
            }
            set
            {
                _imageFile = value;
                string imgSrc = _imageFile.Replace('\\', '/');
                UpdateDomElementAttribute("myImg", "src", imgSrc);
            }
        }

        protected override string HtmlResName
        {
            get { return "AniBox.Controls.res." + "StaticImage.html"; }
        }

        protected override List<HtmlResItem> HtmlReferedResources
        {
            get { return null; }
        }
    }
}
