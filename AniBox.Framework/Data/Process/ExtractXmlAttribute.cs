using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    [Export(typeof(ProcessText))]
    public class ExtractXmlAttribute : ExtractText
    {
        public override string Name
        {
            get { return "Extract Xml Tag Attribute"; }
        }

        [AniProperty]
        public string TagName
        {
            get;
            set;
        }

        [AniProperty]
        public string AttributeName
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

            String regex = "<" + TagName + ".*" + AttributeName + "=\"(?<attr>[\\w\\s]*)\"" + "[^>]*>[\\w\\s]*</" + TagName + ">";
            Match mat = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            if (mat.Success)
            {
                return mat.Groups["attr"].Value;
            }
            else
            {
                return "";
            }
        }
    }
}
