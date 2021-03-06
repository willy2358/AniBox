﻿using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.AniControls.Html
{
    [Export(typeof(AniControl))]
    public class RotateCogs : HtmlAniControl
    {
        public override string ControlTypeName
        {
            get { return "Rotate Cogs"; }
        }

        protected override string HtmlResName
        {
            get { return "AniBox.AniControls.res.sprite_cog.htm"; }
        }

        protected override List<HtmlResItem> HtmlReferedResources
        {
            get
            {
                List<HtmlResItem> items = new List<HtmlResItem>();
                items.Add(new HtmlResItem() { ResPath = "AniBox.AniControls.res", FileName = "cogs.png" });

                return items;
            }
        }

        public override string GetHtmlFile()
        {
            //return @"C:\Users\willy\Documents\Visual Studio 2013\Projects\AniBox\AniBox.AniControls\res\sprite_cog.htm";

            return "http://localhost:8088/testJs/sprite_cog.htm";
        }
    }
}
