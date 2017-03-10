using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    public abstract class ProcessText : IProcessText
    {
        private Object _input;
        private Object _output;


        public abstract string Config { get; }

        public object Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
            }
        }

        public object Output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
            }
        }

        public abstract string Name{get;}


        public abstract string Process(object item);

    }
}
