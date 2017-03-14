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

namespace AniBox.Framework.UI
{
    /// <summary>
    /// SetTextProcessorsView.xaml 的交互逻辑
    /// </summary>
    public partial class SetTextProcessorsView : Window
    {
        public ObservableCollection<ProcessText> _selectedProcessors = new ObservableCollection<ProcessText>();

        private ProcessText.Link_Type _linkType = ProcessText.Link_Type.None;
        public SetTextProcessorsView()
        {
            this.DataContext = this;
            InitializeComponent();

            lstProperties.PropertyFilterType = typeof(AniBox.Framework.Attributes.AniPropertyAttribute);
        }

        public IEnumerable<ProcessText> ProcessTypes
        {
            get
            {
                return IoCTypes.ProcessTypes;
            }

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
            ProcessText newProcess = this.lstProperties.SelectedObject as ProcessText;
            newProcess.LinkType = _linkType;
            if (SelectedProcessors.Count > 0)
            {
                newProcess.Parent = SelectedProcessors[SelectedProcessors.Count - 1];
            }
            SelectedProcessors.Add(newProcess);
        }

        private void comboProcessTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProcessText process = Activator.CreateInstance(e.AddedItems[0].GetType()) as ProcessText;
            this.lstProperties.SelectedObject = process;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void radioLinkNone_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.None;
        }

        private void radioLinkInsert_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.InsertAhead;
        }

        private void radioLinkAsInput_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.AsInput;
        }

        private void radioLinkAppend_Checked(object sender, RoutedEventArgs e)
        {
            _linkType = ProcessText.Link_Type.AppendEnd;
        }
    }
}
