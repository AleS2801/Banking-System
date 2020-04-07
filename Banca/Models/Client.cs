using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    [Serializable]
    public class Client : ISerializable
    {
        public static int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public static List<Client> Clients;
        //static List<Account> AccountList;
        public Client(string firstname, string lastname, string cnp, string phone, string email, string address)
        {
            Id++;
            FirstName = firstname;
            LastName = lastname;
            CNP = cnp;
            Phone = phone;
            Email = email;
            Address = address;
        }
        public Client() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("First Name", FirstName);
            info.AddValue("Last Name", LastName);
            info.AddValue("CNP", CNP);
            info.AddValue("Phone Number", Phone);
            info.AddValue("Email", Email);
            info.AddValue("Address", Address);
        }
        protected Client(SerializationInfo info, StreamingContext ctxt)
        {
            FirstName = (string)info.GetValue("First Name", typeof(string));
            LastName = (string)info.GetValue("Last Name", typeof(string));
            CNP = (string)info.GetValue("CNP", typeof(string));
            Phone = (string)info.GetValue("Phone Number", typeof(string));
            Email = (string)info.GetValue("Email", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", FirstName, LastName, CNP, Phone, Email, Address);
        }
    }
}