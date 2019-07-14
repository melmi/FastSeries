using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace FastSeries
{
    /// <summary>
    /// Create/Destroy read stream
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Reader's stream
        /// </summary>
        public Stream Stream { get;}
        /// <summary>
        /// Get current database's table descriptions
        /// </summary>
        public ReadOnlyCollection<string> TableDescriptions { get; private set; }
        BinaryReader reader;
        /// <summary>
        /// Reader's starting position
        /// </summary>
        long zeroPosition;

        /// <summary>
        /// Create reader by stream
        /// </summary>
        /// <param name="stream"></param>
        public Reader(Stream stream)
        {
            Stream = stream;
            reader = new BinaryReader(Stream);
            var tableDescreiptions = Header.ReadHeader(reader);
            zeroPosition = Stream.Position;
            TableDescriptions = new ReadOnlyCollection<string>(tableDescreiptions);
        }

        /// <summary>
        /// Get path and then return file that the path indicates
        /// </summary>
        /// <param name="path"></param>
        public Reader(string path)
            : this(File.Open(path, FileMode.Open))
        {
        }

        /// <summary>
        /// Move stream position to stream's starting point
        /// </summary>
        public void Reset()
        {
            Stream.Position = zeroPosition;
        }

        /// <summary>
        /// Read records from table
        /// </summary>
        /// <param name="tableId">Designate table by table id</param>
        /// <param name="n">Records to read</param>
        /// <returns></returns>
        public List<Tuple<TimeSpan, Data>> TryRead(UInt16 tableId, int n)
        {
            var result = new List<Tuple<TimeSpan, Data>>(n);
            while (Stream.Position != Stream.Length && n >= 0)
            {
                var rec = Record.ReadFromStream(reader);
                if (rec.Values.TableID == tableId)
                {
                    result.Add(Tuple.Create(rec.Values.Time, rec.Values));
                    --n;
                }
            }
            return result;
        }

        /// <summary>
        /// Read until table ends
        /// </summary>
        /// <param name="tableId">Designate table by table id</param>
        /// <returns></returns>
        public List<Tuple<TimeSpan, Data>> ReadToEnd(UInt16 tableId)
        {
            var result = new List<Tuple<TimeSpan, Data>>();
            while (Stream.Position != Stream.Length)
            {
                var rec = Record.ReadFromStream(reader);
                if (rec.Values.TableID == tableId)
                    result.Add(Tuple.Create(rec.Values.Time, rec.Values));
            }
            return result;
        }

        //TODO: DO IT
        public List<Tuple<TimeSpan, Data>> ReadByTime()
        {
            var result = new List<Tuple<TimeSpan, Data>>();
            
            return result;
        }
        public void Close()
        {
            Stream.Close();
        }
    }
}
