using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using CefSharp.Wpf;
using CefSharp;
using System.Reflection;
using System.IO;


namespace AniBox.Framework.Controls
{
    public abstract class HtmlAniControl : AniControl
    {
        private ChromiumWebBrowser _webBrowser = new ChromiumWebBrowser();
        public HtmlAniControl()
        {
            ScrollViewer.SetHorizontalScrollBarVisibility(_webBrowser, ScrollBarVisibility.Hidden);
            ScrollViewer.SetVerticalScrollBarVisibility(_webBrowser, ScrollBarVisibility.Hidden);
            _webBrowser.IsBrowserInitializedChanged += _webBrowser_IsBrowserInitializedChanged;
            _webBrowser.MouseLeftButtonDown += _webBrowser_MouseLeftButtonDown;
        }

        void _webBrowser_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelected = true;
        }

        void _webBrowser_IsBrowserInitializedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ChromiumWebBrowser browser = sender as ChromiumWebBrowser;
                browser.MenuHandler = new MenuHandler();
                string html = GetHtmlFile();
                if (string.IsNullOrEmpty(html))
                {
                    html = GetHtmlFromEmbeddedResource();
                }

                if (string.IsNullOrEmpty(html))
                {
                    //log error
                    return;
                }

                //browser.LoadHtml(GetHtmlText(), "http://myhtml/test");
                browser.Load(html);
            }
        }

        public override ContentControl GetWPFControl()
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

        private string GetHtmlFromEmbeddedResource()
        {
            string error;
            string html = ReadHtmlResource(HtmlResName, out error);
            if (!string.IsNullOrEmpty(html))
            {
                if ( null != HtmlReferedResources)
                {
                    foreach(var v in HtmlReferedResources)
                    {
                        if(!ReadReferedResource(v.ResPath, v.FileName, out error))
                        {
                            //log error
                        }
                    }
                }
                return html;
            }

            return "";
        }

        public virtual string GetHtmlFile()
        {
            return "";
        }

        protected abstract string HtmlResName { get; }
        protected abstract List<HtmlResItem> HtmlReferedResources { get; }

        private bool SaveEmbeddedResourceToTmpFile(string resPath, string saveFile, out string error)
        {
            error = "";
            Assembly _assembly = this.GetType().Assembly;
            if (!_assembly.GetManifestResourceNames().Contains(resPath))
            {
                error = "Not existed resource:" + resPath;
            }

            try
            {
                using (BinaryReader sr = new BinaryReader(_assembly.GetManifestResourceStream(resPath)))
                {
                    using (FileStream sw = File.Create(saveFile, (int)sr.BaseStream.Length))
                    {
                        byte[] bytes = new byte[sr.BaseStream.Length];
                        sr.Read(bytes, 0, (int)sr.BaseStream.Length);
                        sw.Write(bytes, 0, bytes.Length);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        protected bool ReadReferedResource(string resPath, string fileName, out String error)
        {
            error = "";
            string resName = resPath;
            string savedFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileName);

            return SaveEmbeddedResourceToTmpFile(resName, savedFile, out error);
        }

        protected string ReadHtmlResource(string htmlResName, out string error)
        {
            if (string.IsNullOrEmpty(htmlResName))
            {
                error = "No html resource";
                return "";
            }
            string htmFile = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetTempFileName());
            string htmlPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), htmFile + ".html");

            if(SaveEmbeddedResourceToTmpFile(htmlResName, htmlPath, out error))
            {
                return htmlPath;
            }

            return "";
        }
    }
}
