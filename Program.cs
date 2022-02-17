using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System;

namespace FirstBankOfSuncoast
{

    class Program
    {

        public static void LoadTransaction()
        {
            if (File.Exists("transactions.csv"))  //<--- is File.Exists a function of IO or CsvHelper or something?
            {
                var fileReader = new StreamReader("transactions.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                var transactionFile = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
        }

        public static void SaveTransaction(List<Transaction> transactions)
        {
            var fileWriter = new StreamWriter("transactions.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(transactions);
            fileWriter.Close();
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().ToUpper();
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            if (Int32.TryParse(Console.ReadLine(), out userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using 0 as your answer. ");
                return 0;
            }

        }

        static void Main(string[] args)
        {
            //var database = new TransactionDatabase(); <----- why are these two lines unnecessary?
            //database.LoadTransaction();

            var transaction = new TransactionDatabase();

            //transaction.LoadTransaction();

            var keepGoing = true;

            while (keepGoing)
            {
                LoadTransaction();

                Console.WriteLine("");
                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Make a (d)eposit ");
                Console.WriteLine("Make a (w)ithdrawl ");
                Console.WriteLine("View (b)alance ");
                Console.WriteLine("View transaction (h)istory ");
                Console.WriteLine("(Q)uit ");
                Console.WriteLine("-------------------------------------------");

                var choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "d":
                        TransactionDatabase.Deposit(transaction);
                        break;

                    case "w":
                        TransactionDatabase.WithdrawFunds(transaction);
                        break;

                    case "b":
                        TransactionDatabase.BalanceCheck(transaction);
                        break;

                    case "h":
                        break;

                    case "q":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("That is not a valid response. Please try again.");
                        break;


                }
                SaveTransaction();
                // The application should, after each transaction, write all the transactions to a file. This is the same file the application loads.

            }
        }
    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

