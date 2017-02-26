using AniBox.Framework.Controls;
using AniBox.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AniBox.Framework.SyncUpdate
{
    public class UITimer
    {
        private List<IUpdateData> _updateObjs = new List<IUpdateData>();
        
        private DispatcherTimer _actualTimer = new DispatcherTimer();

        private UInt32 _interval = 5;

        public String Name { get; set; }

        public UITimer()
        {

        }

        public UInt32 Interval
        {
            get;
            set;
        }

        //public UITimer(TimeSpan interval, bool immediateStart = true)
        //{
        //    _interval = interval;
        //    if (immediateStart)
        //    {
        //        _actualTimer.Interval = TimeSpan.FromSeconds(0.1);
        //    }
        //}
        
        public void AddUptateControl(IUpdateData update)
        {
            if (null != update)
            {
                lock(_updateObjs)
                {
                    _updateObjs.Add(update);
                }
            }
            
        }


        public void Start()
        {
            _actualTimer.Interval = TimeSpan.FromSeconds(0.1);

            _actualTimer.Tick += _actualTimer_Tick;

            _actualTimer.Start();
        }

        void _actualTimer_Tick(object sender, EventArgs e)
        {
            if (_actualTimer.Interval < TimeSpan.FromSeconds(_interval))
            {
                _actualTimer.Stop();
                _actualTimer.Interval = TimeSpan.FromSeconds(_interval);
                _actualTimer.Start();
            }

            lock(_updateObjs)
            {
                for (int i = 0; i < _updateObjs.Count; i++)
                {
                    DataSource ds = _updateObjs[i].DataSource;
                    if (null != ds)
                    {
                        string data = ds.GetUpdate();
                        if (null != data)
                        {
                            _updateObjs[i].UpdateData(data);
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            try
            {
                _actualTimer.Stop();
            }
            catch (Exception)
            {

            }
        }
    }
}
