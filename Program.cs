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

        static void Main(string[] args)
        {
            var database = new TransactionDatabase();
            database.LoadTransaction();

            var keepGoing = true;

            while (keepGoing)
            {
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
                        TransactionDatabase.AddFunds(database);
                        break;

                    case "w":
                        TransactionDatabase.WithdrawFunds(database);
                        break;

                    case "b":
                        TransactionDatabase.BalanceCheck(database);
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
                database.SaveTransaction();
                // The application should, after each transaction, write all the transactions to a file. This is the same file the application loads.

            }
        }
    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

