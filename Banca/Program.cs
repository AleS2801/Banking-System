using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Utils.GetListOfBanks();// initializeaza lista de banci existenti pt a putea fi folosita
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("....\n");
            }
            try
            {
                Utils.GetListOfEmployees();// initializeaza lista de angajati existenti pt a putea fi folosita
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("You have never added an employee! Add a new employee.\n");
            }
            try
            {
                Utils.GetListOfClients(); // initializeaza lista de clienti existenti pt a putea fi folosita
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("You have never added a client! Add a new client.\n");
            }
            try
            {
                Utils.GetListOfAccounts(); // initializeaza lista de conturi existente pt a putea fi folosita
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("You have never created an account! Add a new account.\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong\n");
            }
            try
            {
                Utils.GetListOfUsers(); // initializeaza lista de conturi existente pt a putea fi folosita
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("You have never created an account! Add a new account.\n");
            }

            initOperator();
        }
        public static void initOperator()
        {
            Utils.LogIn();
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine(DateTime.Now);
            Thread.Sleep(200);
            Console.WriteLine($"---------------Operator - CBA {Utils.workingBank.Location}----------------");
            Thread.Sleep(200);
            while (true)
            {
                new Commands();
                Thread.Sleep(200);
                Console.Write("Enter a command: ");
                string s = Console.ReadLine().ToUpperInvariant();
                try
                {
                    Thread.Sleep(300);
                    Commands.commands[s].Invoke();
                }
                catch (KeyNotFoundException)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("This command doesn't exist.");
                    Console.WriteLine("Enter HELP to see the list of the commands");
                }
                Console.WriteLine("-----------------------------------------------------------");
            }
        }
    }
}
