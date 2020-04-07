using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Bank
{
    [Serializable]
    public class Transaction : ISerializable
    {
        //public string CNP { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public static List<Transaction> Transactions = Utils.GetListOfTransactions();
        public Transaction(/*string CNP,*/ string Number, decimal Balance, decimal Amount, string Type)
        {
            //this.CNP = CNP;
            this.Number = Number;
            this.Balance = Balance;
            this.Amount = Amount;
            this.Type = Type;
        }
        public Transaction() { }        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("CNP", CNP);
            info.AddValue("Number", Number);
            info.AddValue("Type", Type);
            info.AddValue("Balance", Balance);
            info.AddValue("Amount", Amount);
            //info.AddValue("Currency", Currency);
        }
        protected Transaction(SerializationInfo info, StreamingContext ctxt)
        {
            //CNP = (string)info.GetValue("CNP", typeof(string));
            Number = (string)info.GetValue("Number", typeof(string));
            Type = (string)info.GetValue("Type", typeof(string));
            Balance = (decimal)info.GetValue("Balance", typeof(decimal));
            Amount = (decimal)info.GetValue("Amount", typeof(decimal));
            //Currency = (string)info.GetValue("Currency", typeof(string));
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", /*CNP,*/ Number, Type, Amount, Currency, Balance);
        }
    }
}