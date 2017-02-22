using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniBox.Framework.Data
{
    class DataSource_RandomGenerator : DataSource
    {
        private string _sourceSetting = "0-10000";
        private int _minValue = 0;
        private int _maxValue = 10000;
        Random _random = new Random();
        public override string SourceTypeName
        {
            get { return "Random generator"; }
        }

        public override String QueryData()
        {
            string value = _random.Next(_minValue, _maxValue).ToString();
            return value;
        }

        public override string SourceSetting
        {
            get
            {
                return _sourceSetting;
            }
            set
            {
                _sourceSetting = value;
                string[] parts = _sourceSetting.Split('-');
                if (parts.Length < 2)
                {
                    int.TryParse(parts[0].Trim(), out _minValue);
                }
                else
                {
                    int.TryParse(parts[0].Trim(), out _minValue);
                    int.TryParse(parts[1].Trim(), out _maxValue);
                }
            }
        }
    }
}
