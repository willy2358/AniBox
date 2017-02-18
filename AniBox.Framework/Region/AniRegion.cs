using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using AniBox.Framework.Events;
using AniBox.Framework.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AniBox.Framework.Region
{
    public abstract class AniRegion : UserControl,IAniRegion
    {
        private SolidColorBrush CONTROL_SELECTED_BORDER_BRUSH = Brushes.LightGreen;

        public EventHandler<SelectControlEventArgs> OnSelectedControlChanged;

        private AniControl _selectedControl;

        private ObservableCollection<IAniControl> _aniControls = new ObservableCollection<IAniControl>();
        public AniRegion()
        {
            this.AllowDrop = true;

            this.DragEnter += AniRegion_DragEnter;

            this.Drop += AniRegion_Drop;
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
            get;
            set;
        }

        [AniProperty]
        public double RegionHeight
        {
            get;
            set;
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
                CreateControl(MyCanvas, aniControl, e.GetPosition(this));
                aniControl.ControlName = aniControl.ControlTypeName;
                this.AniControls.Add(aniControl);
                e.Handled = true;
            }
        }

        private void UpdateControlSelectState(AniControl lastSelectedCtrl, AniControl newSelectedCtrl)
        {
            if (null != lastSelectedCtrl)
            {
                Border border = lastSelectedCtrl.GetWPFControl().Parent as Border;
                if (null != border)
                {
                    border.BorderBrush = Brushes.Transparent;
                }
                lastSelectedCtrl.IsSelected = false;
            }
            if (null != newSelectedCtrl)
            {
                Border border = newSelectedCtrl.GetWPFControl().Parent as Border;
                if (null != border)
                {
                    border.BorderBrush = CONTROL_SELECTED_BORDER_BRUSH;
                }
                newSelectedCtrl.IsSelected = true;
            }
        }

        private void CreateControl(Canvas canvas, AniControl aniControl, Point postion)
        {
            AniControl control = Activator.CreateInstance(aniControl.GetType()) as AniControl;
            Border border = new Border();
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = CONTROL_SELECTED_BORDER_BRUSH;
            ContentControl actControl = control.GetWPFControl();
            border.Width = 300;
            border.Height = 300;
            border.Child = actControl;
            border.PreviewMouseLeftButtonDown += (sender, e) =>
                {
                    SelectedControl = control;
                };

            Canvas.SetLeft(border, postion.X);
            Canvas.SetTop(border, postion.Y);
            canvas.Children.Add(border);

            border.PreviewMouseLeftButtonDown += Element_MouseLeftButtonDown;
            border.PreviewMouseMove += Element_MouseMove;
            border.PreviewMouseLeftButtonUp += Element_MouseLeftButtonUp;
            SelectedControl = control;
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
