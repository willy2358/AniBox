using AniBox.Framework.Attributes;
using AniBox.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace AniBox.Framework.Controls
{
    public abstract class AniControl : IAniControl
    {
        public EventHandler<ControlSelectStateEventArgs> SelectStateChanged;
        protected bool _isSelected = false;

        private double _x = 0;
        private double _y = 0;

        

        public abstract ContentControl GetWPFControl();

        public AniControl()
        {

        }

        public abstract string ControlTypeName
        {
            get;
        }

        [AniProperty]
        public string ControlName
        {
            get;
            set;
        }

        [AniProperty]
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                Canvas.SetLeft(this.GetWPFControl().Parent as Border, _x);
            }
        }

        [AniProperty]
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                Canvas.SetTop(this.GetWPFControl().Parent as Border, _y);
            }
        }

        [AniProperty]
        public double ControlWidth
        {
            get;
            set;
        }

        [AniProperty]
        public double ControlHeight
        {
            get;
            set;
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value)
                {
                    return;
                }
                _isSelected = value;

            }
        }
    }
}
