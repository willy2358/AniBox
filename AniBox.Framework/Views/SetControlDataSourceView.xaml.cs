using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AniBox.Framework.Views
{
    /// <summary>
    /// SetDataSourceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetControlDataSourceView : Window
    {
        public SetControlDataSourceView()
        {
            this.DataContext = this;

            InitializeComponent();
        }

        public DataSource CurrentDataSource
        {
            get
            {
                return this.dsControl.CurrentDataSource;
            }
            
        }

        public String SourceName
        {
            get
            {
                return dsControl.SourceName;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SourceName))
            {
                MessageBox.Show("Source name can not be empty");
                return;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
