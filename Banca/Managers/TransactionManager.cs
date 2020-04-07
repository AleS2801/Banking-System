using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Bank
{
    class TransactionManager:Transaction
    {
        public static Bank workingBank = Utils.workingBank;
        public static decimal money = workingBank.BankMoney;

        //Make deposit in account.
        public void Deposit()
        {
            Transaction t;
            string info = initTransaction();
            if (info.Length > 6)
            {
                string Number = info.Split(' ')[0];
                string AccountType = info.Split(' ')[1];
                decimal Balance = decimal.Parse(info.Split(' ')[2]);
                int Time = 0;
                bool ok = int.TryParse(info.Split(' ')[3], out Time);
                string Type = "DEPOSIT";
                Console.Write("Amount: ");
                decimal Amount = Utils.Input();
                if ((AccountType == "DEPOSIT" && Time <= 0) || AccountType == "CURRENT")
                {
                    t = new Transaction(Number, Balance, Amount, Type);
                    Transactions.Add(t);
                    UpdateDeposit(Number, Amount);
                    Utils.Store<Transaction>("../../Transactions.xml", Transactions);
                }
                else Console.WriteLine($"Your account is still locked. {AccountType} - {Time} months");
            }
        }

        //Give credit.
        internal void NewCredit()
        {
            Transaction t;
            string info = initTransaction();
            if (info.Length > 6)
            {
                string Number = info.Split(' ')[0];
                string AccountType = info.Split(' ')[1];
                decimal Balance = decimal.Parse(info.Split(' ')[2]);
                string Type = "DEPOSIT";
                Console.Write("Amount: ");
                decimal Amount = Utils.Input();
                Console.Write("Interest: ");
                string Interest = Console.ReadLine().ToUpperInvariant();
                if (AccountType == "CURRENT")
                {
                    t = new Transaction(Number, Balance, Amount, Type);
                    t.Type += $" CREDIT WITH {Interest}% INTEREST RATE";
                    Transactions.Add(t);
                    UpdateDeposit(Number, Amount);
                    Utils.Store<Transaction>("../../Transactions.xml", Transactions);
                    workingBank.BankMoney -= Amount;
                    Utils.Store<Bank>("../../BankList.xml", Bank.Banks);
                }
                else
                {
                    Console.WriteLine("This is not an eligible account!");
                }
            }
        }

        //Withdraw money from account.
        public void Withdraw()
        {
            Transaction t;
            string info = initTransaction();
            if (info.Length > 6)
            {
                string Number = info.Split(' ')[0];
                string AccountType = info.Split(' ')[1];
                decimal Balance = decimal.Parse(info.Split(' ')[2]);
                int Time = 0;
                bool ok = int.TryParse(info.Split(' ')[3], out Time);
                string Type = "WITHDRAW";
                Console.Write("Amount: ");
                decimal Amount = Utils.Input();
                if ((AccountType == "DEPOSIT" && Time <= 0) || AccountType == "CURRENT")
                {
                    t = new Transaction(Number, Balance, Amount, Type);
                    Transactions.Add(t);
                    UpdateDeposit(Number, Amount);
                    Utils.Store<Transaction>("../../Transactions.xml", Transactions);
                }
                else Console.WriteLine($"Your account is still locked. {AccountType} - {Time} months");
            }
        }

        //Update files after deposit.
        public void UpdateDeposit(string Number, decimal Amount)
        {
            Account.Accounts = Utils.Read<Account>("../../AccountList.xml");
            Account account = AccountManager.GetAccount(Number);
            account.Balance += Amount;
            money += Amount;
            workingBank.BankMoney += money;
            Console.WriteLine("Transaction done.");
            Utils.Store<Account>("../../AccountList.xml", Account.Accounts);
            Utils.Store<Bank>("../../BankList.xml", Bank.Banks);
        }

        //Update files after withdraw.
        public void UpdateWithdraw(string Number, decimal Amount)
        {
            Account.Accounts = Utils.Read<Account>("../../AccountList.xml");
            Account account = AccountManager.GetAccount(Number);
            account.Balance -= Amount;
            money -= Amount;
            workingBank.BankMoney = money;
            Utils.Store<Account>("../../AccountList.xml", Account.Accounts);
            Utils.Store<Bank>("../../BankList.xml", Bank.Banks);
        }

        //Delect the account that is used for transaction & get some info.
        private string initTransaction()
        {
            AccountManager accountManager = new AccountManager();
            string info;
            string Number = "";
            string AccountType = "";
            decimal Balance = 0;
            string Time="";
            accountManager.ShowList();
            if (Account.ListAccountsOfClient.Count > 1)
            {
                Console.WriteLine("Choose the account that you want to operate with!");
                Console.Write("Number: ");
                Number = Console.ReadLine().ToUpperInvariant();
                foreach (Account account in Account.ListAccountsOfClient)
                {
                    if (account.Number == Number)
                    {
                        AccountType = account.Type;
                        Balance = account.Balance;
                        Time = account.Time;
                        break;
                    }
                }
            }
            else if (Account.ListAccountsOfClient.Count == 1)
            {
                Number = Account.ListAccountsOfClient[0].Number;
                AccountType = Account.ListAccountsOfClient[0].Type;
                Balance = Account.ListAccountsOfClient[0].Balance;
                Time = Account.ListAccountsOfClient[0].Time;
            }
            else if (Account.ListAccountsOfClient.Count == 0)
            {
                Console.WriteLine("There are no accounts.Try again.");
            }
            info = Number + " " + AccountType + " " + Balance + " " + Time;
            return info;

        }
    }
}
