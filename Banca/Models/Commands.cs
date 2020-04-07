using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bank
{
    public class Commands
    {
        public static Dictionary<string, Action> commands;
        public static Dictionary<string, Action> commands_Client;
        ClientManager clientManager= new ClientManager();
        BankManager bankManager= new BankManager();
        EmployeeManager employeeManager= new EmployeeManager();
        AccountManager accountManager= new AccountManager();
        TransactionManager transactionManager= new TransactionManager();
        public Commands()
        {
            commands = new Dictionary<string, Action>
            {
                { "BANK TRANSFER", bankManager.Transfer },
                { "CREATE ACCOUNT", accountManager.CreateAccount },
                { "DEPOSIT", transactionManager.Deposit },
                { "EDIT CLIENT", clientManager.EditClient },
                { "EDIT EMPLOYEE", employeeManager.EditEmployee },
                { "EXIT", Exit },
                { "GET ACCOUNTS", accountManager.ShowList },
                { "GET ALL ACCOUNTS", accountManager.ShowAllList },
                { "HELP", ShowCommandList },
                { "HOME", Home },
                { "LIST OF BANKS", bankManager.ShowList },
                { "LIST OF CLIENTS", clientManager.ShowList },
                { "LIST OF EMPLOYEES", employeeManager.ShowList },
                { "NEW BANK", bankManager.CreateBank },
                { "NEW CLIENT", clientManager.CreateClient },
                { "NEW CREDIT", transactionManager.NewCredit },
                { "NEW EMPLOYEE", employeeManager.CreateEmployee },
                { "SEARCH CLIENT", clientManager.SearchClient }, 
                { "SEARCH EMPLOYEE", employeeManager.SearchEmployee }, 
                { "REMOVE CLIENT", clientManager.RemoveClient },
                { "REMOVE EMPLOYEE", employeeManager.RemoveEmployee },
                { "WITHDRAW", transactionManager.Withdraw }
            };
        }

        //Restarts everything.
        public static void Home()
        {
            Console.Clear();
            Program.initOperator();
        }

        //Display all commands.
        private void ShowCommandList()
        {
            Console.Clear();
            foreach (KeyValuePair<string,Action> command in commands)
            {
                Thread.Sleep(20);
                Console.WriteLine(command.Key);
            }
        }

        //Closes App.
        private void Exit()
        {
            Thread.Sleep(300);
            Console.Write("The program will close");
            Thread.Sleep(400);
            Console.Write(".");
            Thread.Sleep(400);
            Console.Write(".");
            Thread.Sleep(400);
            Console.Write(".");
            Thread.Sleep(400);
            Environment.Exit(0);
        }
    }
}
