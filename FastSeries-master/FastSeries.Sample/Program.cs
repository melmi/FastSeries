using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(">>> Writing data.");

            var writer = new FastSeries.Writer("test.db");
            var start = DateTime.Now;

            for (int i = 0; i < 7; ++i)
                writer.WriteItem(0, DateTime.Now - start, i);

            for (int i = 0; i < 9; ++i)
                writer.WriteItem(1, DateTime.Now - start, i);

            writer.Flush();

            for (int i = 0; i < 6; ++i)
                writer.WriteItem(0, DateTime.Now - start, i);

            for (int i = 0; i < 4; ++i)
                writer.WriteItem(1, DateTime.Now - start, i);

            writer.Close();
        }

        static void read()
        {
            Console.WriteLine(">>> Reading data.");

            var reader = new FastSeries.Reader("test.db");

            {
                Console.WriteLine(reader.TableDescriptions[0]);
                Console.WriteLine("==========================");
                for (int t = 0; t < 3; ++t)
                {
                    var items = reader.TryRead(0, 10);
                    foreach (var item in items)
                        Console.WriteLine("{0}    {1}", item.Item1, item.Item2);
                }
            }

            {
                Console.WriteLine(reader.TableDescriptions[1]);
                Console.WriteLine("==========================");
                reader.Reset();
                var items = reader.ReadToEnd(1);
                foreach (var item in items)
                    Console.WriteLine("{0}    {1}", item.Item1, item.Item2);
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
