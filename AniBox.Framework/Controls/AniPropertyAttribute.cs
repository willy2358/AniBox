using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class AniPropertyAttribute : Attribute
    {
        public AniPropertyAttribute()
        {
        }

        public string Name { get; set; }
    }
}
