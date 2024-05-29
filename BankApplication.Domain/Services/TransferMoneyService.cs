namespace BankApplication.Domain;
using BankApplication.Domain.Entities;
    public class TransferMoneyService
    {
        private AccountService accountService;

        public TransferMoneyService(AccountService accountService)
        {
            this.accountService = accountService;
        }

        public bool TransferMoney(string senderAccountNumber, string receiverAccountNumber, decimal amount)
        {
            Account senderAccount = accountService.GetAccountByNumber(senderAccountNumber);
            Account receiverAccount = accountService.GetAccountByNumber(receiverAccountNumber);

            if (senderAccount != null && receiverAccount != null && amount > 0)
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
    }
