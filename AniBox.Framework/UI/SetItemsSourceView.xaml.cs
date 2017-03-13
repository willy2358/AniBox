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
using System.Xml;

namespace AniBox.Framework.UI
{
    /// <summary>
    /// SetItemsSourceView.xaml 的交互逻辑
    /// </summary>
    public partial class SetItemsSourceView : Window
    {
        ObservableCollection<FieldEntry> _fields = new ObservableCollection<FieldEntry>();

        DataMatcher _dataMatcher = null;



        public Object FieldsSourceEntry = "";
        public SetItemsSourceView()
        {
            this.DataContext = this;

            InitializeComponent();
        }

        public IEnumerable<DataSource> DataSourceTypes
        {
            get
            {
                return IoCTypes.DataSourceTypes;
            }
        }

        public String SourceName
        {
            get;
            private set;
        }

        public int UpdateInterval
        {
            get;
            private set;
        }

        public ObservableCollection<FieldEntry> Fields
        {
            get
            {
                return _fields;
            }
        }

        public DataSource CurrentDataSource
        {
            get;
            set;
        }

        public IEnumerable<DataMatcher> DataMatchers
        {
            get
            {
                return IoCTypes.DataMatcherTypes;
            }
        }

        private void comboDSTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataSource ds = this.comboDSTypes.SelectedItem as DataSource;
            if (null != _dataMatcher)
            {
                ds.DataMatcher = _dataMatcher;
            }

            CurrentDataSource = ds;
        }

        private void comboMatchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _dataMatcher = this.comboMatchers.SelectedItem as DataMatcher;
            if (null != CurrentDataSource)
            {
                CurrentDataSource.DataMatcher = _dataMatcher;
            }
        }

        private void addFieldBtn_Click(object sender, RoutedEventArgs e)
        {
            AddFieldView dlg = new AddFieldView();
            dlg.FieldsSource = FieldsSourceEntry;
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult.Value && dlg.ProcessEntries.Count > 0)
            {
                FieldEntry field = new FieldEntry();
                field.SourceInput = FieldsSourceEntry;
                field.FieldName = dlg.FieldName;
                field.Processors = dlg.Processors;
                _fields.Add(field);
            }
        }

        private void delFieldBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void testMatchBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSource ds = this.comboDSTypes.SelectedItem as DataSource;
            ds.SourceSetting = txtSourcePath.Text;
            _dataMatcher.Filter = this.txtFilterString.Text;

            string result = ds.GetUpdateString();
            UpdateFieldsSource(ds.GetUpdateObject());

            this.txtMatchResult.Text = result;
            
        }

        private void UpdateFieldsSource(Object source)
        {
            if (source is XmlNodeList)
            {
                XmlNodeList xmlNodes = source as XmlNodeList;
                if (xmlNodes.Count > 0)
                {
                    this.FieldsSourceEntry = xmlNodes[0];
                }
            }
            else
            {
                this.FieldsSourceEntry = "";
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.SourceName = this.txtSourceName.Text;

            int interval = 10;
            if (int.TryParse(this.txtUpdateInterval.Text, out interval))
            {
                UpdateInterval = interval;
            }

            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSetSourcePath_Click(object sender, RoutedEventArgs e)
        {
            SetTextProcessorsView dlg = new SetTextProcessorsView();
            dlg.Owner = this;
            if (dlg.ShowDialog().Value)
            {
                List<ProcessText> proces = dlg.SelectedProcessors.ToList<ProcessText>();
                if (proces.Count > 0)
                {
                    this.txtSourcePath.Text = proces.Last<ProcessText>().Output.ToString();
                }
            }

        }
    }
}
