using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AniBox.Framework.Utility
{
    class ImmeUITimer
    {
        public EventHandler Tick;
        private DispatcherTimer _actualTimer = new DispatcherTimer();
        private TimeSpan _interval;

        public ImmeUITimer(TimeSpan interval, bool immediateStart = true)
        {
            _interval = interval;
            if (immediateStart)
            {
                _actualTimer.Interval = TimeSpan.FromSeconds(0.1);
            }
        }

        public void Start()
        {
            if (null == Tick)
            {
                return;
            }

            _actualTimer.Tick += _actualTimer_Tick;


            _actualTimer.Start();
        }

        void _actualTimer_Tick(object sender, EventArgs e)
        {
            if (_actualTimer.Interval < _interval)
            {
                _actualTimer.Interval = _interval;
            }

            if (null != Tick)
            {
                Tick(sender, e);
            }
        }

        public void Stop()
        {
            try
            {
                _actualTimer.Stop();
            }
            catch(Exception)
            {

            }
        }

    }
}
