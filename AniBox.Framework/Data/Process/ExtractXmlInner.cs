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
    public class ExtractXmlInner : ExtractText
    {
        public override string Name
        {
            get { return "Extract Xml Tag Inner Text"; }
        }

        [AniProperty]
        public string TagName
        {
            get;
            set;
        }

        public override string Extract(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(TagName))
            {
                return "";
            }

            String regex = "<" + TagName + "[^>]*>(?<text>[\\w\\s]*)</" + TagName + ">";
            Match mat = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            if (mat.Success)
            {
                return mat.Groups["text"].Value;
            }
            else
            {
                return "";
            }
        }
    }
}
