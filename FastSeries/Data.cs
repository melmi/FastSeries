using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    public struct Data
    {
        public UInt32 TableID { get; set; }
        public TimeSpan Time { get; set; }
        public Int32 ID { get; set; }
        public string CTime { get; set; }
        public string Name { get; set; }
        public char TYPE { get; set; }
        public string VALUE_STR { get; set; }
        public double VALUE_NUM { get; set; }
        public string VALUE_RAW { get; set; }

        public override string ToString()
        {
            return "/ID: " + ID + "/CTime: " + CTime + "/Name: " + Name + "/Type: " + TYPE + "/VALUE_STR: " + VALUE_STR + "/VALUE_NUM: " + VALUE_NUM + "/VALUE_RAW: " + VALUE_RAW;
        }
    }
}
