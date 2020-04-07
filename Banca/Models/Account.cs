using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    [Serializable]
    public class Account : ISerializable
    {
        public static int Id { get; set; }
        public string CNP { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public static List<Account> Accounts = new List<Account>();
        public static List<Account> ListAccountsOfClient = new List<Account>();
        public Account(string CNP, string Number, decimal Balance, string Currency, string Type, string Time)
        {
            Id++;
            this.CNP = CNP;
            this.Number = Number;
            this.Balance = Balance;
            this.Currency = Currency;
            this.Type = Type;
            this.Time = Time;
        }
        public Account() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CNP", CNP);
            info.AddValue("Number", Number);
            info.AddValue("Balance", Balance);
            info.AddValue("Currency", Currency);
            info.AddValue("Type", Type);
            info.AddValue("Time", Time);
        }
        protected Account(SerializationInfo info, StreamingContext ctxt)
        {
            CNP = (string)info.GetValue("CNP", typeof(string));
            Number = (string)info.GetValue("Number", typeof(string));
            Balance = (decimal)info.GetValue("Balance", typeof(decimal));
            Currency = (string)info.GetValue("Currency", typeof(string));
            Type = (string)info.GetValue("Type", typeof(string));
            Time = (string)info.GetValue("Time", typeof(string));
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", CNP, Number, Balance, Currency, Type, Time);
        }       
    }
}