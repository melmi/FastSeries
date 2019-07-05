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
                Time = new TimeSpan(DateTime.Now.Ticks),
                NameField = "hello, world!!!!!".ToCharArray(),
                TypeField = 'I',
                DataField = BitConverter.GetBytes(345678901)
            };
            writer.WriteItem(data);
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
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine("{0}    {1}", new DateTime(items[i].Item1.Ticks).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), items[i].Item2.ToString());
                }
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
