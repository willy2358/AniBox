using AniBox.Framework.Attributes;
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

namespace AniBox.Framework.Region
{
    /// <summary>
    /// AniRegion_ImageBackground.xaml 的交互逻辑
    /// </summary>
    /// 
     [Export(typeof(IAniRegion))]
    public partial class AniRegion_ImageBackground : AniRegion
    {
        public string _imagePath;
        public AniRegion_ImageBackground()
        {
            InitializeComponent();
        }

        public override string RegionTypeName
        {
            get
            {
                return "AniRegion_ImageBackground";
            }
        }

        protected override Canvas MyCanvas
        {
            get { return myCanvas; }
        }

        [AniProperty]
        public string BackgroundImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                try
                {
                    this.backImg.ImageSource = (new ImageSourceConverter()).ConvertFrom(_imagePath) as ImageSource;
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
