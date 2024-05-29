namespace BankApplication.Domain;

using global::BankApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;



    public class AccountService
    {
        private List<Account> accounts = new List<Account>();

        public Account CreateAccount(string name, string surname, string email, string password, string phoneNumber, string identityNumber)
        {
            Account newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = name,
                Surname = surname,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                IdentityNumber = identityNumber,
                Balance = "0",
                AccountNumber = GenerateAccountNumber()
            };

            accounts.Add(newAccount);
            return newAccount;
        }

        public Account Login(string email, string password)
        {
            return accounts.SingleOrDefault(a => a.Email == email && a.Password == password);
        }

        public Account GetAccountByNumber(string accountNumber)
        {
            return accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);
        }

        public bool DepositMoney(Account account, decimal amount)
        {
            if (amount > 0)
            {
                decimal currentBalance = decimal.Parse(account.Balance);
                account.Balance = (currentBalance + amount).ToString();
                account.Updated = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool WithdrawMoney(Account account, decimal amount)
        {
            if (amount > 0)
            {
                decimal currentBalance = decimal.Parse(account.Balance);
                if (currentBalance >= amount)
                {
                    account.Balance = (currentBalance - amount).ToString();
                    account.Updated = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        public bool TransferMoney(Account senderAccount, Account receiverAccount, decimal amount)
        {
            if (amount > 0 && senderAccount != null && receiverAccount != null)
            {
                decimal senderBalance = decimal.Parse(senderAccount.Balance);
                if (senderBalance >= amount)
                {
                    senderAccount.Balance = (senderBalance - amount).ToString();
                    decimal receiverBalance = decimal.Parse(receiverAccount.Balance);
                    receiverAccount.Balance = (receiverBalance + amount).ToString();

                    senderAccount.Updated = DateTime.Now;
                    receiverAccount.Updated = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        private string GenerateAccountNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }

