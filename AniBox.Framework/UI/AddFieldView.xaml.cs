using AniBox.Framework.App;
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
using System.Xml;

namespace AniBox.Framework.UI
{
    /// <summary>
    /// AddFieldView.xaml 的交互逻辑
    /// </summary>
    public partial class AddFieldView : Window
    {
        public Object _fieldsSource = null;

        public ObservableCollection<ProcessEntry> _processEntries = new ObservableCollection<ProcessEntry>();

        public List<IProcessText> _processors = new List<IProcessText>();

        public String FieldName;
        public AddFieldView()
        {
            this.DataContext = this;

            InitializeComponent();

            lstProperties.PropertyFilterType = typeof(AniBox.Framework.Attributes.AniPropertyAttribute);
        }

        public ObservableCollection<ProcessEntry> ProcessEntries
        {
            get
            {
                return _processEntries;
            }
        }

        public List<IProcessText> Processors
        {
            get
            {
                return _processors;
            }
        }

        public Object FieldsSource
        {
            get
            {
                return _fieldsSource;
            }
            set
            {
                _fieldsSource = value;
                txtFieldsSource.Text = GetFieldSourceEntryString(_fieldsSource);
            }
        }

        public IEnumerable<ProcessText> ProcessTypes
        {
            get
            {
                return IoCTypes.ProcessTypes;
            }

        }

        public string GetFieldSourceEntryString(Object entry)
        {
            if (entry is XmlNode)
            {
                return (entry as XmlNode).OuterXml;
            }
            else
            {
                return entry.ToString();
            }
        }

        private void comboProcessType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IProcessText process = Activator.CreateInstance(e.AddedItems[0].GetType()) as IProcessText;
            this.lstProperties.SelectedObject = process;
        }

        private void btnTestProcessor_Click(object sender, RoutedEventArgs e)
        {
            IProcessText process = this.lstProperties.SelectedObject as IProcessText;
            this.txtProcessResult.Text = process.Process();
        }

        private void btnAddProcessor_Click(object sender, RoutedEventArgs e)
        {
            ProcessText process = this.lstProperties.SelectedObject as ProcessText;

            ProcessEntry entry = new ProcessEntry();
            entry.TypeName = process.Name;
            entry.Setting = process.Config;
            entry.Output = process.Process();

            _processors.Add(process);
            _processEntries.Add(entry);
        }

        private Object GetLastProcessOutput()
        {
            if (_processEntries.Count < 1)
            {
                return FieldsSource;
            }
            else
            {
                return _processEntries[0].Output;
            }
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

    }
}
