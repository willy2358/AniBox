using AniBox.Framework.App;
using AniBox.Framework.Data;
using AniBox.Framework.Data.Process;
using AniBox.Framework.Utility;
using Newtonsoft.Json.Linq;
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
using System.Xml;

namespace AniBox.Framework.Views.Controls
{
    /// <summary>
    /// FilterDataSourceView.xaml 的交互逻辑
    /// </summary>
    public partial class DataSourceView : UserControl
    {
        DataMatcher _dataMatcher = null;

        public Object FieldsSourceEntry = "";

        public DataSourceView()
        {
            this.DataContext = this;
            InitializeComponent();
        }
        public IEnumerable<DataMatcher> DataMatchers
        {
            get
            {
                return IoCTypes.DataMatcherTypes;
            }
        }

        public IEnumerable<DataSource> DataSourceTypes
        {
            get
            {
                return IoCTypes.DataSourceTypes;
            }
        }

        public DataSource CurrentDataSource
        {
            get;
            private set;
        }

        public string SourceName
        {
            get;
            private set;
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
            else if (source is List<JToken>)
            {
                List<JToken> tokens = source as List<JToken>;
                if (tokens.Count > 0)
                {
                    this.FieldsSourceEntry = tokens[0].ToString();
                }
            }
            else
            {
                this.FieldsSourceEntry = "";
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

        private void testMatchBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSource ds = this.comboDSTypes.SelectedItem as DataSource;
            ds.SourceSetting = txtSourcePath.Text;
            ds.Encoding = this.comboEncoding.Text.Trim();
            _dataMatcher.Filter = this.txtFilterString.Text;

            string result = ds.GetUpdateString();
            UpdateFieldsSource(ds.GetUpdateObject());

            this.txtMatchResult.Text = result;

        }

        private void btnSetSourcePath_Click(object sender, RoutedEventArgs e)
        {
            SetPathProcessorsView dlg = new SetPathProcessorsView();
            dlg.Owner = UiSearchHelper.FindAnchestor<Window>(this);
            if (dlg.ShowDialog().Value)
            {
                List<ProcessText> proces = dlg.SelectedProcessors.ToList<ProcessText>();
                if (proces.Count > 0)
                {
                    this.txtSourcePath.Text = proces.Last<ProcessText>().Output.ToString();
                }
            }

        }

        private void txtSourceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SourceName = this.txtSourceName.Text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //comboDSTypes_SelectionChanged(null, null);
        }
    }
}
