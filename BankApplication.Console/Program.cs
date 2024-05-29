
using System;
using System.Collections.Generic;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, BankAccount> accounts = new Dictionary<int, BankAccount>();
            int accountNumberCounter = 1;
            bool running = true;

            while (running)
            {
                Console.WriteLine("Bankacılık Uygulamasına Hoş Geldiniz!");
                Console.WriteLine("1. Hesap Oluştur");
                Console.WriteLine("2. Bakiye Sorgulama");
                Console.WriteLine("3. Para Yatırma");
                Console.WriteLine("4. Para Çekme");
                Console.WriteLine("5. Çıkış");
                Console.Write("Lütfen bir seçenek girin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateAccount(accounts, ref accountNumberCounter);
                        break;
                    case "2":
                        CheckBalance(accounts);
                        break;
                    case "3":
                        DepositMoney(accounts);
                        break;
                    case "4":
                        WithdrawMoney(accounts);
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Çıkış yapılıyor...");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçenek, lütfen tekrar deneyin.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void CreateAccount(Dictionary<int, BankAccount> accounts, ref int accountNumberCounter)
        {
            Console.Write("Lütfen müşteri adını girin: ");
            string customerName = Console.ReadLine();
            BankAccount newAccount = new BankAccount(accountNumberCounter, customerName);
            accounts.Add(accountNumberCounter, newAccount);
            Console.WriteLine($"Hesap başarıyla oluşturuldu! Hesap Numarası: {accountNumberCounter}");
            accountNumberCounter++;
        }

        static void CheckBalance(Dictionary<int, BankAccount> accounts)
        {
            int accountNumber = GetAccountNumber();
            if (accounts.ContainsKey(accountNumber))
            {
                Console.WriteLine($"Hesap Numarası: {accountNumber}, Bakiye: {accounts[accountNumber].Balance} TL");
            }
            else
            {
                Console.WriteLine("Geçersiz hesap numarası.");
            }
        }

        static void DepositMoney(Dictionary<int, BankAccount> accounts)
        {
            int accountNumber = GetAccountNumber();
            if (accounts.ContainsKey(accountNumber))
            {
                Console.Write("Yatırmak istediğiniz miktarı girin: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    accounts[accountNumber].Deposit(amount);
                    Console.WriteLine($"Yeni bakiye: {accounts[accountNumber].Balance} TL");
                }
                else
                {
                    Console.WriteLine("Geçersiz miktar.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz hesap numarası.");
            }
        }

        static void WithdrawMoney(Dictionary<int, BankAccount> accounts)
        {
            int accountNumber = GetAccountNumber();
            if (accounts.ContainsKey(accountNumber))
            {
                Console.Write("Çekmek istediğiniz miktarı girin: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    if (accounts[accountNumber].Withdraw(amount))
                    {
                        Console.WriteLine($"Yeni bakiye: {accounts[accountNumber].Balance} TL");
                    }
                    else
                    {
                        Console.WriteLine("Yetersiz bakiye.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz miktar.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz hesap numarası.");
            }
        }

        static int GetAccountNumber()
        {
            Console.Write("Hesap numarasını girin: ");
            int.TryParse(Console.ReadLine(), out int accountNumber);
            return accountNumber;
        }
    }

    class BankAccount
    {
        public int AccountNumber { get; private set; }
        public string CustomerName { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(int accountNumber, string customerName)
        {
            AccountNumber = accountNumber;
            CustomerName = customerName;
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
