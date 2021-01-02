using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    public interface IProcessText
    {
        String Name { get; }

        string Process();

    }
}
