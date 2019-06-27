﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace FastSeries
{
    class Record
    {
        Data[] buffer = new Data[10];
        int curser = 0;
        public Data Values;
        public static Record FromStream(BinaryReader reader)
        {
            Data values = new Data {
                TableID = reader.ReadUInt32(),
                Time = new TimeSpan(reader.ReadInt64()),
                intField = reader.ReadInt32(),
                charsField = reader.ReadChars(21),
                charField = reader.ReadChar(),
                doubleField = reader.ReadDouble(),
                boolField = reader.ReadBoolean()
        };
            return new Record { Values = values};
        }
        public static void MoveStream(BinaryReader reader)
        {

        }

        public async void  WriteToStream(BinaryWriter writer)
        {
            // buffer[curser].TableID = (UInt32)Values.TableID;
            //buffer[curser].Time = (Int64)Values.Time;
            writer.Write((UInt32)Values.TableID);
            writer.Write((Int64)Values.Time.Ticks);
            writer.Write((Int32)Values.intField);
            writer.Write((char[])Values.charsField);
            writer.Write((char)Values.charField);
            writer.Write((double)Values.doubleField);
            writer.Write((bool)Values.boolField);
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
