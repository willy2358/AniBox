using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AniBox.Framework
{
    public abstract class HtmlAniControl : IAniControl
    {
        private ChromiumWebBrowser _webBrowser = null;
        public HtmlAniControl()
        {
            _webBrowser = new ChromiumWebBrowser();
            _webBrowser.IsBrowserInitializedChanged += _webBrowser_IsBrowserInitializedChanged;
        }

        void _webBrowser_IsBrowserInitializedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ChromiumWebBrowser browser = sender as ChromiumWebBrowser;
                browser.Load(GetHtmlFile());
            }
        }

        public ContentControl GetWPFControl()
        {
            if (null != _webBrowser)
            {
                return _webBrowser as ContentControl;
            }
            else
            {
                return null;
            }
        }

        public abstract string GetHtmlText();
        public abstract string GetHtmlFile();

        public abstract string ControlName
        {
            get;
        }

        [AniProperty]
        public double X
        {
            get;
            set;
        }

        [AniProperty]
        public double Y
        {
            get;
            set;
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
    }
}
