using AniBox.Framework.Attributes;
using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls
{
    [Export(typeof(AniControl))]
    class HtmlStaticImage : HtmlAniControl
    {
        private string _imageFile;
        public override string ControlTypeName
        {
            get { return "HtmlStaticImage"; }
        }

        //public override string GetHtmlText()
        //{
        //    Assembly _assembly;
        //    string htmlText = "";
        //    _assembly = Assembly.GetExecutingAssembly();
        //    string htmlFile = "AniBox.Controls.res." + "StaticImage.html";
        //    if(_assembly.GetManifestResourceNames().Contains(htmlFile))
        //    {
        //        StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(htmlFile));
        //        htmlText = _textStreamReader.ReadToEnd();
        //    }
        //    return htmlText;
        //}

        //public override string GetHtmlFile()
        //{
        //    string htmFile = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetTempFileName());
        //    string htmlFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), htmFile + ".html");
        //    string htmlText = GetHtmlText();

        //    try
        //    {
        //        System.IO.File.WriteAllText(htmlFile, htmlText, Encoding.UTF8);

        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return htmlFile;
        //}

        [AniProperty]
        public string ImageFile
        {
            get
            {
                return _imageFile;
            }
            set
            {
                _imageFile = value;
                string imgSrc = _imageFile.Replace('\\', '/');
                UpdateDomElementAttribute("myImg", "src", imgSrc);
            }
        }

        protected override string HtmlResName
        {
            get { return "AniBox.Controls.res." + "StaticImage.html"; }
        }

        protected override List<HtmlResItem> HtmlReferedResources
        {
            get { return null; }
        }
    }
}
