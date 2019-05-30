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
            values.VALUE_RAW = reader.ReadString();
           
            return new Record { Time = time, TableID = tableID, Values = values};
        }

        public async void WriteToStream(BinaryWriter writer)
        {
            using(writer) {
                
                await BinaryWrite(writer ,TableID);
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
        private static Task<UInt16> BinaryWrite(BinaryWriter writer, UInt16 value)
        {
            Task task = new Task(
            () => writer.Write(value));
            return task.Start();
        }
        private static int BinaryWrite(BinaryWriter writer, Int64 value)
        {
            writer.Write(value);
            return 0;
        }
        private static int BinaryWrite(BinaryWriter writer, Int32 value)
        {
            writer.Write(value);
            return 0;
        }
        private static int BinaryWrite(BinaryWriter writer, string value)
        {
            writer.Write(value);
            return 0;
        }
        private static int BinaryWrite(BinaryWriter writer, char value)
        {
            writer.Write(value);
            return 0;
        }
        private static int BinaryWrite(BinaryWriter writer, double value)
        {
            writer.Write(value);
            return 0;
        }
    }
    
}
