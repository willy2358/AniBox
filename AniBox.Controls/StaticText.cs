using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AniBox.Controls
{
    [Export(typeof(AniControl))]
    public class StaticText : WPFAniControl
    {
        StaticTextControl _textControl = new StaticTextControl();
        public override System.Windows.Controls.ContentControl GetWPFControl()
        {
            return _textControl;
        }

        public override string ControlTypeName
        {
            get { return "StaticText"; }
        }

        [AniProperty]
        public String Text
        {
            get
            {
                return _textControl.textBlock.Text;
            }
            set
            {
                _textControl.textBlock.Text = value;
            }
        }

        [AniProperty]
        public double TextSize
        {
            get
            {
                return _textControl.textBlock.FontSize;
            }
            set
            {
                _textControl.textBlock.FontSize = value;
            }
        }

        private Color _textColor = Colors.Black;
        [AniProperty]
        public Color TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
                _textControl.textBlock.Foreground = new SolidColorBrush(_textColor);
            }
        }
    }
}
