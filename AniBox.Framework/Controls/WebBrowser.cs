using AniBox.Framework.Attributes;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AniBox.Framework.Controls
{
    [Export(typeof(AniControl))]
    public class WebBrowser : AniControl
    {
        private const string DEFAULT_INITIAL_URL = "www.baidu.com";

        private string _initialUrl = DEFAULT_INITIAL_URL;

        private ChromiumWebBrowser _webBrowser = new ChromiumWebBrowser();
        public WebBrowser()
        {
            ScrollViewer.SetHorizontalScrollBarVisibility(_webBrowser, ScrollBarVisibility.Hidden);
            ScrollViewer.SetVerticalScrollBarVisibility(_webBrowser, ScrollBarVisibility.Hidden);
            _webBrowser.IsBrowserInitializedChanged += _webBrowser_IsBrowserInitializedChanged;
            _webBrowser.MouseLeftButtonDown += _webBrowser_MouseLeftButtonDown;
        }

        void _webBrowser_IsBrowserInitializedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ChromiumWebBrowser browser = sender as ChromiumWebBrowser;
                
                browser.Load(InitialUrl);
            }
        }

        void _webBrowser_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelected = true;
        }

        public override System.Windows.Controls.ContentControl GetWPFControl()
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

        public override string ControlTypeName
        {
            get { return "Web Browser"; }
        }

        [AniProperty]
        public String InitialUrl
        {
            get
            {
                return _initialUrl;
            }
            set
            {
                _initialUrl = value;
                if (!string.IsNullOrEmpty(_initialUrl))
                {
                    this._webBrowser.Load(_initialUrl);
                }
            }
        }
    }
}
