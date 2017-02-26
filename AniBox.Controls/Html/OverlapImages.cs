using AniBox.Framework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls.Html
{
    [Export(typeof(AniControl))]
    public class OverlapImages : HtmlAniControl
    {
        public OverlapImages()
        {
            this.ControlWidth = 400;
            this.ControlHeight = 500;
        }
        protected override string HtmlResName
        {
            get { return "AniBox.Controls.res.CasImgs.html"; }
        }

        protected override List<HtmlResItem> HtmlReferedResources
        {
            get 
            {
                List<HtmlResItem> items = new List<HtmlResItem>();
                items.Add(new HtmlResItem() { ResPath = "AniBox.Controls.res.overlap_1.jpg", FileName = "1.jpg" });
                items.Add(new HtmlResItem() { ResPath = "AniBox.Controls.res.overlap_2.jpg", FileName = "2.jpg" });
                items.Add(new HtmlResItem() { ResPath = "AniBox.Controls.res.overlap_3.jpg", FileName = "3.jpg" });
                return items;
            }
        }

        public override string ControlTypeName
        {
            get { return "OverlapImages"; }
        }
    }
}
