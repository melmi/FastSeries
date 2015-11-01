using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    static class Header
    {
        public static void WriteHeader(BinaryWriter writer, string[] tableDescriptions)
        {
            writer.Write((UInt16)tableDescriptions.Length);
            foreach (var item in tableDescriptions) writer.Write(item);
        }

        public static List<string> ReadHeader(BinaryReader reader)
        {
            UInt16 n = reader.ReadUInt16();
            List<string> result = new List<string>(n);
            for (int i = 0; i < n; i++) result.Add(reader.ReadString());
            return result;
        }
    }
}
