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
        //Yeni kullanıcı oluşturma ve güncelleme
        static void CreateAccount()
        {
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

            accounts.Add(newAccount);
            Console.WriteLine($"Hesap başarıyla oluşturuldu! Hesap Numarası: {newAccount.AccountNumber}");
        }
        //Yeni oluşturulan kullanıcıya atanan hesap numarası ve bakiyenin Consolda görüntülenmesi
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
        //Kullanıcının hesabına bakiye eklemesi
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
        //Para çekme ve kalan bakiyenin görüntülenmmesi
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

        static void TransferMoney()
        {
            Console.Write("Gönderen hesap numarasını girin: ");
            string senderAccountNumber = Console.ReadLine();
            Account senderAccount = accounts.Find(a => a.AccountNumber == senderAccountNumber);

            if (senderAccount != null)
            {
                Console.Write("Alıcı hesap numarasını girin: ");
                string receiverAccountNumber = Console.ReadLine();
                Account receiverAccount = accounts.Find(a => a.AccountNumber == receiverAccountNumber);

                if (receiverAccount != null)
                {
                    Console.Write("Transfer etmek istediğiniz miktarı girin: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                    {
                        decimal senderBalance = decimal.Parse(senderAccount.Balance);
                        if (senderBalance >= amount)
                        {
                            senderAccount.Balance = (senderBalance - amount).ToString();
                            decimal receiverBalance = decimal.Parse(receiverAccount.Balance);
                            receiverAccount.Balance = (receiverBalance + amount).ToString();

                            senderAccount.Updated = DateTime.Now;
                            receiverAccount.Updated = DateTime.Now;

                            Console.WriteLine($"Transfer başarılı! Gönderen yeni bakiye: {senderAccount.Balance} TL, Alıcı yeni bakiye: {receiverAccount.Balance} TL");
                        }
                        else
                        {
                            Console.WriteLine("Gönderen hesapta yetersiz bakiye.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz miktar.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz alıcı hesap numarası.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz gönderen hesap numarası.");
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
