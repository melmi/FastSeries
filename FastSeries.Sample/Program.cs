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
                ID = 1,
                CTime = string.Format("{0:s}", DateTime.Now),
                Name = "Test",
                TYPE = 'A',
                VALUE_STR = "123.00",
                VALUE_NUM = 123.00,
                VALUE_RAW = "123.00"
            };
            for (int i = 0; i < 5; i++)
            {
                data.Time = DateTime.Now - start;
                writer.WriteItem( data);
                Thread.Sleep(100);
            }

            writer.Flush();
            writer.Close();
            read();
            System.Diagnostics.Debug.WriteLine(st.ElapsedTicks);
        }

        static void read()
        {
            Console.WriteLine(">>> Reading data.");

            var reader = new FastSeries.Reader("test.db");
            
            {
                Console.WriteLine(reader.TableDescriptions[1]);
                Console.WriteLine("==========================");
                reader.Reset();
                var items = reader.ReadToEnd(0);
                foreach (var item in items)
                    Console.WriteLine("{0}    {1}", item.Item1, item.Item2.ToString());
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
