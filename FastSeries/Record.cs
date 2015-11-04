using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    class Record
    {
        public UInt16 TableID { get; set; }
        public TimeSpan Time { get; set; }
        public float Value { get; set; }

        public static Record FromStream(BinaryReader reader)
        {
            var tableID = reader.ReadUInt16();
            var ticks = reader.ReadInt64();
            var time = new TimeSpan(ticks);
            var value = reader.ReadSingle();
            return new Record { Time = time, TableID = tableID, Value = value };
        }

        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write(TableID);
            writer.Write(Time.Ticks);
            writer.Write(Value);
        }
    }
}
