using AniBox.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AniBox.Controls
{
    /// <summary>
    /// StaticImage.xaml 的交互逻辑
    /// </summary>
    /// 
    [Export(typeof(AniBox.Framework.IAniControl))]
    public partial class StaticImage : WPFAniControl
    {
        private string _imageFile = "DemoImage";
        public StaticImage()
        {
            InitializeComponent();
        }


        public override string ControlName
        {
            get { return "StaticImage"; }
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
                myImage.Source = (new ImageSourceConverter()).ConvertFrom(_imageFile) as ImageSource;
            }
        }
    }
}
