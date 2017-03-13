using AniBox.Framework.App;
using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// SetTextProcessorsView.xaml 的交互逻辑
    /// </summary>
    public partial class SetTextProcessorsView : Window
    {
        public ObservableCollection<ProcessText> _selectedProcessors = new ObservableCollection<ProcessText>();
        public SetTextProcessorsView()
        {
            this.DataContext = this;
            InitializeComponent();

            lstProperties.PropertyFilterType = typeof(AniBox.Framework.Attributes.AniPropertyAttribute);
        }

        public IEnumerable<ProcessText> ProcessTypes
        {
            get
            {
                return IoCTypes.ProcessTypes;
            }

        }

        public ObservableCollection<ProcessText> SelectedProcessors
        {
            get
            {
                return _selectedProcessors;
            }
        }

        private void btnAddProcessor_Click(object sender, RoutedEventArgs e)
        {
            this.btnAddProcessor.Focus();
            SelectedProcessors.Add(this.lstProperties.SelectedObject as ProcessText);
        }

        private void comboProcessTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessText process = Activator.CreateInstance(e.AddedItems[0].GetType()) as ProcessText;
            this.lstProperties.SelectedObject = process;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
