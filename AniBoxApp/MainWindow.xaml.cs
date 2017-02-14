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

namespace AniBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DRAGED_CONTROL_DATA = "DragedControlType";
        private const string DRAGED_REGION_DATA = "DragedRegionType";

        public const string CONTROLS_FOLDER = "controls";
        private CompositionContainer _container;

        [ImportMany]
        IEnumerable<IAniControl> _controlTypes;

        [ImportMany]
        IEnumerable<IAniRegion> _regionTypes;

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

            string controlsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONTROLS_FOLDER);
            catalog.Catalogs.Add(new DirectoryCatalog(controlsDir));
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
            if (e.Data.GetDataPresent(DRAGED_CONTROL_DATA))
            {
                IAniControl aniControl = e.Data.GetData(DRAGED_CONTROL_DATA) as IAniControl;
                CreateControl(myCanvas, aniControl);
            }
            else if (e.Data.GetDataPresent(DRAGED_REGION_DATA))
            {
                IAniRegion aniRegion = e.Data.GetData(DRAGED_REGION_DATA) as IAniRegion;
                CreateRegion(myCanvas, aniRegion);
            }
        }

        private void CreateControl(Canvas canvas, IAniControl aniControl)
        {
            if (aniControl is WPFAniControl)
            {
                CreateWPFControl(canvas, aniControl as WPFAniControl);
            }
            else if (aniControl is HtmlAniControl)
            {
                CreateHtmlControl(canvas, aniControl as HtmlAniControl);
            }
        }

        private void CreateRegion(Canvas canvas, IAniRegion aniRegion)
        {
            System.Diagnostics.Debug.WriteLine("");
        }

        private void CreateWPFControl(Canvas canvas, WPFAniControl aniControl)
        {
            UserControl control = Activator.CreateInstance(aniControl.GetType()) as UserControl;
            control.Width = 300;
            control.Height = 300;
            Canvas.SetLeft(control, 20);
            Canvas.SetTop(control, 20);
            canvas.Children.Add(control);

            lstProperties.SelectedObject = control;
        }


        private void CreateHtmlControl(Canvas canvas, HtmlAniControl aniControl)
        {
            HtmlAniControl webControl = Activator.CreateInstance(aniControl.GetType()) as HtmlAniControl;
            ContentControl control = webControl.GetWPFControl();
            control.Width = 500;
            control.Height = 500;
            Canvas.SetLeft(control, 100);
            Canvas.SetTop(control, 10);
            canvas.Children.Add(control);

            lstProperties.SelectedObject = webControl;
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
                // Get the dragged ListViewItem
                ListBox listView = sender as ListBox;
                ListBoxItem listViewItem =
                    FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                IAniControl aniControl = (IAniControl)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);
                //IAniControl aniControl = controls.ElementAt(0);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(DRAGED_CONTROL_DATA, aniControl);
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

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DRAGED_CONTROL_DATA) || sender == e.Source)
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
                // Get the dragged ListViewItem
                ListBox listView = sender as ListBox;
                ListBoxItem listViewItem =
                    FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                IAniRegion aniRegion = (IAniRegion)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(DRAGED_REGION_DATA, aniRegion);
                DragDrop.DoDragDrop(lstControls, dragData, DragDropEffects.Move);
            } 
        }

    }
}
