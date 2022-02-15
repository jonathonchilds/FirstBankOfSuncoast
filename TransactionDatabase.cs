using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System;


namespace FirstBankOfSuncoast
{
    // create Transaction class to support both checking and savings as well as deposits and withdrawls.
    class TransactionDatabase
    {
        //store a history of transactions in a SINGLE List<Transaction>.
        //create transactions method to view transactions by savings or checking
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public static void AddFunds(TransactionDatabase database)
        {
            var transaction = new Transaction();
            Console.WriteLine();
            transaction.AccountType = DepositAccountType("Would you like to deposit these funds to your (c)hecking or (s)avings account? ").ToLower();
            transaction.TransactionAmount = PromptForInteger("How much would you like to deposit?");
            transaction.WhenAcquired = DateTime.Now;
            database.Transactions.Add(transaction);
        }

        public static void WithdrawFunds(TransactionDatabase database)
        {
            var transaction = new Transaction();
            Console.WriteLine();
            transaction.AccountType = DepositAccountType("Would you like to withdraw these funds from your (c)hecking or (s)avings account? ").ToLower();
            transaction.TransactionAmount = PromptForInteger("How much would you like to withdraw?");
            transaction.WhenAcquired = DateTime.Now;
            database.Transactions.Add(transaction);
        }

        public static void BalanceCheck(TransactionDatabase database)
        {
            Console.WriteLine();
            var viewPreference = PromptForString("Would you like to view the balance of your (s)avings or (c)hecking? ").ToUpper();
            Console.WriteLine();
            var viewChecking = database.Transactions.Sum(checking => database.transactions.AccountType);
            var viewSavings = database.Transactions.Sum(savings => database.Transactions.AccountType);

            if (database.Transactions.Count == 0)
            {
                Console.WriteLine("It looks like you haven't made any deposits, yet.");
            }
            else if (viewPreference == "C")
            {
                foreach (var checkingDeposit in viewChecking)
                {
                    viewChecking.CheckingBalance();
                }
            }
            else if (viewPreference == "S")
            {
                foreach (var savingsDeposit in viewSavings)
                {
                    viewSavings.SavingsBalance();
                }
            }
        }

        public void LoadTransaction()
        {
            if (File.Exists("transactions.csv"))
            {
                var fileReader = new StreamReader("transactions.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                Transactions = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
        }
        public void SaveTransaction()
        {
            var fileWriter = new StreamWriter("transactions.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(Transactions);
            fileWriter.Close();
        }

        public static string DepositAccountType(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine().ToLower();
            if (userInput == "c")
            {
                return ("Checking Deposit");
            }
            if (userInput == "s")
            {
                return ("Savings Deposit");
            }
            return ("Invalid Input");
        }

        public static string WithdrawlAccountType(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine().ToLower();
            if (userInput == "c")
            {
                return ("Checking Withdrawl");
            }
            if (userInput == "s")
            {
                return ("Savings Withdrawl");
            }
            return ("Invalid Input");
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine().ToUpper();
            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput); //<---- need be in decimal format
            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using 0 as your answer. ");
                return 0;
            }

        }



    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

