using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Models
{
    public class BankCard
    {
        private Guid BCguid;

        public Guid BCGuid { get => BCguid; set => BCguid = value; }
        public int cardNumber;

        public BankCard(Guid BCguid, int cardNumber)
        {
            this.BCguid = BCguid;
            this.cardNumber = cardNumber;
        }
    }
}
