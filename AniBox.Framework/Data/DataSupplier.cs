using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public class DataSupplier
    {
        public string Name { get; set; }

        public DataSource Source { get; set; }

        public List<FieldEntry> Fields { get; set; }
    }
}
