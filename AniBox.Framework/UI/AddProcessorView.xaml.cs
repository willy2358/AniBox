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

namespace AniBox.Framework.UI
{
    /// <summary>
    /// AddProcessorView.xaml 的交互逻辑
    /// </summary>
    public partial class AddProcessorView : Window
    {
        private ProcessText.Link_Type _linkType = ProcessText.Link_Type.None;

        //private Object _input;
        private ProcessText _parentProcess = null;
        private ProcessText _myProcess = null;
        public AddProcessorView()
        {
            this.DataContext = this;
            InitializeComponent();

            this.lstProperties.PropertyFilterType = typeof(AniBox.Framework.Attributes.AniPropertyAttribute);
        }

        public ProcessText ParentProcess
        {
            get
            {
                return _parentProcess;
            }
            set
            {
                _parentProcess = value;
                txtInputString.Text = _parentProcess.Output.ToString();
            }
        }

        public ProcessText MyProcess
        {
            get
            {
                return _myProcess;
            }
            private set
            {
                _myProcess = value;
            }
        }


        public IEnumerable<ProcessText> ProcessTypes
        {
            get
            {
                return IoCTypes.ProcessTypes;
            }

        }

        private void lstProperties_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateProcessResult();
        }

        private void lstProperties_PropertyEditingFinished(object sender, RoutedEventArgs e)
        {
            UpdateProcessResult();
        }

        private void lstProperties_PropertyValueChanged(object sender, System.Windows.Controls.WpfPropertyGrid.PropertyValueChangedEventArgs e)
        {
            UpdateProcessResult();
        }

        private void comboProcessTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessText process = Activator.CreateInstance(e.AddedItems[0].GetType()) as ProcessText;
            this.lstProperties.SelectedObject = process;

            UpdateProcessResult();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void radioLinkNone_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.None;
            UpdateProcessResult();
        }

        private void radioLinkInsert_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.InsertAhead;
            UpdateProcessResult();
        }

        private void radioLinkAsInput_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.AsInput;
            UpdateProcessResult();

        }

        private void radioLinkAppend_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.AppendEnd;
            UpdateProcessResult();
        }

        private void UpdateProcessResult()
        {
            ProcessText newProcess = this.lstProperties.SelectedObject as ProcessText;
            if (null == newProcess)
            {
                return;
            }
            newProcess.LinkType = _linkType;
            newProcess.Parent = ParentProcess;
            MyProcess = newProcess;

            this.txtResult.Text = newProcess.Output.ToString();
        }
    }
}
