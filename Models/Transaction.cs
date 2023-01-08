using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Models
{
    public class Transaction
    {
        private int trnID;
        public int TrnID { get => trnID; set => trnID = value; }

        private DateTime date;
        public DateTime Date { get => date; set => date = value; }

        private User user;
        public User User { get => user; set => user = value; }

        private BankCard bankCard;

        public BankCard BankCard { get => bankCard; set => bankCard = value; }

        private Int32 amount;
        public int Amount { get => amount; set => amount = value; }

        public Transaction(int trnID, DateTime date, User user, BankCard bankCard, Int32 amount)
        {
            this.TrnID = trnID;
            this.Date = date;
            this.User = user;
            this.BankCard = bankCard;
            this.Amount = amount;
        }
    }
}
