using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
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
    /// BindDataSupplierView.xaml 的交互逻辑
    /// </summary>
    public partial class BindDataSupplierView : Window
    {
        public FieldEntry SelectedField { get; private set; }

        public BindDataSupplierView()
        {
            InitializeComponent();
        }

        private List<FieldEntry> _fields;
        public List<FieldEntry> Fields
        {
            set
            {
                _fields = value;
                lstFields.ItemsSource = _fields;
            }
        }

        private void lstFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedField = e.AddedItems[0] as FieldEntry;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
