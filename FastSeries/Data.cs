using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSeries
{
    public struct Data
    {
        static char[] tempStr;
        public UInt32 TableID { get; set; }
        public TimeSpan Time { get; set; }
        public Int32 intField { get; set; }
        public char[] charsField {
            get
            {
                return tempStr; }
            set
            {
                //Console.WriteLine(value);
                if (value.Length > 21)
                    throw new ArgumentOutOfRangeException
                        ("CTime Value is over 21 chars\n" + "Value: " + value.Length);
                else
                {
                    tempStr = Enumerable.Repeat<char>('\0', 21).ToArray<char>(); ;
                    value.CopyTo(tempStr, 0);
                }
            }
        }
        public char charField { get; set; }
        public double doubleField { get; set; }
        public bool boolField{ get; set; }

        public override string ToString()
        {
            return "/intField: " + intField + "  /charsField: " + charsField + "  /charField: " + charField + "  /doubleField: " + doubleField + "  /boolField: " + boolField;
        }
    }
}
