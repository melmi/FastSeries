using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
namespace FastSeries.Sample
{
    class Program
    {
        static void create()
        {
            Console.WriteLine(">>> Creating db.");
            FastSeries.Creator.Create("test.db", "temprature (centigrads)", "speed (m/s)");
        }
        static void write()
        {
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            Console.WriteLine(">>> Writing data.");

            var writer = new FastSeries.Writer("test.db");
            var start = DateTime.Now;
            Data data = new Data {
                TableID = 0,
                intField = 123,
                charsField= string.Format("{0:s}", DateTime.Now).ToCharArray(),
                charField = 'A',
                doubleField = 123.123,
                boolField = true
            };
            for (int i = 0; i < 1; i++)
            {
                data.Time = DateTime.Now - start;
                if (i %50 == 0)
                {
                    //data.Name += "1";
                    //data.VALUE_STR += "1";
                }
                writer.WriteItem( data);
            }

            writer.Flush();
            writer.Close();
            Debug.WriteLine(st.ElapsedTicks);
        }

        static void read()
        {
            Console.WriteLine(">>> Reading data.");

            var reader = new Reader("test.db");
            
            {
                Console.WriteLine(reader.TableDescriptions[1]);
                Console.WriteLine("==========================");
                reader.Reset();
                var items = reader.ReadToEnd(0);
                foreach (var item in items)
                    Console.WriteLine("{0}    {1}", item.Item1.ToString(), item.Item2.ToString());
            }
            reader.Close();
        }

        static void Main(string[] args)
        {
            create();
            write();
            read();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
