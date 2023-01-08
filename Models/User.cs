using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Models
{
   public class User
    {
        public int cardNumber;
        public string Iban;
        private double balance;


        public double Balance { get => balance; set => balance = value; }
        private string password;
        public string Password { get => password; set => password = value; }
        public User(int cardNumber, string Iban, double balance, string password)
        {
            this.cardNumber = cardNumber;
            this.Iban = Iban;
            this.Balance = balance;
            this.Password = password;
        }
    }
}
