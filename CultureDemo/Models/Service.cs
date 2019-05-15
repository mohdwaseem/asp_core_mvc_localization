using System;
using System.Collections.Generic;

namespace CultureDemo.Models
{
    public partial class Service
    {
        public Service()
        {
            ContractService = new HashSet<ContractService>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public virtual ICollection<ContractService> ContractService { get; set; }
    }
}
