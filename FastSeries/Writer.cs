using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    public class Writer
    {
        public Stream Stream { get; private set; }
        public ReadOnlyCollection<string> TableDescriptions { get; private set; }
        BinaryWriter writer;

        public Writer(Stream stream)
        {
            Stream = stream;
            var reader = new BinaryReader(Stream);
            var tableDescreiptions = Header.ReadHeader(reader);
            TableDescriptions = new ReadOnlyCollection<string>(tableDescreiptions);
            writer = new BinaryWriter(Stream);
        }

        public Writer(string path)
            : this(File.Open(path, FileMode.Open))
        {
        }

        public void WriteItem(UInt16 tableId, TimeSpan time, Data value)
        {
            new Record{ TableID = tableId, Time = time, Values = value }.WriteToStream(writer);
        }

        public void Flush()
        {
            Stream.Flush();
        }

        public void Close()
        {
            Stream.Close();
        }
    }
}
