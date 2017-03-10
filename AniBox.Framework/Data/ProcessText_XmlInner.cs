using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AniBox.Framework.Data
{
    [Export(typeof(IProcessText))]
    public class ProcessText_XmlInner : ProcessText
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


        public override string Process(object item)
        {
            Input = item;
            if (item is XmlNode)
            {
                XmlNode node = item as XmlNode;
                XmlNode child = node.SelectSingleNode(TagName);
                if (null != child)
                {
                    Output = child.InnerText;
                    return Output.ToString();
                }
            }

            return "";
        }

        public override string Config
        {
            get { return "TagName:" + TagName; }
        }

   
    }
}
