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

namespace AniBox.Framework.Region
{
    public abstract class AniRegion : UserControl,IAniRegion
    {
        public EventHandler<SelectControlEventArgs> OnSelectedControlChanged;

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

        public ObservableCollection<IAniControl> AniControls
        {
            get
            {
                return _aniControls;
            }
        }


        public double XPos
        {
            get;
            set;
        }

        public double YPos
        {
            get;
            set;
        }

        public double RegionWidth
        {
            get;
            set;
        }

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
                IAniControl aniControl = e.Data.GetData(CommConst.DRAGED_CONTROL_DATA) as IAniControl;
                CreateControl(MyCanvas, aniControl);
                aniControl.ControlName = aniControl.ControlTypeName;
                this.AniControls.Add(aniControl);
                e.Handled = true;
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

        private void CreateWPFControl(Canvas canvas, WPFAniControl aniControl)
        {
            UserControl control = Activator.CreateInstance(aniControl.GetType()) as UserControl;
            control.Width = 300;
            control.Height = 300;
            Canvas.SetLeft(control, 20);
            Canvas.SetTop(control, 20);
            canvas.Children.Add(control);

            if (null != OnSelectedControlChanged)
            {
                OnSelectedControlChanged(this, new SelectControlEventArgs(aniControl));
            }
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

            if (null != OnSelectedControlChanged)
            {
                OnSelectedControlChanged(this, new SelectControlEventArgs(aniControl));
            }
        }
    }
}
