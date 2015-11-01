using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    public class Reader
    {
        public Stream Stream { get; private set; }
        public ReadOnlyCollection<string> TableDescriptions { get; private set; }
        BinaryReader reader;
        long zeroPosition;

        public Reader(Stream stream)
        {
            Stream = stream;
            reader = new BinaryReader(Stream);
            var tableDescreiptions = Header.ReadHeader(reader);
            zeroPosition = Stream.Position;
            TableDescriptions = new ReadOnlyCollection<string>(tableDescreiptions);
        }

        public Reader(string path)
            : this(File.Open(path, FileMode.Open))
        {
        }

        public void Reset()
        {
            Stream.Position = zeroPosition;
        }

        public List<Tuple<DateTime, float>> TryRead(UInt16 tableId, int n)
        {
            var result = new List<Tuple<DateTime, float>>(n);
            while (Stream.Position != Stream.Length && n >= 0)
            {
                var rec = Record.FromStream(reader);
                if (rec.TableID == tableId)
                {
                    result.Add(Tuple.Create(rec.Time, rec.Value));
                    --n;
                }
            }
            return result;
        }

        public List<Tuple<DateTime, float>> ReadToEnd(UInt16 tableId)
        {
            var result = new List<Tuple<DateTime, float>>();
            while (Stream.Position != Stream.Length)
            {
                var rec = Record.FromStream(reader);
                if (rec.TableID == tableId)
                    result.Add(Tuple.Create(rec.Time, rec.Value));
            }
            return result;
        }

        public void Close()
        {
            Stream.Close();
        }
    }
}
