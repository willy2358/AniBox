﻿using AniBox.Framework.Attributes;
using AniBox.Framework.AniEventArgs;
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
        public const double DEFAULT_CONTROL_HEIGHT = 200;
        public const double DEFAULT_CONTROL_WIDTH = 300;
        public EventHandler<ControlSelectStateEventArgs> SelectStateChanged;
        public EventHandler OnPositionChanged;
        public EventHandler OnSizeChanged;
        protected bool _isSelected = false;

        private double _x = 0;
        private double _y = 0;

        public abstract ContentControl GetWPFControl();

        public AniControl()
        {
            ContentControl actualControl = GetWPFControl();
            if (this.ControlWidth == DEFAULT_CONTROL_WIDTH
                && !double.IsNaN(actualControl.Width))
            {
                this.ControlWidth = actualControl.Width;
            }
           
            if (this.ControlHeight == DEFAULT_CONTROL_HEIGHT
                && !double.IsNaN(actualControl.Height))
            {
                this.ControlHeight = actualControl.Height;
            }
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
                if (null != OnPositionChanged)
                {
                    OnPositionChanged(this, new EventArgs());
                }
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
                if (null != OnPositionChanged)
                {
                    OnPositionChanged(this, new EventArgs());
                }
            }
        }

        private double _controlWidth = DEFAULT_CONTROL_WIDTH; 
        [AniProperty]
        public double ControlWidth
        {
            get
            {
                return _controlWidth;
            }
            set
            {
                _controlWidth = value;
                if (null != OnSizeChanged)
                {
                    OnSizeChanged(this, new EventArgs());
                }
            }
        }

        private double _controlHeight = DEFAULT_CONTROL_HEIGHT;
        [AniProperty]
        public double ControlHeight
        {
            get                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            {
                return _controlHeight;
            }
            set
            {
                _controlHeight = value;
                if (null != OnSizeChanged)
                {
                    OnSizeChanged(this, new EventArgs());
                }
            }
        }

        public bool                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             IsSelected
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
