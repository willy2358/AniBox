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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AniBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public const string CONTROL_TYPES_FOLDER = "controls";
        public const string REGION_TYPES_FOLDER = "regions";
        private CompositionContainer _container;

        [ImportMany]
        IEnumerable<IAniControl> _controlTypes = null;

        [ImportMany]
        IEnumerable<IAniRegion> _regionTypes = null;

        ObservableCollection<AniRegion> _userRegions = new ObservableCollection<AniRegion>();
        

        private Point _startControlLstPoint;
        private Point _StartRegionLstPoint;

        private AniRegion _currentRegion = null;

        #region codes for bindableBase
        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion 

        public MainWindow()
        {
            InitializeAggregateCatalog();

            InitializeComponent();

            this.DataContext = this;

            lstProperties.PropertyFilterType = typeof(AniBox.Framework.AniPropertyAttribute);

            this.lstControls.ItemsSource = _controlTypes;
            this.lstRegions.ItemsSource = _regionTypes;
        }

        public ObservableCollection<AniRegion> UserRegions
        {
            get
            {
                return _userRegions;
            }
        }

        public AniRegion CurrentRegion
        {
            get
            {
                return _currentRegion;
            }
            set
            {
                SetProperty(ref _currentRegion, value, "CurrentRegion");
            }
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
            AniRegion newRegion = Activator.CreateInstance(aniRegion.GetType()) as AniRegion;
            newRegion.OnSelectedControlChanged = (sender, e) =>
            {
                this.lstProperties.SelectedObject = e.SelectedControl;
            };
            newRegion.RegionName = string.Format("region{0}", this.tabRegions.Items.Count + 1);
            this.UserRegions.Insert(0, newRegion);

            tabRegions.SelectedItem = newRegion;
            this.CurrentRegion = newRegion;
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

        private void testRun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deployRun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tabRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
