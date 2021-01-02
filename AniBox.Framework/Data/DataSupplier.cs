using AniBox.Framework.AniEventArgs;
using AniBox.Framework.Data;
using AniBox.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;

namespace AniBox.Framework.Data
{
    public class DataSupplier
    {
        ImmeUITimer _timer;

        private XmlNodeList _xmlnodeItems;

        private List<String> _strItems;

        private int _curItemIdx = 0;

        public DataSupplier()
        {
        }

        public void StartUpdate()
        {
            if (UpdateInterval < 1)
            {
                return;
            }

            UpdateDataItems();
            _timer = new ImmeUITimer(TimeSpan.FromSeconds(UpdateInterval));
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void UpdateDataItems()
        {
            Object obj = Source.GetUpdateObject();
            if (obj is XmlNodeList)
            {
                _xmlnodeItems = obj as XmlNodeList;
            }
            else if (obj is List<string>)
            {
                _strItems = obj as List<string>;
            }
        }

        private Object GetNextItem()
        {
            Object item = null;
            if (null != _xmlnodeItems)
            {
                if (_curItemIdx < _xmlnodeItems.Count)
                {
                    item = _xmlnodeItems[_curItemIdx];
                }
                else
                {
                    UpdateDataItems();
                }
            }
            else if (null != _strItems)
            {
                if (_curItemIdx < _strItems.Count)
                {
                    item = _strItems[_curItemIdx];
                }
                else
                {
                    UpdateDataItems();
                }
            }

            return null;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            Object item = GetNextItem();
            if (null != item)
            {
                foreach (var v in Fields)
                {
                    if (null != v.ResultListener)
                    {
                        DataFieldProcessArgs arg = new DataFieldProcessArgs();
                        arg.Field = v;
                        arg.SourceInput = item;
                        v.ResultListener(this, arg);
                    }
                }
            }
        }

        public string Name { get; set; }

        public Int32 UpdateInterval { get; set; }

        public DataSource Source { get; set; }

        public List<FieldEntry> Fields { get; set; }
    }
}
