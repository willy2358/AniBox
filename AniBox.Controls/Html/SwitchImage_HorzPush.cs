using AniBox.Framework.Controls;
using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls.Html
{
    [Export(typeof(AniControl))]
    class SwitchImage_HorzPush : HtmlAniControl, IUpdateData
    {
        public SwitchImage_HorzPush()
        {
            this.ControlWidth = 600;
            this.ControlHeight = 250;
        }
        protected override string HtmlResName
        {
            get { return "AniBox.Controls.res.HorScroll.html"; }
        }

        protected override List<HtmlResItem> HtmlReferedResources
        {
            get
            {
                List<HtmlResItem> items = new List<HtmlResItem>();
                items.Add(new HtmlResItem() { ResPath = "AniBox.Controls.res.overlap_1.jpg", FileName = "1.jpg" });
                items.Add(new HtmlResItem() { ResPath = "AniBox.Controls.res.overlap_2.jpg", FileName = "2.jpg" });
                return items;
            }
        }

        public override string ControlTypeName
        {
            get { return "SwitchImage_HorzPush"; }
        }

        public override string GetHtmlFile()
        {
            //return @"C:\Users\willy\Documents\Visual Studio 2013\Projects\AniBox\AniBox.Controls\res\sprite_cog.htm";
            string location = this.GetType().Assembly.Location;
            string dir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(location), System.IO.Path.GetFileNameWithoutExtension(location) + ".hres");
            string html = System.IO.Path.Combine(dir, "HorScroll.html");

            return html;

        }

        public void UpdateData(string source)
        {
            //throw new NotImplementedException();
        }

        public void OnFieldSourceUpdated(Object field, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public DataSource DataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
