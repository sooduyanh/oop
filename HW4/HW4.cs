using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    class Program
    {
        static void Main()
        {
            Bank bank = new Bank();

            // Mở tài khoản cho Alice, Bob, Alice (lần thứ 2) và Eve
            bank.CreateAccount("001", "Alice", "901", 100, 5);
            bank.CreateAccount("002", "Bob", "902", 50, 5);
            bank.CreateAccount("003", "Alice", "901", 200, 10);
            bank.CreateAccount("004", "Eve", "903", 200, 10);

            // Thực hiện các giao dịch
            bank.Deposit("001", 100, new DateTime(2005, 7, 15));
            bank.Deposit("001", 100, new DateTime(2005, 7, 31));
            bank.Deposit("002", 150, new DateTime(2005, 7, 1));
            bank.Deposit("002", 150, new DateTime(2005, 7, 15));
            bank.Deposit("003", 200, new DateTime(2005, 7, 5));
            bank.Deposit("004", 250, new DateTime(2005, 7, 31));
            bank.Withdraw("001", 10, new DateTime(2005, 7, 10));
            bank.Withdraw("002", 20, new DateTime(2005, 7, 15));
            bank.Withdraw("003", 30, new DateTime(2005, 7, 31));
            bank.Withdraw("004", 40, new DateTime(2005, 7, 31));

            // Tính lãi suất và cập nhật số tiền
            bank.CalculateInterest();

            // In báo cáo
            bank.GenerateReport();
        }
    }

    class Transaction
    {
        public DateTime Date { get; }
        public string Type { get; }
        public decimal Amount { get; }

        public Transaction(DateTime date, string type, decimal amount)
        {
            Date = date;
            Type = type;
            Amount = amount;
        }
    }

    class Account
    {
        public string AccountNumber { get; }
        public string OwnerName { get; }
        public string OwnerID { get; }
        public decimal Balance { get; private set; }
        public decimal InterestRate { get; }
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public Account(string accountNumber, string ownerName, string ownerID, decimal balance, decimal interestRate)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            OwnerID = ownerID;
            Balance = balance;
            InterestRate = interestRate;
        }

        public void Deposit(decimal amount, DateTime date)
        {
            Balance += amount;
            Transactions.Add(new Transaction(date, "Deposit", amount));
        }

        public void Withdraw(decimal amount, DateTime date)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Transactions.Add(new Transaction(date, "Withdrawal", amount));
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }
    }

    class Bank
    {
        private List<Account> accounts = new List<Account>();

        public void CreateAccount(string accountNumber, string ownerName, string ownerID, decimal balance, decimal interestRate)
        {
            accounts.Add(new Account(accountNumber, ownerName, ownerID, balance, interestRate));
        }

        public void Deposit(string accountNumber, decimal amount, DateTime date)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Deposit(amount, date);
            }
        }

        public void Withdraw(string accountNumber, decimal amount, DateTime date)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount, date);
            }
        }

        public void CalculateInterest()
        {
            foreach (var account in accounts)
            {
                decimal interest = (account.Balance * account.InterestRate) / 100;
                account.Deposit(interest, DateTime.Now);
            }
        }

        public void GenerateReport()
        {
            foreach (var account in accounts)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Owner: {account.OwnerName}");
                Console.WriteLine($"Balance: {account.Balance}");
                Console.WriteLine("Transactions:");
                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine($"{transaction.Date}: {transaction.Type} - {transaction.Amount}");
                }
                Console.WriteLine();
            }
        }

        private Account FindAccount(string accountNumber)
        {
            return accounts.Find(account => account.AccountNumber == accountNumber);
        }
    }
}
