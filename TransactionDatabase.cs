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

        public void LoadTransaction()
        {
            if (File.Exists("transactions.csv"))
            {
                var fileReader = new StreamReader("transactions.csv");

                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                Transactions = csvReader.GetRecords<Transaction>().ToList();
            }
        }
        public void SaveTransaction()
        {
            var fileWriter = new StreamWriter("transactions.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(Transactions);

            fileWriter.Close();

        }
        public void CheckingAccount()
        {
            //var checkingBalance =
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
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);
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

        public static void AddTransaction(TransactionDatabase database)
        {
            var transaction = new Transaction();
            Console.WriteLine();
            transaction.AccountType = PromptForString("Would you like to deposit these funds to your (c)hecking or (s)avings account? ").ToLower();
            transaction.TransactionAmount = PromptForInteger("How much would you like to deposit? ");

            database.Transactions.Add(transaction);

            //     public static void Add(DinosaurDatabase database)
            // {
            //     var dinosaur = new Dinosaur();
            //     Console.WriteLine();
            //     dinosaur.Name = PromptForString("What is the dinosaurs name? ").ToUpper();
            //     dinosaur.DietType = PromptForDiet("Is this dinosaur an (H)erbivore or a (C)arnivore? ").ToUpper();
            //     dinosaur.Weight = PromptForInteger("How much does your dinosaur weigh, in pounds? ");
            //     dinosaur.EnclosureNumber = PromptForInteger("Please assign an enclosure number to this dinosaur: ");
            //     dinosaur.WhenAcquired = DateTime.Now;
            //     database.Dinosaurs.Add(dinosaur);
            // }

        }

    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

