using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CultureDemo.ViewModels
{
    public class ServiceProfileVM
    {
        public int ContracServiceId { get; set; }
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? AuthorityId { get; set; }
        [BindRequired]
        [Required]
        public int? ServiceId { get; set; }
        public int? BankId { get; set; }

        public string Authority { get; set; }
        public string Bank { get; set; }
        public string Service { get; set; }

        public SelectList AuthorityList { get; set; }
        public SelectList BankList { get; set; }
        public SelectList ServiceList { get; set; }
    }
}
