using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    class ClientManager:Client
    {

        //Create and store the client.
        internal void CreateClient()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - New Client");
            string cnp = Utils.GetCNP();
            if (Utils.ClientExists(cnp)) Console.WriteLine("Client already exists!");
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
                Client myClient = new Client(firstname, lastname, cnp, phone, email, address);
                Console.WriteLine("Adding Client...");
                Clients.Add(myClient);
                Utils.Store<Client>("../../ClientList.xml", Clients);
                Thread.Sleep(700);
                Console.WriteLine("Client added!");
            }
        }

        //Search a client.
        internal void SearchClient()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - Search Client");
            string CNP = Utils.GetCNP();
            bool ok = false;
            Clients = Utils.Read<Client>("../../ClientList.xml");
            foreach (Client client in Client.Clients)
            {
                if (client.CNP == CNP)
                {
                    Console.WriteLine($"Client name is: {client.FirstName} {client.LastName}");
                    ok = true;
                    break;
                }
            }
            if (ok==false) Console.WriteLine("Client not found!");
        }

        //Edit client's info.
        internal void EditClient()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - Edit Client");
            string CNP = Utils.GetCNP();
            if (Utils.ClientExists(CNP))
            {
                Clients = Utils.Read<Client>("../../ClientList.xml" );
                foreach (Client c in Clients)
                {
                    if (CNP == c.CNP)
                    {
                        Console.WriteLine("\n\tFirst name: " + c.FirstName+ "\n\tLast Name: " + c.LastName + "\n\tPhone number: " + c.Phone + "\n\tEmail: " + c.Email + "\n\tAddress: " + c.Address + "\n");
                        Console.WriteLine("Leave empty if you don't want to change the specified information.");
                        Console.Write("\tFirst name: ");
                        string fn = Console.ReadLine();
                        if (fn != "") c.FirstName = fn;
                        Console.Write("\tLast name: ");
                        string ln = Console.ReadLine();
                        if (ln != "") c.LastName = ln;
                        Console.Write("\tPhone: ");
                        string p = Console.ReadLine();
                        if (p != "") c.Phone = p;
                        Console.Write("\tEmail: ");
                        string e = Console.ReadLine();
                        if (e != "") c.Email = e;
                        Console.Write("\tAddress: ");
                        string a = Console.ReadLine();
                        if (a != "") c.Address = a;
                        break;
                    }
                }
                Utils.Store<Client>("../../ClientList.xml", Clients);
            }
            else Console.WriteLine("Client not found!");
        }

        //Remove client and all related data.
        internal void RemoveClient()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - Remove Client");
            string CNP = Utils.GetCNP();
            if (Utils.ClientExists(CNP))
            {
                Clients = Utils.Read<Client>("../../ClientList.xml");
                foreach (Client c in Clients)
                {
                    if (CNP == c.CNP)
                    {
                        Console.WriteLine("Removing Client...");
                        Clients.Remove(c);
                        Utils.Store<Client>("../../ClientList.xml", Clients);
                        Thread.Sleep(700);
                        Console.WriteLine("Client removed!");
                        Console.WriteLine($"Removing {c.FirstName} {c.LastName} accounts.");
                        break;
                    }
                }
                List<Account> Accounts = Utils.Read<Account>("../../AccountList.xml");
                decimal sum=0;
                for (int i = 0; i < Accounts.Count; i++)
                {
                    if(CNP == Accounts[i].CNP)
                    {
                        sum += Accounts[i].Balance;
                    }
                }
                Accounts.RemoveAll(account => account.CNP == CNP);
                Account.Accounts = Accounts;
                Utils.workingBank.BankMoney -= sum;
                Utils.Store<Bank>("../../BankList.xml", Bank.Banks);
                Utils.Store<Account>("../../AccountList.xml", Accounts);
                Console.WriteLine("Removed successfully!");
            }
            else Console.WriteLine("Client doesn't exist!");
        }

        //Display the list of clients.
        public void ShowList()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - List of Clients");
            Clients = Utils.Read<Client>("../../ClientList.xml");
            if (Clients.Count == 0) Console.WriteLine("There are no Clients");
            foreach (Client c in Clients)
            {
                Thread.Sleep(20);
                Console.WriteLine(c.ToString());
            }
        }
       
    }
}
