using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AniBox.Controls
{
    [Export(typeof(AniControl))]
    public class StaticImage : WPFAniControl
    {
        private StaticImageControl _imgControl = new StaticImageControl();
        private string _imageFile = "DemoImage";
        public StaticImage()
        {

        }
        public override System.Windows.Controls.ContentControl GetWPFControl()
        {
            return _imgControl;
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
                _imgControl.thisImage.Source = (new ImageSourceConverter()).ConvertFrom(_imageFile) as ImageSource;
            }
        }
    }
}
