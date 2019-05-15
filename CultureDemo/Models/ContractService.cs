using System;
using System.Collections.Generic;

namespace CultureDemo.Models
{
    public partial class ContractService
    {
        public int ContracServiceId { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? AuthorityId { get; set; }
        public int? ServiceId { get; set; }
        public int? BankId { get; set; }

        public virtual Authority Authority { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual Service Service { get; set; }
    }
}
