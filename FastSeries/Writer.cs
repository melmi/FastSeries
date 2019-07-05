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
    /// Create/Destroy write stream
    /// </summary>
    public class Writer
    {
        /// <summary>
        /// Writer's stream
        /// </summary>
        public Stream Stream { get; }
        /// <summary>
        /// Get current database's table descriptions
        /// </summary>
        public ReadOnlyCollection<string> TableDescriptions { get; }
        BinaryWriter writer;
        /// <summary>
        /// Create writer to writing data in DB.
        /// </summary>
        /// <param name="stream"></param>
        public Writer(Stream stream, string path)
        {
            Stream = stream;
            var reader = new BinaryReader(Stream);
            var tableDescreiptions = Header.ReadHeader(reader);
            TableDescriptions = new ReadOnlyCollection<string>(tableDescreiptions);
            stream.Close();
            Stream = File.Open(path, FileMode.Append);
            writer = new BinaryWriter(Stream);
        }
        /// <summary>
        /// Open database file to write item
        /// </summary>
        /// <param name="path"></param>
        public Writer(string path)
            : this(File.Open(path, FileMode.Open), path)
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
