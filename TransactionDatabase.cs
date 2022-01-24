using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System;


namespace FirstBankOfSuncoast
{

    class Transaction
    {
        public string AccountType { get; set; }
        public string TransactionAmount { get; set; }
        public DateTime WhenAcquired { get; set; }

        public void DisplayTransactions()
        {
            Console.WriteLine($"Account used: {AccountType} ");
            Console.WriteLine($"Deposit/Withdrawl amount: {TransactionAmount} ");
            Console.WriteLine($"Transaction date & time: {WhenAcquired} ");

            Console.WriteLine();
        }
    }
    // create Transaction class to support both checking and savings as well as deposits and withdrawls.
    class TransactionDatabase
    {
        //store a history of transactions in a SINGLE List<Transaction>.
        //create transactions method to view transactions by savings or checking
        public List<TransactionDatabase> Transactions { get; set; } = new List<TransactionDatabase>();

        public void LoadTransaction()
        {
            if (File.Exists("transactions.csv"))
            {
                var fileReader = new StreamReader("transactions.csv");

                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                Transactions = csvReader.GetRecords<TransactionDatabase>().ToList();
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
            var checkingBalance =
        }

        public void AddTransaction(TransactionDatabase add)
        {
            Console.WriteLine("Would you like to deposit these funds to your (c)hecking or (s)avings account? ");
            var choice = Console.ReadLine().ToLower();
            if (choice == "c")
            {

            }

            Transactions.Add(add);
        }

    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

