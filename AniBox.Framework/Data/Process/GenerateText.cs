using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data.Process
{
    public abstract class GenerateText : ProcessText
    {
        public abstract string Generate();

        public override string Process()
        {
            string myOutput = Generate();
            if (null == Parent)
            {
                return myOutput;
            }
            else
            {
                if (LinkType == Link_Type.AppendEnd)
                {
                    return Parent.Output + myOutput;
                }
                else if (Link_Type.InsertAhead == LinkType)
                {
                    return myOutput + Parent.Output;
                }
                else
                {
                    return myOutput;
                }
            }
        }
    }
}
