using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{

    // create Transaction class to support both checking and savings as well as deposits and withdrawls.
    class Transaction
    {
        //store a history of transactions in a SINGLE List<Transaction>.
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    class Program
    {


        // The application should load past transactions from a file when it first starts.
        static void Main(string[] args)
        {
            var database = new TransactionDatabase();

            database.LoadTransactions();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("");

                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Make a (d)eposit ");
                //create deposit method that prompts for account to deposit into: savings or checking

                Console.WriteLine("Make a (w)ithdrawl ");
                //create withdrawl method that prompts for account to withdrawl from: savings or checking

                Console.WriteLine("View (b)alance ");
                //create balance method that prompts for account for which user wants balance displayed: savings or checking

                Console.WriteLine("View transaction (h)istory ");
                // create transactions method to view transactions by savings or checking

                Console.WriteLine("(Q)uit ");
                Console.WriteLine("-------------------------------------------");

                var choice = Console.ReadLine().ToLower();

                switch (choice)
                {
                    case "d":
                        break;

                    case "w":
                        break;

                    case "b":
                        break;

                    case "Q":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("That is not a valid response. Please try again.");
                        break;


                }
                database.SaveTransactions();
                // The application should, after each transaction, write all the transactions to a file. This is the same file the application loads.

            }
        }
    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

