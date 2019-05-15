using System;
using System.Collections.Generic;

namespace CultureDemo.Models
{
    public partial class Bank
    {
        public Bank()
        {
            ContractService = new HashSet<ContractService>();
        }

        public int BankId { get; set; }
        public string BankName { get; set; }

        public virtual ICollection<ContractService> ContractService { get; set; }
    }
}
