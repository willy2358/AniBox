using AniBox.Framework.Data;
using AniBox.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AniBox.Framework.Data
{
    public class DataSupplier
    {
        ImmeUITimer _timer;
        public DataSupplier()
        {
        }

        public void StartUpdate()
        {
            if (UpdateInterval < 1)
            {
                return;
            }

            _timer = new ImmeUITimer(TimeSpan.FromSeconds(UpdateInterval));
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            Object obj = Source.GetUpdateObject();
            foreach(var v in Fields)
            {
                if (null != v.ResultListener)
                {
                    v.ResultListener(v, new EventArgs());
                }
            }
        }

        public string Name { get; set; }

        public Int32 UpdateInterval { get; set; }

        public DataSource Source { get; set; }

        public List<FieldEntry> Fields { get; set; }
    }
}
