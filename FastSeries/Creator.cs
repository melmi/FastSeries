using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    /// <summary>
    /// Choose open or create database file
    /// </summary>
    public static class Creator
    {
        /// <summary>
        /// Create new database file
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="tableDescriptions"></param>
        public static void Create(Stream stream, params string[] tableDescriptions)
        {
            var writer = new BinaryWriter(stream);
            Header.WriteHeader(writer, tableDescriptions);
        }

        public static void Create(string path, params string[] tableDescriptions)
        {
            using (var file = File.Open(path, FileMode.Append, FileAccess.Write))
            {
                if (file.Position == 0)
                    Create(file, tableDescriptions);
            }
        }
    }
}
