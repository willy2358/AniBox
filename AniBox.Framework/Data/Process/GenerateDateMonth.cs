using AniBox.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    [Export(typeof(ProcessText))]
    class GenerateDateMonth : GenerateText
    {
        private int _digitNum = 2;

        [AniProperty]
        public int DigitNum
        {
            get
            {
                return _digitNum;
            }
            set
            {
                _digitNum = value;
            }
        }

        public override string Name
        {
            get { return "Month of Date"; }
        }

        public override string Generate()
        {
            string myOutput = System.DateTime.Now.Month.ToString("D" + DigitNum.ToString());
            if (_digitNum < 2)
            {
                myOutput = System.DateTime.Now.Month.ToString();
            }

            return myOutput;
        }
    }
}
