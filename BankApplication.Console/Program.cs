using BankApplication.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BankApplication
{
    class Program
    {
        static List<Account> accounts = new List<Account>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Bankacılık Uygulamasına Hoş Geldiniz!");
                Console.WriteLine(" ");
                Console.WriteLine("Lütfen Yapmak İstediğiniz İşlemi Giriniz: ");
                Console.WriteLine("1. Hesap Oluştur");
                Console.WriteLine("2. Bakiye Sorgulama");
                Console.WriteLine("3. Para Yatırma");
                Console.WriteLine("4. Para Çekme");
                Console.WriteLine("5. Çıkış");
                Console.Write("Lütfen bir seçenek girin: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    CreateAccount();
                }
                else if (choice == "2")
                {
                    CheckBalance();
                }
                else if (choice == "3")
                {
                    DepositMoney();
                }
                else if (choice == "4")
                {
                    WithdrawMoney();
                }
                else if (choice == "5")
                {
                    running = false;
                    Console.WriteLine("Çıkış yapılıyor...");
                }
                else
                {
                    Console.WriteLine("Geçersiz seçenek, lütfen tekrar deneyin.");
                }

                Console.WriteLine();
            }
        }

        static void CreateAccount()
        {
            Account newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            Console.Write("Adınızı girin: ");
            newAccount.Name = Console.ReadLine();

            Console.Write("Soyadınızı girin: ");
            newAccount.Surname = Console.ReadLine();

            Console.Write("E-posta adresinizi girin: ");
            newAccount.Email = Console.ReadLine();

            Console.Write("Şifrenizi girin: ");
            newAccount.Password = Console.ReadLine();

            Console.Write("Telefon numaranızı girin: ");
            newAccount.PhoneNumber = Console.ReadLine();

            Console.Write("Kimlik numaranızı girin: ");
            newAccount.IdentityNumber = Console.ReadLine();

            newAccount.Balance = "0";
            newAccount.AccountNumber = GenerateAccountNumber();

            accounts.Add(newAccount);
            Console.WriteLine($"Hesap başarıyla oluşturuldu! Hesap Numarası: {newAccount.AccountNumber}");
        }

        static void CheckBalance()
        {
            string accountNumber = GetAccountNumber();
            Account account = accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                Console.WriteLine($"Hesap Numarası: {accountNumber}, Bakiye: {account.Balance} TL");
            }
            else
            {
                Console.WriteLine("Geçersiz hesap numarası.");
            }
        }

        static void DepositMoney()
        {
            string accountNumber = GetAccountNumber();
            Account account = accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                Console.Write("Yatırmak istediğiniz miktarı girin: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    decimal currentBalance = decimal.Parse(account.Balance);
                    account.Balance = (currentBalance + amount).ToString();
                    account.Updated = DateTime.Now;
                    Console.WriteLine($"Yeni bakiye: {account.Balance} TL");
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

        static void WithdrawMoney()
        {
            string accountNumber = GetAccountNumber();
            Account account = accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                Console.Write("Çekmek istediğiniz miktarı girin: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    decimal currentBalance = decimal.Parse(account.Balance);
                    if (currentBalance >= amount)
                    {
                        account.Balance = (currentBalance - amount).ToString();
                        account.Updated = DateTime.Now;
                        Console.WriteLine($"Yeni bakiye: {account.Balance} TL");
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

        static string GetAccountNumber()
        {
            Console.Write("Hesap numarasını girin: ");
            return Console.ReadLine();
        }

        static string GenerateAccountNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }

    
}
