using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bank
{
    //nefolosita
    [Serializable]
    public class Address : ISerializable
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string Apartament { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public Address()
        {

        }
        public Address(string county, string city, string street, string number, string block, string floor, string apartament, string zip)
        {
            County = county;
            City = city;
            Street = street;
            Number = number;
            Block = block;
            Floor = floor;
            Apartament = apartament;
            Zip = zip;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Assign key value pair for your data
            info.AddValue("County", County);
            info.AddValue("City", City);
            info.AddValue("Street", Street);
            info.AddValue("Number", Number);
            info.AddValue("Block", Block);
            info.AddValue("Floor", Floor);
            info.AddValue("Apartament", Apartament);
            info.AddValue("Zip", Zip);
        }

        // The deserialize function (Removes Object Data from File)
        protected Address(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the properties
            County = (string)info.GetValue("County", typeof(string));
            City = (string)info.GetValue("City", typeof(string));
            Street = (string)info.GetValue("Street", typeof(string));
            Number = (string)info.GetValue("Number", typeof(string));
            Block = (string)info.GetValue("Block", typeof(string));
            Floor = (string)info.GetValue("Floor", typeof(string));
            Apartament = (string)info.GetValue("Apartament", typeof(string));
            Zip = (string)info.GetValue("Zip", typeof(string));
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                County, City, Street, Number, Block, Floor, Apartament, Zip);
        }
    }
}
