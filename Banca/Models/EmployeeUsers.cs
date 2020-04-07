using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Bank
{
    [Serializable]
    public class EmployeeUsers:ISerializable
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CNP { get; set; }
        public string WorkLocation { get; set; }

        public static List<EmployeeUsers> Users;

        public EmployeeUsers() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Username", Username);
            info.AddValue("Password", Password);
            info.AddValue("CNP", CNP);
            info.AddValue("WorkLocation", WorkLocation);
        }
        protected EmployeeUsers(SerializationInfo info, StreamingContext ctxt)
        {
            Username = (string)info.GetValue("Username", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
            CNP = (string)info.GetValue("CNP", typeof(string));
            WorkLocation = (string)info.GetValue("WorkLocation", typeof(string));
        }

        public EmployeeUsers(string user, string password, string cnp, string worklocation)
        {
            Username = user;
            Password = password;
            CNP = cnp;
            WorkLocation = worklocation; 
        }
    }
}
