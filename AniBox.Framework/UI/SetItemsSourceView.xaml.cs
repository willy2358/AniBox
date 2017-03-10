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
        ObservableCollection<DataSource> _dataSourceTypes = null;

        ObservableCollection<DataMatcher> _dataMatchers = null;

        DataMatcher _dataMatcher = null;

        public Object FieldsSourceEntry = "";
        public SetItemsSourceView()
        {
            InitializeDataSourceTypes();

            InitializeDataFilters();

            this.DataContext = this;

            InitializeComponent();
        }

        public ObservableCollection<DataSource> DataSourceTypes
        {
            get
            {
                return _dataSourceTypes;
            }
        }

        public DataSource CurrentDataSource
        {
            get;
            set;
        }

        public ObservableCollection<DataMatcher> DataMatchers
        {
            get
            {
                return _dataMatchers;
            }
        }

        private void InitializeDataSourceTypes()
        {
            _dataSourceTypes = new ObservableCollection<DataSource>();
            _dataSourceTypes.Add(new DataSource_LocalFile());
            _dataSourceTypes.Add(new DataSource_WebUrl());
        }

        private void InitializeDataFilters()
        {
            _dataMatchers = new ObservableCollection<DataMatcher>();
            _dataMatchers.Add(new DataMatcher_NoFilter());
            _dataMatchers.Add(new DataMatcher_XPath());
            _dataMatchers.Add(new DataMatcher_Regex());
            _dataMatchers.Add(new DataMatcher_JsonPath());
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
    }
}
