using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailConfirmed { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Balance { get; set; }
        public string PhoneNumber {  get; set; }
        public string IdentityNumber { get; set; }
        public string AccountNumber { get; set; }
    }
}
