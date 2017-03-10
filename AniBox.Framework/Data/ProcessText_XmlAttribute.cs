﻿using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    [Export(typeof(IProcessText))]
    public class ProcessText_XmlAttribute : ProcessText
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


        public override string Process(object item)
        {
            throw new NotImplementedException();
        }


        public override string Config
        {
            get 
            {
                return "TagName:" + TagName + ";" + "AttributeName:" + AttributeName;
            }
        }
    }
}
