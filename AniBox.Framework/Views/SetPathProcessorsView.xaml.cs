using AniBox.Framework.App;
using AniBox.Framework.Data;
using AniBox.Framework.Data.Process;
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

namespace AniBox.Framework.Views
{
    /// <summary>
    /// SetTextProcessorsView.xaml 的交互逻辑
    /// </summary>
    public partial class SetPathProcessorsView : Window
    {
        public ObservableCollection<ProcessText> _selectedProcessors = new ObservableCollection<ProcessText>();

        public SetPathProcessorsView()
        {
            this.DataContext = this;
            InitializeComponent();
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
            if (SelectedProcessors.Count > 0)
            {
                this.processorCtrl.ParentProcess = SelectedProcessors[SelectedProcessors.Count - 1];
            }

            if (null != this.processorCtrl.MyProcess)
            {
                SelectedProcessors.Add(this.processorCtrl.MyProcess);
                this.processorCtrl.ParentProcess = this.processorCtrl.MyProcess;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnRemoveProcess_Click(object sender, RoutedEventArgs e)
        {
            if ( this.dgProcessors.SelectedIndex != this.dgProcessors.Items.Count - 1)
            {
                return;
            }

            Object lastProcessor = this.dgProcessors.Items[dgProcessors.Items.Count - 1];
            this.SelectedProcessors.Remove(lastProcessor as ProcessText);
        }
    }
}
