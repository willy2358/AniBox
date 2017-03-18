using AniBox.Framework.Data.Process;
using Newtonsoft.Json.Linq;
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
using System.Xml;

namespace AniBox.Framework.UI
{
    /// <summary>
    /// AddField2View.xaml 的交互逻辑
    /// </summary>
    public partial class AddField2View : Window
    {
        public String FieldName;
        private Object _fieldInput = null;
        private ProcessText_InToOut _headProcess = new ProcessText_InToOut();
        private ObservableCollection<ProcessText> _processors = new ObservableCollection<ProcessText>();
        public AddField2View()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<ProcessText> Processors
        {
            get
            {
                return _processors;
            }
        }

        public ProcessText HeadProcess
        {
            get
            {
                return _headProcess;
            }
        }

        public Object FieldInput
        {
            get
            {
                return _fieldInput;
            }
            set
            {
                _fieldInput = value;
                _headProcess.Input = GetFieldInputString();
                this.txtInputStr.Text = _headProcess.Input.ToString();
                
            }
        }

        public String GetFieldInputString()
        {
            if (_fieldInput is XmlNode)
            {
                return (_fieldInput as XmlNode).OuterXml;
            }
            else if (_fieldInput is JToken)
            {
                return (_fieldInput as JToken).ToString();
            }

            return _fieldInput.ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.FieldName = txtFieldName.Text;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAddProcessor_Click(object sender, RoutedEventArgs e)
        {
            AddProcessorView dlg = new AddProcessorView();
            dlg.Owner = this;
            if (_processors.Count > 0)
            {
                dlg.ParentProcess = _processors[_processors.Count - 1];
            }
            else
            {
                dlg.ParentProcess = _headProcess;
            }

            if (dlg.ShowDialog().Value)
            {
                _processors.Add(dlg.MyProcess);
            }
        }

        private void btnRemoveProcessor_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgProcessors.SelectedIndex != this.dgProcessors.Items.Count - 1)
            {
                return;
            }

            Object lastProcessor = this.dgProcessors.Items[dgProcessors.Items.Count - 1];
            this.Processors.Remove(lastProcessor as ProcessText);
        }
    }
}
