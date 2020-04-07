using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    [Serializable]
    public class Bank:ISerializable
    {
        public string Location { get; set; }
        public decimal BankMoney { get; set; }
        public static decimal Money { get; set; }
        public static string FullName = "Commercial Bank Ale";
        public static string name = "CBA";
        internal static List<Bank> Banks;
        public Bank()
        {

        }
        public Bank(string location, decimal money)
        {
            Location = location;
            BankMoney = money;
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Location", Location);
            info.AddValue("Money", BankMoney);
        }
        protected Bank(SerializationInfo info, StreamingContext ctxt)
        {
            Location = (string)info.GetValue("Location", typeof(string));
            BankMoney = (decimal)info.GetValue("Money", typeof(decimal));
        }
        public override string ToString()
        {
            //return string.Format("{0}", Money);
            return string.Format("{0} {1}",Location, BankMoney);
        }
        
    }
}
