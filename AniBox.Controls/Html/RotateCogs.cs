using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls.Html
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
            get { return "AniBox.Controls.res.sprite_cog.htm"; }
        }

        protected override List<HtmlResItem> HtmlReferedResources
        {
            get
            {
                List<HtmlResItem> items = new List<HtmlResItem>();
                items.Add(new HtmlResItem() { ResPath = "AniBox.Controls.res", FileName = "cogs.png" });

                return items;
            }
        }

        public override string GetHtmlFile()
        {
            return @"C:\Users\willy\Documents\Visual Studio 2013\Projects\AniBox\AniBox.Controls\res\sprite_cog.htm";
        }
    }
}
