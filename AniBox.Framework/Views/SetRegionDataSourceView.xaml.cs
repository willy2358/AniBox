using AniBox.Framework.App;
using AniBox.Framework.Data;
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

namespace AniBox.Framework.Views
{
    /// <summary>
    /// SetItemsSourceView.xaml 的交互逻辑
    /// </summary>
    public partial class SetRegionDataSourceView : Window
    {
        ObservableCollection<FieldEntry> _fields = new ObservableCollection<FieldEntry>();

        
        public SetRegionDataSourceView()
        {
            this.DataContext = this;

            InitializeComponent();
        }

        //public IEnumerable<DataSource> DataSourceTypes
        //{
        //    get
        //    {
        //        return IoCTypes.DataSourceTypes;
        //    }
        //}

        //public String SourceName
        //{
        //    get;
        //    private set;
        //}

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

        private void addFieldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (null == this.dsControl.FieldsSourceEntry || string.IsNullOrEmpty(this.dsControl.FieldsSourceEntry.ToString()))
            {
                return;
            }
            AddRegionFieldView dlg = new AddRegionFieldView();
            dlg.FieldInput = this.dsControl.FieldsSourceEntry;
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult.Value && dlg.Processors.Count > 0)
            {
                FieldEntry field = new FieldEntry();
                field.SourceInput = this.dsControl.FieldsSourceEntry;
                field.FieldName = dlg.FieldName;
                List<ProcessText> procs = new List<ProcessText>();
                procs.Add(dlg.HeadProcess);
                procs.AddRange(dlg.Processors);
                field.Processors = procs;
                _fields.Add(field);
            }
        }

        private void delFieldBtn_Click(object sender, RoutedEventArgs e)
        {

        }





        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            //this.SourceName = this.dsControl.txtSourceName.Text;

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


    }
}
