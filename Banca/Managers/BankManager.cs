using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bank
{
    class BankManager:Bank
    {
        //Create & stroe bank.
        internal void CreateBank()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - New Bank");
            Console.Write("Location: ");
            string loc = Console.ReadLine().ToUpperInvariant();
            Console.Write("Money: ");
            decimal money = Utils.Input();
            Bank bank = new Bank(loc, money);
            Banks.Add(bank);
            Utils.Store<Bank>("../../BankList.xml", Banks);
        }

        //Display the list of banks.
        public void ShowList()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - List of banks");
            Banks = Utils.Read<Bank>("../../BankList.xml");
            if (Banks.Count == 0) Console.WriteLine("There are no banks");
            else
            {
                foreach (Bank c in Banks)
                {
                    Thread.Sleep(20);
                    Console.WriteLine(c.ToString());
                }
            }
        }

        //Select the Bank for future operations.
        public static Bank SelectBank()
        {
            Banks = Utils.Read<Bank>("../../BankList.xml");
            if (Banks.Count == 0) Console.WriteLine("There are no banks");
            else
            {
                bool ok = false;
                do
                {
                    Console.Write("Enter bank name: ");
                    string bankInput = Console.ReadLine().ToUpperInvariant();
                    if (Utils.BankExists(bankInput))
                    {
                        ok = true;
                        foreach (Bank c in Banks)
                        {
                            if (c.Location == bankInput) return c;
                        }
                    }
                    else Console.WriteLine("This bank doesn'y exist.");
                } while (ok==false);
            }
            return new Bank();
        }

        //Money transfer between banks.
        public void Transfer()
        {
            Bank.Banks = Utils.Read<Bank>("../../BankList.xml");
            Bank current = new Bank();
            Console.WriteLine($"Current Bank: {Utils.workingBank.Location} - {Utils.workingBank.BankMoney}");
            if (Banks.Count == 0) Console.WriteLine("There are no banks");
            else
            {
                foreach (Bank c in Banks)
                {
                    Thread.Sleep(20);
                    if(c.Location!= Utils.workingBank.Location) Console.WriteLine(c.ToString());
                    if (c.Location == Utils.workingBank.Location) current = c;
                } 
                string loc = LocInput();
                Console.Write("Amount: ");
                decimal Amount = Utils.Input();
                foreach (Bank c in Banks)
                {
                    if(c.Location == loc.ToUpperInvariant())
                    {
                        if (Utils.workingBank.BankMoney >= Amount)
                        {
                            c.BankMoney += Amount;
                            current.BankMoney -= Amount;
                            Utils.workingBank.BankMoney -= Amount;
                            Utils.Store<Bank>("../../BankList.xml", Banks);
                        }
                        else Console.WriteLine($"There are not enough money in {Utils.workingBank}");
                        break;
                    }
                }
                Utils.Store<Bank>("../../BankList.xml", Banks);
            }
        }

        //Input check for bank location (workingLocation).
        private string LocInput()
        {
            bool ok = false; 
            string loc;
            do
            {
                Console.Write("Transfer to:");
                loc = Console.ReadLine().ToUpperInvariant();
                if (loc == Utils.workingBank.Location.ToUpperInvariant())
                {
                    Console.WriteLine("You can't transfer money in you own bank.");
                }
                else if (Utils.BankExists(loc) && loc != Utils.workingBank.Location)
                {
                    ok = true;
                    return loc;
                }
                else
                {
                    Console.WriteLine("This bank doen't exist");
                }
            } while (ok == false) ;
            return loc;
        }
    }
}
