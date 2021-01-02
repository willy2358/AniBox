using AniBox.Framework.App;
using AniBox.Framework.Data.Process;
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

namespace AniBox.Framework.Views
{
    /// <summary>
    /// AddProcessorView.xaml 的交互逻辑
    /// </summary>
    public partial class AddFieldProcessorView : Window
    {
        public AddFieldProcessorView()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public ProcessText ParentProcess
        {
            set
            {
                this.processorCtrl.ParentProcess = value;
                txtInputString.Text = value.Output.ToString();
            }
        }

        public ProcessText MyProcess
        {
            get
            {
                return this.processorCtrl.MyProcess;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
       
    }
}
