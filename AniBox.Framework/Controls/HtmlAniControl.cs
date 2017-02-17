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
            ScrollViewer.SetHorizontalScrollBarVisibility(_webBrowser, ScrollBarVisibility.Hidden);
            ScrollViewer.SetVerticalScrollBarVisibility(_webBrowser, ScrollBarVisibility.Hidden);
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

        protected void UpdateDomElementAttribute(string elemId, string attrName, string attrValue)
        {
            if (null == _webBrowser)
            {
                return;
            }

            //string script = "$(\"#thisBtn\").value='234';";
            string script = " var elem = document.getElementById('" + elemId + "');"
                          + " if (null != elem) { elem." + attrName + "='" + attrValue + "';}";
            CefSharp.WebBrowserExtensions.ExecuteScriptAsync(_webBrowser, script);
        }

        public abstract string GetHtmlText();
        public abstract string GetHtmlFile();

        [AniProperty]
        public string ControlName
        {
            get;
            set;
        }

        public abstract string ControlTypeName
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
