using AniBox.Framework.Data;
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

namespace AniBox.Framework.UI
{
    /// <summary>
    /// AddFieldView.xaml 的交互逻辑
    /// </summary>
    public partial class AddFieldView : Window
    {
        public string _fieldsSource = "";
        public AddFieldView()
        {
            this.DataContext = this;

            InitializeComponent();

            lstProperties.PropertyFilterType = typeof(AniBox.Framework.Attributes.AniPropertyAttribute);
        }

        public string FieldsSource
        {
            get
            {
                return _fieldsSource;
            }
            set
            {
                _fieldsSource = value;
                txtFieldsSource.Text = _fieldsSource;
            }
        }

        public IEnumerable<IProcessText> ProcessTypes
        {
            get
            {
                return AniBox.Framework.Region.AniRegion.ProcessTypes;
            }

        }

        private void comboProcessType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lstProperties.SelectedObject = e.AddedItems[0];
        }
    }
}
