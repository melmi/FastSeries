using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    /// <summary>
    /// TODO: Make it async/await
    /// </summary>
    public class Writer
    {
        public Stream Stream { get; private set; }
        public ReadOnlyCollection<string> TableDescriptions { get; private set; }
        BinaryWriter writer;
        /// <summary>
        /// Create writer to writing data in DB.
        /// </summary>
        /// <param name="stream"></param>
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
        /// <summary>
        /// Write item to this writer. Params must be Initialized
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="time"></param>
        /// <param name="value"></param>
        public void WriteItem(Data value)
        {
             new  Record{ Values = value }.WriteToStream(writer);
        }
        /// <summary>
        /// Commit changes which you have wrote;
        /// </summary>
        public void Flush()
        {
            Stream.Flush();
        }
        /// <summary>
        /// Close DB
        /// </summary>
        public void Close()
        {
            Stream.Close();
        }
    }
}
