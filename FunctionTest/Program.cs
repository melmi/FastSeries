using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] asd = "hello, world!".ToCharArray();
            char[] fas = Enumerable.Repeat<char>('\0', 20).ToArray<char>();
            asd.CopyTo(fas, 0);
            DateTime date = DateTime.Now;
            for(int i = 0; i< 10000; i++)
            {
                date = date.AddHours(i);
                Console.WriteLine("---");
                Console.WriteLine(string.Format("{0:s}", date).ToCharArray());
            }
            Console.ReadKey();
        }
    }
}
