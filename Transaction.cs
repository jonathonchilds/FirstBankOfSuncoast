using System;


namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public string AccountType { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime WhenAcquired { get; set; }

        public void DisplayTransactions()
        {
            Console.WriteLine($"Account used: {AccountType} ");
            Console.WriteLine($"Deposit/Withdrawl amount: {TransactionAmount} ");
            Console.WriteLine($"Transaction date & time: {WhenAcquired} ");

            Console.WriteLine();
        }
    }
}


// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive...
//...The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)

