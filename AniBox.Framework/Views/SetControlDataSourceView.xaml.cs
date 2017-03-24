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
        //ObservableCollection<DataSource> _dataSourceTypes = null;

        //ObservableCollection<DataMatcher> _dataMatchers = null;

        //DataMatcher _dataMatcher = null;
        public SetControlDataSourceView()
        {
        //    InitializeDataSourceTypes();

        //    InitializeDataFilters();

            this.DataContext = this;

            InitializeComponent();
        }

        //public ObservableCollection<DataSource> DataSourceTypes
        //{
        //    get
        //    {
        //        return _dataSourceTypes;
        //    }
        //}

        public DataSource CurrentDataSource
        {
            get;
            set;
        }

        //public ObservableCollection<DataMatcher> DataMatchers
        //{
        //    get
        //    {
        //        return _dataMatchers;
        //    }
        //}

        //private void InitializeDataSourceTypes()
        //{
        //    _dataSourceTypes = new ObservableCollection<DataSource>();
        //    _dataSourceTypes.Add(new DataSource_LocalFile());
        //    _dataSourceTypes.Add(new DataSource_WebUrl());
        //    _dataSourceTypes.Add(new DataSource_RandomGenerator());
        //}

        //private void InitializeDataFilters()
        //{
        //    _dataMatchers = new ObservableCollection<DataMatcher>();
        //    _dataMatchers.Add(new DataMatcher_NoFilter());
        //    _dataMatchers.Add(new DataMatcher_XPath());
        //    _dataMatchers.Add(new DataMatcher_Regex());
        //    _dataMatchers.Add(new DataMatcher_JsonPath());
        //}

        //private void btnTestDataSource_Click(object sender, RoutedEventArgs e)
        //{
        //    DataSource ds = this.comboDSTypes.SelectedItem as DataSource;
        //    ds.SourceSetting = txtDataSourceSetting.Text;

        //    string rawText = ds.GetUpdateString();
        //    txtSourceRawData.Text = rawText;

        //    if (null != _dataMatcher)
        //    {
        //        txtFilteredData.Text = _dataMatcher.FilterData(rawText).ToString();
        //    }
        //}

        //private void comboDSTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (null != txtDataSourceSetting)
        //    {
        //        DataSource ds = this.comboDSTypes.SelectedItem as DataSource;

        //        txtDataSourceSetting.Text = ds.SourceSetting;

        //        if (null != _dataMatcher)
        //        {
        //            ds.DataMatcher = _dataMatcher;
        //        }

        //        CurrentDataSource = ds;
        //    }
        //}

        //private void comboMatchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _dataMatcher = this.comboMatchers.SelectedItem as DataMatcher;
        //    if (null != CurrentDataSource)
        //    {
        //        CurrentDataSource.DataMatcher = _dataMatcher;
        //    }
        //}

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
