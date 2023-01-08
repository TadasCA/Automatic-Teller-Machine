using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Teller_Machine.Models
{
    public class ATM
    {
        private Guid ATMguid;

        public Guid ATMGuid { get => ATMguid; set => ATMguid = value; }

        public string bankName;

        public ATM(Guid ATMguid, string bankName)
        {
            this.ATMGuid = ATMguid;
            this.bankName = bankName;
        }
    }
}
