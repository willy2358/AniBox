using AniBox.Controls;
using AniBox.Framework;
using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StaticImage
{
      [Export(typeof(AniBox.Framework.Controls.AniControl))]
    class StaticImage : WPFAniControl
    {
        UserControl1 _control = new UserControl1();
        private string _imageFile = "DemoImage";
        public override System.Windows.Controls.ContentControl GetWPFControl()
        {
            return _control;
        }

        public override string ControlTypeName
        {
            get { return "WPFStaticImage"; }
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
                _control.myImage.Source = (new ImageSourceConverter()).ConvertFrom(_imageFile) as ImageSource;
            }
        }
    }
}
