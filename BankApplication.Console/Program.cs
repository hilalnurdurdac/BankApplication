using BankApplication.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Account> accounts = new Dictionary<string, Account>();
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
                switch (choice)
                {
                    case "1":
                        CreateAccount(accounts);
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

        static void CreateAccount(Dictionary<string, Account> accounts)
        {
            //Yeni kullanıcı oluşturma ve güncelleme
            Account newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            
            //Yeni kullanıcı için Hesap oluşturma adımları
            Console.Write("Adınızı girin: ");
            newAccount.Name = Console.ReadLine();

            Console.Write("Soyadınızı girin: ");
            newAccount.Surname = Console.ReadLine();

            Console.Write("E-posta adresinizi girin: ");
            newAccount.Email = Console.ReadLine();

            Console.Write("Şifrenizi oluşturun: ");
            newAccount.Password = Console.ReadLine();
            Console.WriteLine("Şifreniz başarıyla oluşturuldu! ");

            Console.Write("Telefon numaranızı girin: ");
            newAccount.PhoneNumber = Console.ReadLine();

            Console.Write("Kimlik numaranızı girin: ");
            newAccount.IdentityNumber = Console.ReadLine();

            newAccount.Balance = "0";
            newAccount.AccountNumber = GenerateAccountNumber();

            accounts.Add(newAccount.AccountNumber, newAccount);
            Console.WriteLine($"Hesap başarıyla oluşturuldu! Hesap Numarası: {newAccount.AccountNumber}");
        }

        //Yeni oluşturulan kullanıcıya atanan hesap numarası ve bakiyenin Consolda görüntülenmesi
        static void CheckBalance(Dictionary<string, Account> accounts)
        {
            string accountNumber = GetAccountNumber();
            if (accounts.ContainsKey(accountNumber))
            {
                Console.WriteLine($"Hesap Numarası: {accountNumber}, Bakiye: {accounts[accountNumber].Balance} TL");
            }
            else
            {
                Console.WriteLine("Geçersiz hesap numarası.");
            }
        }
        //Kullanıcının hesabına bakiye eklemesi
        static void DepositMoney(Dictionary<string, Account> accounts)
        {
            string accountNumber = GetAccountNumber();
            if (accounts.ContainsKey(accountNumber))
            {
                Console.Write("Yatırmak istediğiniz miktarı girin: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    decimal currentBalance = decimal.Parse(accounts[accountNumber].Balance);
                    accounts[accountNumber].Balance = (currentBalance + amount).ToString();
                    accounts[accountNumber].Updated = DateTime.Now;
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
        //Para çekme ve kalan bakiyenin görüntülenmmesi
        static void WithdrawMoney(Dictionary<string, Account> accounts)
        {
            string accountNumber = GetAccountNumber();
            if (accounts.ContainsKey(accountNumber))
            {
                Console.Write("Çekmek istediğiniz miktarı girin: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    decimal currentBalance = decimal.Parse(accounts[accountNumber].Balance);
                    if (currentBalance >= amount)
                    {
                        accounts[accountNumber].Balance = (currentBalance - amount).ToString();
                        accounts[accountNumber].Updated = DateTime.Now;
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
        //Hesap numarasının girilmesi
        static string GetAccountNumber()
        {
            Console.Write("Hesap numarasını girin: ");
            return Console.ReadLine();
        }
        //Yeni kullanıcıya random hesap numarası atama
        static string GenerateAccountNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }

    
}
