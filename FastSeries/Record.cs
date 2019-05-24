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

        
        public Data Values;
        public static Record FromStream(BinaryReader reader)
        {
           System.Diagnostics.Debug.WriteLine("_");
            System.Diagnostics.Debug.WriteLine(reader.BaseStream.Length);
            var tableID = reader.ReadUInt16();
            var ticks = reader.ReadInt64();
            var time = new TimeSpan(ticks);
            Data values = new Data { };
            values.ID = reader.ReadInt32();
            
            values.CTime = reader.ReadString();
            values.Name = reader.ReadString();
            values.TYPE = reader.ReadChar();
            values.VALUE_STR = reader.ReadString();
            values.VALUE_NUM = reader.ReadDouble();
            values.VALUE_RAW = reader.ReadString() ;
           
            return new Record { Time = time, TableID = tableID, Values = values};
        }

        public void WriteToStream(BinaryWriter writer)
        {

            writer.Write(TableID);
            writer.Write(Time.Ticks);
            writer.Write(Values.ID);
            writer.Write(Values.CTime);
            writer.Write(Values.Name);
            writer.Write(Values.TYPE);
            writer.Write(Values.VALUE_STR);
            writer.Write(Values.VALUE_NUM);
            writer.Write(Values.VALUE_RAW);
        }
    }
    
}
