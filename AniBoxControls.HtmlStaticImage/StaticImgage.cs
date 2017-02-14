using AniBox.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls.HtmlStaticImage
{
    [Export(typeof(AniBox.Framework.IAniControl))]
    public class StaticImgage : HtmlAniControl
    {
        private string _imageFile;
        public override string ControlName
        {
            get { return "HtmlStaticImage"; }
        }

        public override string GetHtmlText()
        {
            Assembly _assembly;
            _assembly = Assembly.GetExecutingAssembly();
            StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("AniBox.Controls.HtmlStaticImage.res.StaticImage.html"));
            string html = _textStreamReader.ReadToEnd();
            return html;
        }

        public override string GetHtmlFile()
        {
            string htmFile = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetTempFileName());
            string htmlFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), htmFile + ".html");
            string htmlText = GetHtmlText();

            try
            {
                System.IO.File.WriteAllText(htmlFile, htmlText, Encoding.UTF8);
                
            }
            catch(Exception)
            {
                
            }

            return htmlFile;
        }

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
    }
}
