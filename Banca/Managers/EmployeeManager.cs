using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    class EmployeeManager:Employee
    {
        //Create and store the employee.
        public void CreateEmployee()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - New Employee");
            string cnp = Utils.GetCNP();
            if (Utils.EmployeeExists(cnp)) Console.WriteLine("Employee already exists!");
            else
            {
                Console.Write($"First Name: ");
                string firstname = Console.ReadLine();
                Console.Write($"Last Name: ");
                string lastname = Console.ReadLine();
                Console.Write($"Phone Number: ");
                string phone = Console.ReadLine();
                Console.Write($"Address: ");
                string address = Console.ReadLine();
                Console.Write($"Email: ");
                string email = Console.ReadLine();
                string worklocation;
                do
                {
                    Console.Write($"Work Location: ");
                    worklocation = Console.ReadLine().ToUpperInvariant();
                    if (Utils.BankExists(worklocation))
                    {
                        BankEmployee.bankEmployees = Utils.Read<BankEmployee>($"../../Bank{worklocation}.xml");
                        Console.WriteLine("--Work user account--");
                        Console.Write("Username: ");
                        string user = Console.ReadLine();
                        Console.Write("Password: ");
                        string password = Console.ReadLine();
                        Employee myEmployee = new Employee(firstname, lastname, cnp, phone, email, address, worklocation);
                        BankEmployee be = new BankEmployee(myEmployee);
                        EmployeeUsers employeeUser = new EmployeeUsers(user, password, cnp, worklocation);
                        Console.WriteLine("Adding Employee...");
                        Employees.Add(myEmployee);
                        Utils.Store<Employee>("../../EmployeeList.xml", Employees);
                        Thread.Sleep(700);
                        Console.WriteLine("Employee added!");
                        BankEmployee.bankEmployees.Add(be);
                        EmployeeUsers.Users.Add(employeeUser);
                        Utils.Store<BankEmployee>($"../../Bank{worklocation}.xml", BankEmployee.bankEmployees);
                        Utils.Store<EmployeeUsers>("../../EmployeeUsers.xml", EmployeeUsers.Users);
                    }
                    else Console.WriteLine("This bank doesn't exist");
                } while (Utils.BankExists(worklocation) == false);
            }
        }

        //Search the employee.
        internal void SearchEmployee()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - Search Employee");
            bool ok = false;
            string CNP = Utils.GetCNP();
            Employees = Utils.Read<Employee>("../../EmployeeList.xml");
            foreach (Employee e in Employees)
            {
                if (e.CNP == CNP)
                {
                    Console.WriteLine($"Employee name is: {e.FirstName} {e.LastName}");
                    ok = true;
                    break;
                }
            }
            if (!ok) Console.WriteLine("Employee not found!");
        }

        //Edit the employee information.
        internal void EditEmployee()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - Edit Employee");
            string CNP = Utils.GetCNP();
            if (Utils.EmployeeExists(CNP))
            {
                Employees = Utils.Read<Employee>("../../EmployeeList.xml");
                foreach (Employee e in Employees)
                {
                    if (CNP == e.CNP)
                    {
                        Console.WriteLine("\n\tFirst name: " + e.FirstName + "\n\tLast Name: " + e.LastName + "\n\tPhone number: " + e.Phone + "\n\tEmail: " + e.Email + "\n\tAddress: " + e.Address + "\n\tWork Location: " + e.WorkLocation + "\n");
                        Console.WriteLine("Leave empty if you don't want to change the specified information.");
                        Console.Write("\tFirst name: ");
                        string fn = Console.ReadLine();
                        if (fn != "") e.FirstName = fn;
                        Console.Write("\tLast name: ");
                        string ln = Console.ReadLine();
                        if (ln != "") e.LastName = ln;
                        Console.Write("\tPhone: ");
                        string p = Console.ReadLine();
                        if (p != "") e.Phone = p;
                        Console.Write("\tEmail: ");
                        string em = Console.ReadLine();
                        if (em != "") e.Email = em;
                        Console.Write("\tAddress: ");
                        string a = Console.ReadLine();
                        if (a != "") e.Address = a;
                        //Console.Write("\tWork Location: ");
                        //string wl = Console.ReadLine();
                        //if (wl != "") e.WorkLocation = wl;
                        break;
                    }
                }
                Utils.Store<Employee>("../../EmployeeList.xml", Employees);
            }
            else Console.WriteLine("Employee not found!");
        }

        //Remove an employee and all related data.
        internal void RemoveEmployee()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - Remove Employee");
            string CNP = Utils.GetCNP();
            if (Utils.EmployeeExists(CNP))
            {
                Employees = Utils.Read<Employee>("../../EmployeeList.xml");
                foreach (Employee c in Employees)
                {
                    if (CNP == c.CNP)
                    {
                        Console.WriteLine("Removing Employee...");
                        Employees.Remove(c);
                        Utils.Store<Employee>("../../EmployeeList.xml", Employees);
                        Thread.Sleep(700);
                        Console.WriteLine("Employee removed!");
                        break;
                    }
                }
                EmployeeUsers.Users = Utils.Read<EmployeeUsers>("../../EmployeeUsers.xml");
                EmployeeUsers.Users.RemoveAll(employee => employee.CNP == CNP);
                Utils.Store<EmployeeUsers>("../../EmployeeUsers.xml", EmployeeUsers.Users);
                BankEmployee.BankEmployees = Utils.Read<BankEmployee>($"../../Bank{Utils.workingBank.Location.ToUpperInvariant()}.xml");
                BankEmployee.BankEmployees.RemoveAll(employee => employee.CNP == CNP);
                Utils.Store<BankEmployee> ($"../../Bank{Utils.workingBank.Location.ToUpperInvariant()}.xml", BankEmployee.BankEmployees);

            }
            else Console.WriteLine("Employee doesn't exist!");
        }

        //Display the list of employees.
        public void ShowList()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - List of Employees");
            Employees = Utils.Read<Employee>("../../EmployeeList.xml");
            if (Employees.Count == 0) Console.WriteLine("There are no Employees");
            foreach (Employee c in Employees)
            {
                Thread.Sleep(20);
                Console.WriteLine(c.ToString());
            }
        }
    }
}
