using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
namespace FastSeries.Sample
{
    enum XSCADA
    {
        ALARM_LOG,
        DATATAG,
        DATATAG_LOG,
        DATATAG_STATLOG,
        DATA_REPOSITORY,
        MOBILE,
        SMS_LOG,
        SYSTEM,
        SYSTEM_LOG,
        USER_LOG
    }
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
                CTime = DateTime.Now.ToString(),
                Name = "Test",
                TYPE = 'A',
                VALUE_STR = "123.00",
                VALUE_NUM = 123.00,
                VALUE_RAW = "123.00"
            };
            
            for (int i = 0; i < 1000; i++)
            {
                writer.WriteItem((ushort)XSCADA.ALARM_LOG, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.DATATAG, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.DATATAG_LOG, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.DATATAG_STATLOG, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.DATA_REPOSITORY, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.MOBILE, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.SMS_LOG, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.SYSTEM, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.SYSTEM_LOG, DateTime.Now - start, data);
                writer.WriteItem((ushort)XSCADA.USER_LOG, DateTime.Now - start, data);
                Thread.Sleep(5);
            }
            writer.Flush();
            writer.Close();
            read();
            System.Diagnostics.Debug.WriteLine(st.ElapsedMilliseconds);
        }

        static void read()
        {
            Console.WriteLine(">>> Reading data.");

            var reader = new FastSeries.Reader("test.db");
            for(ushort i = 0; i<10; i++)
            {
                Console.WriteLine(reader.TableDescriptions[1]);
                Console.WriteLine("==========================");
                reader.Reset();
                var items = reader.ReadToEnd(i);
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
