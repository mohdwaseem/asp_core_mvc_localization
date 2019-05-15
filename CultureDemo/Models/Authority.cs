using System;
using System.Collections.Generic;

namespace CultureDemo.Models
{
    public partial class Authority
    {
        public Authority()
        {
            ContractService = new HashSet<ContractService>();
        }

        public int AuthorityId { get; set; }
        public string AuthorityName { get; set; }

        public virtual ICollection<ContractService> ContractService { get; set; }
    }
}
