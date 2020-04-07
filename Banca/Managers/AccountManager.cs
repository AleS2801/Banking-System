using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Bank
{
    public class AccountManager:Account
    {
        static Random rnd = new Random();

        //Create and store the account.
        public void CreateAccount()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("MENU - New account");
            Account account;
            string CNP = Utils.GetCNP();
            if (Utils.ClientExists(CNP))
            {
                decimal Balance = 0;
                Console.Write($"Currency: ");
                string Currency = CheckCurrency(Console.ReadLine().ToUpperInvariant());
                string Type = AccountType().ToUpper();
                string Time = "-";
                string Number = Bank.name + Currency.ToUpperInvariant() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + CNP.Substring(9, 4) + rnd.Next(100, 999).ToString();
                if(Type == "DEPOSIT")
                {
                    Time = TimeInput().ToString();
                    Console.Write("Amount: ");
                    Balance = Utils.Input();
                }
                account = new Account(CNP, Number, Balance, Currency, Type,Time);
                Accounts.Add(account);
                Console.WriteLine("Adding account...");
                Utils.Store<Account>("../../AccountList.xml", Accounts);
                Thread.Sleep(700);
                Console.WriteLine("Account created!");
            }
            else
            {
                Console.WriteLine("Client doesn't exist! Add a the client!");
            }
        }

        private int TimeInput()
        {
            int time;
            bool ok = false;
            do
            {
                Console.WriteLine("Time (months): ");
                ok = int.TryParse(Console.ReadLine(),out time);
                if (ok == false) Console.WriteLine("Wrong time input. Try again.");
            } while (ok == false);
            return time;
        }

        //Display the list of accounts.
        internal void ShowAllList()
        {
            Console.Clear();
            Accounts = Utils.Read<Account>("../../AccountList.xml");
            foreach (Account a in Accounts)
            {
                Thread.Sleep(30);
                Console.WriteLine(a.ToString());
            }
        }

        //Input for the type of the account.
        private string AccountType()
        {
            bool ok = false;
            string type;
            do
            {
                Console.Write("Insert the type: ");
                type = Console.ReadLine();
                if (type.ToLower() != "current" && type.ToLower() != "deposit")
                {
                    Console.WriteLine("The type of the account can be CURRENT or DEPOSIT");
                }
                else ok = true;
            } while (ok==false);
            return type;
        }

        //Returns a specified account based on the unique number.
        public static Account GetAccount(string Number)
        {
            foreach (Account account in Accounts)
            {
                if (Number == account.Number)
                {
                    return account;
                }
            }
            return new Account();
        }

        //Display the accounts of a client, based on unique CNP.
        public List<Account> AccountsOfClient()
        {
            //Client.SearchClient();
            ListAccountsOfClient.Clear();
            Accounts = Utils.Read<Account>("../../AccountList.xml");
            string CNP = Utils.GetCNP();
            if (Utils.ClientExists(CNP))
            {
                Console.WriteLine("Searching for accounts...");
                Thread.Sleep(1000);
                foreach (Account a in Accounts)
                {
                    if (CNP == a.CNP)
                    {
                        ListAccountsOfClient.Add(a);
                    }
                }
                return ListAccountsOfClient;
            }
            else
            {
                Console.WriteLine("Client doesn't exist!");
            }
            return new List<Account>();
        }

        //Checks if the currency is an actual one.
        public string CheckCurrency(string currency)
        {
            bool ok = false;
            do
            {
                if (Enum.IsDefined(typeof(Currencies), currency.ToUpperInvariant())) ok = true;
                else
                {
                    Console.Write("Currency: ");
                    currency = Console.ReadLine();
                }
            } while (ok == false);
            return currency;
        }

        internal void ShowList()
        {
            Console.Clear();
            ListAccountsOfClient = AccountsOfClient();
            foreach (Account a in ListAccountsOfClient)
            {
                Thread.Sleep(30);
                Console.WriteLine(a.ToString());
            }
        }
        enum Currencies
        {
            AED,
            AFN,
            ALL,
            AMD,
            ANG,
            AOA,
            ARS,
            AUD,
            AWG,
            AZN,
            BAM,
            BBD,
            BDT,
            BGN,
            BHD,
            BIF,
            BMD,
            BND,
            BOB,
            BRL,
            BSD,
            BTN,
            BWP,
            BYN,
            BZD,
            CAD,
            CDF,
            CHF,
            CLP,
            CNY,
            COP,
            CRC,
            CUC,
            CUP,
            CVE,
            CZK,
            DJF,
            DKK,
            DOP,
            DZD,
            EGP,
            ERN,
            ETB,
            EUR,
            FJD,
            FKP,
            GBP,
            GEL,
            GGP,
            GHS,
            GIP,
            GMD,
            GNF,
            GTQ,
            GYD,
            HKD,
            HNL,
            HRK,
            HTG,
            HUF,
            IDR,
            ILS,
            IMP,
            INR,
            IQD,
            IRR,
            ISK,
            JEP,
            JMD,
            JOD,
            JPY,
            KES,
            KGS,
            KHR,
            KMF,
            KPW,
            KRW,
            KWD,
            KYD,
            KZT,
            LAK,
            LBP,
            LKR,
            LRD,
            LSL,
            LYD,
            MAD,
            MDL,
            MGA,
            MKD,
            MMK,
            MNT,
            MOP,
            MRU,
            MUR,
            MVR,
            MWK,
            MXN,
            MYR,
            MZN,
            NAD,
            NGN,
            NIO,
            NOK,
            NPR,
            NZD,
            OMR,
            PAB,
            PEN,
            PGK,
            PHP,
            PKR,
            PLN,
            PYG,
            QAR,
            RON,
            RSD,
            RUB,
            RWF,
            SAR,
            SBD,
            SCR,
            SDG,
            SEK,
            SGD,
            SHP,
            SLL,
            SOS,
            SPL,
            SRD,
            STN,
            SVC,
            SYP,
            SZL,
            THB,
            TJS,
            TMT,
            TND,
            TOP,
            TRY,
            TTD,
            TVD,
            TWD,
            TZS,
            UAH,
            UGX,
            USD,
            UYU,
            UZS,
            VEF,
            VND,
            VUV,
            WST,
            XAF,
            XCD,
            XDR,
            XOF,
            XPF,
            YER,
            ZAR,
            ZMW,
            ZWD
        }
    }
}
