﻿using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    public abstract class ProcessText : IProcessText
    {
        private Object _input;
        protected Object _output;
        public enum Link_Type { None = 0, AppendEnd, AsInput, InsertAhead };

        private Link_Type _linkType = Link_Type.None;

        public abstract string Config { get; }

        public ProcessText Parent { get; set; }

        public Link_Type LinkType
        {
            get
            {
                return _linkType;
            }
            set
            {
                _linkType = value;
            }
        }

        public object Output
        {
            get
            {
                return Process();
            }
            //set
            //{
            //    _output = value;
            //}
        }

        public abstract string Name{get;}


        public abstract string Process();

    }
}
