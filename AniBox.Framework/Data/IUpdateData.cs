using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public interface IUpdateData
    {
        void UpdateData(String source);

        void OnFieldSourceUpdated(Object field, EventArgs e);

        DataSource DataSource { get; set; }




       
    }
}
