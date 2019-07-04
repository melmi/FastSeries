using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace FastSeries
{
    /// <summary>
    /// Actually does write/read from database file
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Data struct instance
        /// </summary>
        public Data Values;
        /// <summary>
        /// Read data from reader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Record ReadFromStream(BinaryReader reader)
        {
            Debug.WriteLine(GetEndPos(reader));
            Debug.WriteLine(Data.Size);
            Data values = new Data {
                TableID = reader.ReadUInt32(),
                Time = new TimeSpan(reader.ReadInt64()),
                NameField = reader.ReadChars(Data.MaxChars),
                TypeField = reader.ReadChar(),
                DataField = reader.ReadBytes(Data.MaxDataBytes)
        };
            return new Record { Values = values};
        }
        //TODO: DO IT
        public static void MoveStream(BinaryReader reader, int curser)
        {
            reader.BaseStream.Seek(Data.Size*curser, SeekOrigin.Begin);
        }
        //TODO: DO IT
        public static long GetEndPos(BinaryReader reader)
        {
            return reader.BaseStream.Length;
        }
        /// <summary>
        /// Write data through BinaryWriter
        /// </summary>
        /// <param name="writer"></param>
        public void  WriteToStream(BinaryWriter writer)
        {
            // buffer[curser].TableID = (UInt32)Values.TableID;
            //buffer[curser].Time = (Int64)Values.Time;
            writer.Write((UInt32)Values.TableID);
            writer.Write((Int64)Values.Time.Ticks);
            writer.Write((char[])Values.NameField);
            writer.Write((char)Values.TypeField);
            writer.Write((byte[])Values.DataField);
            /*lock (writer)
            {
                BinaryWrite(writer);
            }*/
        }
        /*
        private async Task BinaryWrite(BinaryWriter writer)
        {
            await Task.Run(() =>
            {
                Debug.Write(1);
                writer.Write((UInt32)Values.TableID);
                Debug.Write(2);
                writer.Write((Int64)Values.Time.Ticks);
                Debug.Write(3);
                writer.Write((Int32)Values.ID);
                Debug.Write(4);
                writer.Write((string)Values.CTime);
                Debug.Write(5);
                writer.Write((string)Values.Name);
                Debug.Write(6);
                writer.Write((char)Values.TYPE);
                Debug.Write(7);
                writer.Write((string)Values.VALUE_STR);
                Debug.Write(8);
                writer.Write((double)Values.VALUE_NUM);
                Debug.Write(9);
                writer.Write((string)Values.VALUE_RAW);
            });
        }
        */
    }
    
}
