using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.DataSource
{
    public interface IUpdateData
    {
        void UpdateData(Object source);

        DataSource DataSource { get; set; }

       
    }
}
