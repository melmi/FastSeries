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
                ID = 1,
                CTime = DateTime.Now.ToString()+'\0',
                Name = "Test",
                TYPE = 'A',
                VALUE_STR = "123.00",
                VALUE_NUM = 123.00,
                VALUE_RAW = "123.00"
            };
            for (int i = 0; i < 1000; i++)
            {
                writer.WriteItem(0, DateTime.Now - start, data);
                Thread.Sleep(5);
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
            ThreadStart start = new ThreadStart(write);
            Thread th = new Thread(start);
            th.Start();
            th.Join();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
    }
}
