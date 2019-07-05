using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FastSeries
{
    public struct Data
    {
        public const int MaxChars = 64;
        public const int MaxDataBytes = 64;
        char[] tempStr;
        byte[] tempBytes;
        public UInt32 TableID { get; set; }
        public TimeSpan Time { get; set; }
        public char[] NameField {
            get
            { return tempStr; }
            set
            {
                if (value.Length > MaxChars)
                    throw new ArgumentOutOfRangeException
                        ("Overflow Warning\nIf you want to extend name string, then modify FastSeries.dll\n" + "Value: " + value.Length);
                else
                {
                    tempStr = Enumerable.Repeat<char>('\0', MaxChars).ToArray<char>(); ;
                    value.CopyTo(tempStr, 0);
                }
            }
        }
        public char TypeField { get; set; }
        public byte[] DataField {
            get
                { return tempBytes; }
            set
            {
                if (value.Length > MaxDataBytes)
                    throw new ArgumentOutOfRangeException
                        ("Overflow Warning\nIf you want to extend data size, then modify FastSeries.dll\n" + "Value bytes: " + value.Length);
                else
                {
                    tempBytes = new byte[MaxDataBytes];
                    value.CopyTo(tempBytes, 0);
                }
            }
        }
        
        public override string ToString()
        {
            StringBuilder tempStr = new StringBuilder();
            for (int i = 0; i < NameField.Length; i++)
                if (NameField[i] != '\0') tempStr.Insert(i, NameField[i]);
                else break;
            dynamic tempBytes;
            switch(TypeField)
            {
                case 'I':
                    tempBytes = BitConverter.ToInt32(DataField, 0);
                    break;
                case 'D':
                    tempBytes = BitConverter.ToDouble(DataField, 0);
                    break;
                case 'S':
                    tempBytes = Encoding.UTF8.GetString(DataField);
                    break;
                case 'B':
                    tempBytes = BitConverter.ToInt32(DataField, 0);
                    break;
                default:
                    tempBytes = Encoding.Default.GetString(DataField);
                    break;
            }
            
            return "/TableID: " + TableID
                + "  /Inserted Time: " + Time.ToString()
                + "  /Name: " + tempStr 
                + "  /Type: " + TypeField
                + "  /Data: " + tempBytes;
        }
        public static int Size
        {
            get
            {
                return sizeof(UInt32) 
                    + sizeof(Int64) 
                    + sizeof(char) * MaxChars 
                    + sizeof(byte) * MaxDataBytes;
            }
        }
    }
}
