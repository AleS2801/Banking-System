using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bank
{
    [Serializable]
    public class BankEmployee : Employee
    {
        public static List<BankEmployee> bankEmployees;
        public BankEmployee()
        {
        }
        public BankEmployee(Employee employee)
        {
            CNP = employee.CNP;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Email = employee.Email;
            Address = employee.Address;
            Phone = employee.Phone;
            WorkLocation = employee.WorkLocation;
        }

    }

}