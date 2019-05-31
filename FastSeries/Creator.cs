using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    public static class Creator
    {
        public static void Create(Stream stream, params string[] tableDescriptions)
        {
            var writer = new BinaryWriter(stream);
            Header.WriteHeader(writer, tableDescriptions);
        }
        public static void Create(Stream stream)
        {
            var writer = new BinaryWriter(stream);
        }
        public static void Create(string fileName, params string[] tableDescriptions)
        {
            using (var file = File.Open(fileName, FileMode.Append, FileAccess.Write))
            {
                if (file.Position != 0) Create(file);
                else Create(file, tableDescriptions);
            }
                
           // using (var file2 = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                //Create(file2, tableDescriptions);
            }
        }
    }
}
