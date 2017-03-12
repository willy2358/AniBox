using AniBox.Framework.Controls;
using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Controls
{
    [Export(typeof(AniControl))]
    public class DynamicText : StaticText, IUpdateData
    {
        public DynamicText()
        {
            Text = "动态文本";
        }
        public override string ControlTypeName
        {
            get { return "DynamicText"; }
        }

        public void UpdateData(String source)
        {
            string textStr = source.ToString();
            this.Text = textStr;
        }


        public DataSource DataSource
        {
            get;
            set;
        }


        public void OnFieldSourceUpdated(Object field, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
