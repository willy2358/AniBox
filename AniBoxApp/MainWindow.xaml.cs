using System;
using System.Collections.Generic;
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

using AniBox.Framework;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using CefSharp.Wpf;
using CefSharp;
using AniBox.Framework.Region;
using AniBox.Framework.Share;

namespace AniBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string CONTROL_TYPES_FOLDER = "controls";
        public const string REGION_TYPES_FOLDER = "regions";
        private CompositionContainer _container;

        [ImportMany]
        IEnumerable<IAniControl> _controlTypes = null;

        [ImportMany]
        IEnumerable<IAniRegion> _regionTypes = null;

        List<AniRegion> _userRegions = new List<AniRegion>();

        private Point _startControlLstPoint;
        private Point _StartRegionLstPoint;

        public MainWindow()
        {
            InitializeAggregateCatalog();

            InitializeComponent();

            lstProperties.PropertyFilterType = typeof(AniBox.Framework.AniPropertyAttribute);

            this.lstControls.ItemsSource = _controlTypes;
            this.lstRegions.ItemsSource = _regionTypes;
        }

        private void InitializeAggregateCatalog()
        {
            var catalog = new AggregateCatalog();
            string controlTypesDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONTROL_TYPES_FOLDER);
            string regionTypesDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, REGION_TYPES_FOLDER);
            catalog.Catalogs.Add(new DirectoryCatalog(controlTypesDir));
            if (System.IO.Directory.Exists(regionTypesDir))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(regionTypesDir));
            }
            _container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(CommConst.DRAGED_REGION_DATA))
            {
                IAniRegion aniRegion = e.Data.GetData(CommConst.DRAGED_REGION_DATA) as IAniRegion;
                CreateRegion(aniRegion);
            }
        }

        private void CreateRegion(IAniRegion aniRegion)
        {
            TabItem regionItem= new TabItem();
            regionItem.Header = string.Format("region{0}", this.tabRegions.Items.Count + 1);

            AniRegion newRegion = Activator.CreateInstance(aniRegion.GetType()) as AniRegion;
            newRegion.OnSelectedControlChanged = (sender, e) =>
            {
                this.lstProperties.SelectedObject = e.SelectedControl;
            };
            regionItem.Content = newRegion;

            this.tabRegions.Items.Add(regionItem);
        }

        private void lstControls_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startControlLstPoint = e.GetPosition(null);
        }

        private void lstControls_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (lstControls.SelectedItems.Count < 1)
            {
                return;
            }

            Point mousePos = e.GetPosition(null);
            Vector diff = _startControlLstPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed
                && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    ||Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                )
            {
                ListBox listView = sender as ListBox;
                ListBoxItem listViewItem = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);
                if (null == listViewItem)
                {
                    return;
                }

                IAniControl aniControl = (IAniControl)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(CommConst.DRAGED_CONTROL_DATA, aniControl);
                DragDrop.DoDragDrop(lstControls, dragData, DragDropEffects.Move);
            } 
        }

        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void TabReions_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(CommConst.DRAGED_REGION_DATA) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Control - T Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.T 
                && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                //test codes
            }
        }

        private void lstProperties_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //System.Diagnostics.Debugger.Break();
        }

        private void lstProperties_PropertyEditingFinished(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debugger.Break();
        }

        private void lstProperties_PropertyValueChanged(object sender, System.Windows.Controls.WpfPropertyGrid.PropertyValueChangedEventArgs e)
        {
            //System.Diagnostics.Debugger.Break();
        }

        private void lstRegion_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _StartRegionLstPoint = e.GetPosition(null);
        }

        private void lstRegion_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (lstRegions.SelectedItems.Count < 1)
            {
                return;
            }

            Point mousePos = e.GetPosition(null);
            Vector diff = _StartRegionLstPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed
                && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                )
            {
                ListBox listView = sender as ListBox;
                ListBoxItem listViewItem = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);
                if (null == listViewItem)
                {
                    return;
                }

                IAniRegion aniRegion = (IAniRegion)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                DataObject dragData = new DataObject(CommConst.DRAGED_REGION_DATA, aniRegion);
                DragDrop.DoDragDrop(lstControls, dragData, DragDropEffects.Move);
            } 
        }

    }
}
