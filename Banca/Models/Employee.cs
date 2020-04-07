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
    public class Employee : ISerializable
    {
        public static int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkLocation { get; set; }
        public static List<Employee> Employees;
        public  static List<BankEmployee> BankEmployees;
        public Employee(string firstname, string lastname, string cnp, string phone, string email, string address, string workLocation)
        {
            Id++;
            FirstName = firstname;
            LastName = lastname;
            CNP = cnp;
            Phone = phone;
            Email = email;
            Address = address;
            WorkLocation = workLocation;
        }
        public Employee() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("First Name", FirstName);
            info.AddValue("Last Name", LastName);
            info.AddValue("CNP", CNP);
            info.AddValue("Phone Number", Phone);
            info.AddValue("Email", Email);
            info.AddValue("Address", Address);
            info.AddValue("Work Location", WorkLocation);
        }
        protected Employee(SerializationInfo info, StreamingContext ctxt)
        {
            FirstName = (string)info.GetValue("First Name", typeof(string));
            LastName = (string)info.GetValue("Last Name", typeof(string));
            CNP = (string)info.GetValue("CNP", typeof(string));
            Phone = (string)info.GetValue("Phone Number", typeof(string));
            Email = (string)info.GetValue("Email", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            WorkLocation = (string)info.GetValue("Work Location", typeof(string));
        }
        public override string ToString()
        {
            return string.Format("{0} {1} -- {2}", FirstName, LastName, WorkLocation);
        }
    }
}