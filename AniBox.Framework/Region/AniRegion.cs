using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using AniBox.Framework.Data;
using AniBox.Framework.Events;
using AniBox.Framework.Share;
using AniBox.Framework.SyncUpdate;
using AniBox.Framework.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace AniBox.Framework.Region
{
    public abstract class AniRegion : UserControl,IAniRegion
    {
        private const double DEFAULT_CONTROL_WIDTH = 300;
        private const double DEFAULT_CONTROL_HEIGHT = 300;
        private SolidColorBrush CONTROL_SELECTED_BORDER_BRUSH = Brushes.LightGreen;

        public EventHandler<SelectControlEventArgs> OnSelectedControlChanged;

        private AniControl _selectedControl;

        private ObservableCollection<IAniControl> _aniControls = new ObservableCollection<IAniControl>();

        private Dictionary<Border, AniControl> _hostAndControlsRel = new Dictionary<Border, AniControl>();

        private ObservableCollection<UITimer> _timers = new ObservableCollection<UITimer>();

        private List<DataSource> _dataSourceTypes { set; get; }

        public AniRegion()
        {
            this.AllowDrop = true;

            this.DragEnter += AniRegion_DragEnter;

            this.Drop += AniRegion_Drop;

            this.ContextMenu = GetContextMenu();
        }

        protected abstract Canvas MyCanvas { get; }

        public abstract string RegionTypeName { get; }


        [AniProperty]
        public string RegionName
        {
            get;
            set;
        }

        public AniControl SelectedControl
        {
            get
            {
                return _selectedControl;
            }
            set
            {
                if (_selectedControl != value)
                {
                    UpdateControlSelectState(_selectedControl, value);
                    
                    _selectedControl = value;
                    if (null != OnSelectedControlChanged)
                    {
                        OnSelectedControlChanged(this, new SelectControlEventArgs(_selectedControl));
                    }
                }
            }
        }

        public ObservableCollection<IAniControl> AniControls
        {
            get
            {
                return _aniControls;
            }
        }

        public List<DataSource> DataSourceTypes
        {
            get
            {
                return _dataSourceTypes;
            }
        }

        public ObservableCollection<UITimer> Timers
        {
            get
            {
                return _timers;
            }
        }

        [AniProperty]
        public double XScreenPos
        {
            get;
            set;
        }

        [AniProperty]
        public double YScreenPos
        {
            get;
            set;
        }

        [AniProperty]
        public double RegionWidth
        {
            get
            {
                return this.Width;
            }
            set
            {
                this.Width = value;
            }
        }

        [AniProperty]
        public double RegionHeight
        {
            get
            {
                return this.Height;
            }
            set
            {
                this.Height = value;
            }
        }

        void AniRegion_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(CommConst.DRAGED_CONTROL_DATA) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        void AniRegion_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(CommConst.DRAGED_CONTROL_DATA))
            {
                AniControl aniControl = e.Data.GetData(CommConst.DRAGED_CONTROL_DATA) as AniControl;
                AniControl newControl = CreateControl(MyCanvas, aniControl, e.GetPosition(this));
                newControl.ControlName = aniControl.ControlTypeName;
                this.AniControls.Add(newControl);
                e.Handled = true;
            }
        }

        private ContextMenu GetContextMenu()
        {
            ContextMenu ctxMenu = new ContextMenu();

            MenuItem menuItem = new MenuItem(){ Header = "Add Timer"};
            menuItem.Click += menuItem_Click;
            
            ctxMenu.Items.Add(menuItem);

            return ctxMenu;
        }

        void menuItem_Click(object sender, RoutedEventArgs e)
        {
            UITimer timer = new UITimer();
            timer.Name = "Timer" + (Timers.Count + 1).ToString();

            Timers.Add(timer);

            timer.Start();
        }

        private void UpdateControlSelectState(AniControl lastSelectedCtrl, AniControl newSelectedCtrl)
        {
            if (null != lastSelectedCtrl)
            {
                Border border = GetControlBorder(lastSelectedCtrl);
                if (null != border)
                {
                    border.BorderBrush = Brushes.Transparent;
                }
                lastSelectedCtrl.IsSelected = false;
            }
            if (null != newSelectedCtrl)
            {
                Border border = GetControlBorder(newSelectedCtrl);
                if (null != border)
                {
                    border.BorderBrush = CONTROL_SELECTED_BORDER_BRUSH;
                }
                newSelectedCtrl.IsSelected = true;
            }
        }
    
        private AniControl CreateControl(Canvas canvas, AniControl aniControlTemplate, Point postion)
        {
            AniControl control = Activator.CreateInstance(aniControlTemplate.GetType()) as AniControl;
            control.X = postion.X;
            control.Y = postion.Y;
            control.ControlWidth = control.ControlWidth;
            control.ControlHeight = control.ControlHeight;
            Border border = CreateControlContainer(control);
            border.ContextMenu = CreateControlContextMenu(control);
            border.Tag = control;
            canvas.Children.Add(border);
            _hostAndControlsRel.Add(border, control);

            control.OnSizeChanged += (sender, e) =>
            {
                border.Width = (sender as AniControl).ControlWidth;
                border.Height = (sender as AniControl).ControlHeight;
            };
            control.OnPositionChanged += (sender, e) =>
            {
                AniControl aniCtrl = sender as AniControl;
                Canvas.SetTop(border, aniCtrl.Y);
                Canvas.SetLeft(border, aniCtrl.X);
            };

            SelectedControl = control;
            return control;
        }

        private ContextMenu CreateControlContextMenu(AniControl aniControl)
        {
            if (aniControl is IUpdateData)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem() { Header = "设置数据源" };
                item.Tag = aniControl;
                item.Click += SetDataSource_Click;

                menu.Items.Add(item);
                return menu;
            }

            return null;
        }

        void SetDataSource_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            SetDataSourceWindow dlg = new SetDataSourceWindow();
            bool? ret = dlg.ShowDialog();
            if (ret.HasValue && ret.Value)
            {
                (menuItem.Tag as IUpdateData).DataSource = dlg.CurrentDataSource;
            }
        }

        private Border CreateControlContainer(AniControl aniControl)
        {
            ContentControl actControl = aniControl.GetWPFControl();

            Grid grid = new Grid();
            grid.Children.Add(actControl);

            Border border = new Border();
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = CONTROL_SELECTED_BORDER_BRUSH;
            border.AllowDrop = true;
            border.Drop += actControl_Drop;
            border.DragEnter += actControl_DragEnter;

            border.Width = aniControl.ControlWidth;
            border.Height = aniControl.ControlHeight;
            border.Child = grid;
            border.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                SelectedControl = aniControl;
            };
            border.PreviewMouseLeftButtonDown += Element_MouseLeftButtonDown;
            border.PreviewMouseMove += Element_MouseMove;
            border.PreviewMouseLeftButtonUp += Element_MouseLeftButtonUp;

            Canvas.SetLeft(border, aniControl.X);
            Canvas.SetTop(border, aniControl.Y);
            return border;
        }

        void actControl_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(CommConst.DRAGED_TIMER_DATA) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private AniControl GetActualControl(Border container)
        {
            return container.Tag as AniControl;
        }

        void actControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(CommConst.DRAGED_TIMER_DATA))
            {
                UITimer timer = e.Data.GetData(CommConst.DRAGED_TIMER_DATA) as UITimer;
                Border border = sender as Border;
                AniControl control = GetActualControl(border);
                if (control is IUpdateData)
                {
                    timer.AddUptateControl(control as IUpdateData);
                    AddTimerIndicatorToControl(border.Child as Grid);
                    e.Handled = true;
                }
            }
        }

        private Border GetControlBorder(AniControl aniControl)
        {
            foreach(var v in _hostAndControlsRel)
            {
                if (v.Value == aniControl)
                {
                    return v.Key;
                }
            }

            return null;
        }

        private AniControl GetBorderControl(Border border)
        {
            if (_hostAndControlsRel.ContainsKey(border))
            {
                return _hostAndControlsRel[border];
            }

            return null;
        }


        private void AddTimerIndicatorToControl(Grid grid)
        {
            if (null == grid)
            {
                return;
            }
            TimerImage timer = new TimerImage();
            timer.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            timer.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            grid.Children.Add(timer);
        }

        #region Move Controls
        bool isDragDropInEffect = false;
        Point _movePos = new Point();
        void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement currEle = sender as FrameworkElement;
                double xPos = e.GetPosition(null).X - _movePos.X + (double)currEle.GetValue(Canvas.LeftProperty);
                double yPos = e.GetPosition(null).Y - _movePos.Y + (double)currEle.GetValue(Canvas.TopProperty);
                currEle.SetValue(Canvas.LeftProperty, xPos);
                currEle.SetValue(Canvas.TopProperty, yPos);
                _movePos = e.GetPosition(null);

                AniControl aniControl = GetBorderControl(sender as Border);
                aniControl.X = xPos;
                aniControl.Y = yPos;

                RefreshControlProperties();
            }
        }

        private void RefreshControlProperties()
        {
            if (null != OnSelectedControlChanged)
            {
                OnSelectedControlChanged(this, new SelectControlEventArgs(null));

                OnSelectedControlChanged(this, new SelectControlEventArgs(_selectedControl));
            }
        }

        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement fEle = sender as FrameworkElement;
            isDragDropInEffect = true;
            _movePos = e.GetPosition(null);
            fEle.CaptureMouse();
            fEle.Cursor = Cursors.Hand;
        }

        void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement ele = sender as FrameworkElement;
                isDragDropInEffect = false;
                ele.ReleaseMouseCapture();
            }
        }
        #endregion
    }
}
