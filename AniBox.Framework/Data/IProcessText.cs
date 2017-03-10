using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public interface IProcessText
    {
        String Name { get; }

        string Process(Object item);

        string Config { get; }

        Object Input { get; set; }

        Object Output { get; set; }
    }
}
