using AniBox.Framework.Attributes;
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
        protected Object _output;


        public abstract string Config { get; }

        //[AniProperty]
        //public object Input
        //{
        //    get
        //    {
        //        return _input;
        //    }
        //    set
        //    {
        //        _input = value;
        //    }
        //}

        public object Output
        {
            get
            {
                if (null == _output)
                {
                    _output = Process();
                }

                return _output;
            }
            set
            {
                _output = value;
            }
        }

        public abstract string Name{get;}


        public abstract string Process();

    }
}
