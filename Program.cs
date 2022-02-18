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
        public static void LoadTransactions(List<Transaction> transactions)
        {
            if (File.Exists("transactions.csv"))  //<--- is File.Exists a function of IO or CsvHelper or something?
            {
                var fileReader = new StreamReader("transactions.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                var transactionFile = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
        }

        public static void SaveTransactions(List<Transaction> transactions)
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

        static void Main(string[] args)
        {
            var transactions = new List<Transaction>();
            var keepGoing = true;
            while (keepGoing)
            {
                LoadTransactions(transactions);

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
                        Console.WriteLine("Would you like to deposit these funds to your (c)hecking or (s)avings account? ");
                        var depositAccount = Console.ReadLine().ToLower();
                        if (depositAccount == "c")
                        {
                            var checkingDeposit = new Transaction();
                            checkingDeposit.AccountType = "Checking";
                            checkingDeposit.TransactionType = "Deposit";
                            checkingDeposit.TransactionAmount = PromptForInteger("Please enter deposit amount.");
                            Console.WriteLine($"${checkingDeposit.TransactionAmount} was deposited into your checking account. ");
                            transactions.Add(checkingDeposit);
                        }
                        else if (depositAccount == "s")
                        {
                            var savingsDeposit = new Transaction();
                            savingsDeposit.AccountType = "Savings";
                            savingsDeposit.TransactionType = "Deposit";
                            savingsDeposit.TransactionAmount = PromptForInteger("How much would you like to deposit into your savings account? ");
                            Console.WriteLine("");
                            Console.WriteLine($"${savingsDeposit.TransactionAmount} has been deposited into your savings account. ");
                            Console.WriteLine("");
                            transactions.Add(savingsDeposit);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that's not a valid input.");
                        }
                        SaveTransactions(transactions);
                        break;

                    case "w":
                        Console.WriteLine("Would you like to withdraw these funds from your (c)hecking or (s)avings account?");
                        var withdrawalAccount = Console.ReadLine().ToLower();
                        if (withdrawalAccount == "c")
                        {
                            var checkingWithdrawal = new Transaction();
                            checkingWithdrawal.AccountType = "Checking";
                            checkingWithdrawal.TransactionType = "Withdrawal";
                            checkingWithdrawal.TransactionAmount = PromptForInteger("How much would you like to withdraw? ");
                            Console.WriteLine($"${checkingWithdrawal.TransactionAmount} has been withdrawn from your checking account. ");
                            transactions.Add(checkingWithdrawal);
                        }
                        else if (withdrawalAccount == "s")
                        {
                            var savingsWithdrawal = new Transaction();
                            savingsWithdrawal.AccountType = "Savings";
                            savingsWithdrawal.TransactionType = "Withdrawal";
                            savingsWithdrawal.TransactionAmount = PromptForInteger("How much would you like to withdraw?");
                            Console.WriteLine($"${savingsWithdrawal.TransactionAmount} has been withdrawn from your savings account. ");
                            transactions.Add(savingsWithdrawal);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, that's not a valid input.");
                        }
                        SaveTransactions(transactions);
                        break;

                    case "b":
                        var totalCheckingDeposits = transactions.Where(transaction => transaction.AccountType == "Checking" && transaction.AccountType == "Deposit")
                                                    .Sum(transaction => transaction.TransactionAmount);
                        var totalCheckingWithdrawals = transactions.Where(transaction => transaction.AccountType == "Checking" && transaction.AccountType == "Withdrawal")
                                                                .Sum(transaction => transaction.TransactionAmount);
                        var totalChecking = totalCheckingDeposits - totalCheckingWithdrawals;

                        var totalSavingsDeposits = transactions.Where(transaction => transaction.AccountType == "Savings" && transaction.AccountType == "Deposit")
                                                               .Sum(transaction => transaction.TransactionAmount);
                        var totalSavingsWithdrawals = transactions.Where(transaction => transaction.AccountType == "Savings" && transaction.AccountType == "Withdrawal")
                                                                  .Sum(transaction => transaction.TransactionAmount);
                        var totalSavings = totalSavingsDeposits - totalSavingsWithdrawals;
                        Console.WriteLine($"Your total checking account balance is ${totalChecking}");
                        Console.WriteLine($"Your total savings account balance is ${totalSavings}");
                        break;

                    case "h":
                        foreach (var transaction in transactions)
                        {
                            if (transaction.AccountType == "Deposit")
                            {
                                Console.WriteLine($"{transaction.TransactionType} to {transaction.AccountType}: ${transaction.TransactionAmount} ");
                            }
                            else if (transaction.AccountType == "Withdrawal")
                            {
                                Console.WriteLine($"{transaction.TransactionType} from {transaction.AccountType}: ${transaction.TransactionAmount} ");
                            }
                        }
                        Console.WriteLine("");
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

            }
        }
    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

