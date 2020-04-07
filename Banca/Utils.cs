using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    class Utils
    {
        public static Bank workingBank;

        //Citirea din fisier -- Liste
        public static List<T> Read<T>(string filename)
        {
            List<T> list;
            XmlSerializer read = new XmlSerializer(typeof(List<T>));
            using (FileStream reader = File.OpenRead(filename))
            {
                list = (List<T>)read.Deserialize(reader);
                return list;
            }
        }

        //Scrierea in fisier -- Liste
        public static void Store<T>(string filename,List<T> list) 
        {
            using (Stream writer = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer write = new XmlSerializer(typeof(List<T>));
                write.Serialize(writer, list);
            }
        }

        //Lista de tranzactii -- din fisier
        public static List<Transaction> GetListOfTransactions()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<Transaction>));
            using (FileStream reader = File.OpenRead("../../Transactions.xml"))
            {
                Transaction.Transactions = (List<Transaction>)read.Deserialize(reader);
            }
            return Transaction.Transactions;
        }

        //Lista de conturi -- din fisier
        public static List<Account> GetListOfAccounts()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<Account>));
            using (FileStream reader = File.OpenRead("../../AccountList.xml"))
            {
                Account.Accounts = (List<Account>)read.Deserialize(reader);
            }
            return Account.Accounts;
        }

        //Lista de angajati pt fiecare banca
        public static List<BankEmployee> GetListOfBankEmployee()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<BankEmployee>));
            using (FileStream reader = File.OpenRead($"../../Bank{workingBank}.xml"))
            {
                BankEmployee.BankEmployees = (List<BankEmployee>)read.Deserialize(reader);
            }
            return BankEmployee.BankEmployees;

        }

        //Lista de angajati -- din fisier
        public static List<Employee> GetListOfEmployees()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<Employee>));
            using (FileStream reader = File.OpenRead("../../EmployeeList.xml"))
            {
                Employee.Employees = (List<Employee>)read.Deserialize(reader);
            }
            return Employee.Employees;
        }

        //Lista de clienti -- din fisier
        public static List<Client> GetListOfClients()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<Client>));
            using (FileStream reader = File.OpenRead("../../ClientList.xml"))
            {
                Client.Clients = (List<Client>)read.Deserialize(reader);
            }
            return Client.Clients;
        }

        //Daca exista banca
        public static bool BankExists(string location)
        {
            Bank.Banks = Utils.Read<Bank>("../../BankList.xml");
            bool ok = false;
            foreach (Bank c in Bank.Banks)
                if (location == c.Location) ok = true;
            return ok;
        }

        //Daca exista clientul
        public static bool ClientExists(string CNP)
        {
            Client.Clients = Utils.Read<Client>("../../ClientList.xml");
            bool ok = false;
            foreach (Client c in Client.Clients)
                if (CNP == c.CNP) ok = true;
            return ok;
        }

        //Daca exista angajatul
        public static bool EmployeeExists(string CNP)
        {
            XmlSerializer read = new XmlSerializer(typeof(List<Employee>));
            using (FileStream reader = File.OpenRead("../../EmployeeList.xml"))
            {
                Employee.Employees = (List<Employee>)read.Deserialize(reader);
            }
            bool ok = false;
            foreach (Employee c in Employee.Employees)
                if (CNP == c.CNP) ok = true;
            return ok;
        }

        //CNP input validation.
        public static string GetCNP()
        {
            bool ok = false;
            string CNP;
            do
            {
                Console.Write("CNP: ");
                CNP = Console.ReadLine();
                if (CNP.Length < 13 || CNP.Length > 13)
                {
                    Console.WriteLine("CNP must have 13 characters");
                    ok = false;
                }
                else
                {
                    if (CNP.Length == 13)
                    {
                        ok = long.TryParse(CNP, out _);
                        if (ok==false) Console.WriteLine("CNP must have only numbers!");
                    }
                }
            } while (ok==false);
            return CNP;
        }

        //Money input validation.
        public static decimal Input()
        {
            decimal input;
            bool b;
            do
            {
                b = decimal.TryParse(Console.ReadLine(), out input);
                if (b == false) Console.Write("Something went wrong! Please reenter: ");
            } while (b == false);
            return input;
        }

        //transaction validations.
        public static bool Validation(string accounttype, decimal balance, decimal amount)
        {
            bool ok = false;
            if (workingBank.BankMoney < amount) 
            {
                ok = false;
                Console.WriteLine("there is a problem at the bank.");
                return ok;
            }
            else if (accounttype.ToUpperInvariant() == "CURRENT")
            {
                if (balance >= amount)
                {
                    ok = true;
                    Console.WriteLine("transaction accepted!");
                    return ok;
                }
                else
                {
                    Console.WriteLine("insufficient funds!");
                    return ok;
                }
            }
            else if (accounttype.ToUpperInvariant() == "DEPOSIT")
            {
                if (balance >= amount)  // && time > timelimit
                {
                    //ok = true;
                    Console.WriteLine("You have to wait until you can get your money!");
                    return ok;
                }
                else
                {
                    Console.WriteLine("insufficient funds!");
                    return ok;
                }

            }
            return ok;
        }

        //LogIn
        internal static void LogIn()
        {
            int i = 0;
            foreach (Bank bank in GetListOfBanks())
            {
                Thread.Sleep(200);
                Console.WriteLine($"Bank {++i}: {bank.Location}");
            }
            bool ok = false;
            Thread.Sleep(200);
            workingBank = BankManager.SelectBank();
            do
            {
                Thread.Sleep(500);
                Console.Write("Username: ");
                string user = Console.ReadLine();
                Thread.Sleep(500);
                Console.Write("Password: ");
                string password = Console.ReadLine();
                string account = $"{user} {password}";
                foreach (EmployeeUsers u in GetListOfUsers())
                {
                    if(user == u.Username && password == u.Password)
                    {
                        if (u.WorkLocation.ToUpperInvariant() == workingBank.Location.ToUpperInvariant())
                        {
                            ok = true;
                        }
                        else continue;
                    }
                }

                if (ok==false)
                {
                    Console.WriteLine("\nWrong username or password. Try again!");
                    Thread.Sleep(800);
                    //Console.Clear();
                }
            } while (ok==false);
        }

        public static List<EmployeeUsers> GetListOfUsers()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<EmployeeUsers>));
            using (FileStream reader = File.OpenRead("../../EmployeeUsers.xml"))
            {
                EmployeeUsers.Users = (List<EmployeeUsers>)read.Deserialize(reader);
            }
            return EmployeeUsers.Users;
        }

        internal static List<EmployeeUsers> Users()
        {
            return GetListOfUsers(); ;
        }

        internal static List<Bank> GetListOfBanks()
        {
            XmlSerializer read = new XmlSerializer(typeof(List<Bank>));
            using (FileStream reader = File.OpenRead("../../BankList.xml"))
            {
                Bank.Banks = (List<Bank>)read.Deserialize(reader);
            }
            return Bank.Banks;

        }
    }
}
