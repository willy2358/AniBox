using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;


namespace AniBox.Framework.Data.Process
{
    [Export(typeof(ProcessText))]
    public class ProcessText_XmlInner : ProcessText
    {
        public override string Name
        {
            get { return "Extract Xml Tag Inner Text"; }
        }

        [AniProperty]
        public string XmlSnippet
        {
            get;
            set;
        }

        [AniProperty]
        public string TagName
        {
            get;
            set;
        }


        public override string Process()
        {
            String regex = "<" + TagName +"[^>]*>(?<text>\\w*)</" + TagName +">";

            Match mat = Regex.Match(XmlSnippet, regex, RegexOptions.IgnoreCase);
            if (mat.Success)
            {
                //if (mat.Groups["text"].Value)

                return mat.Groups["text"].Value;
            }
            else
            {
                return "";
            }

        }

        public override string Config
        {
            get { return "TagName:" + TagName; }
        }

   
    }
}
