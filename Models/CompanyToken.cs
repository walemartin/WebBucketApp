using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using WebBucketApp.ViewModels;

namespace WebBucketApp.Models
{
    public class CompanyToken
    {
        
         
        public CompanyToken()
        {
            Random n = new Random();
            TrxnDate = DateTime.Now;
            CompToken = "RSC" + n.Next(999,999999999);
        }
        public int ID { get; set; }

        [Display(Name = "Company Name")]
        public string Branch { get; set; }

        [Display(Name = "Token")]
        public string CompToken { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Created by")]
        public string Email { get; set; }

      

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TrxnDate { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
        public virtual ICollection<RegisterViewModel> RegisterViewModel { get; set; }
        public virtual ICollection<EditUserViewModel> EditUserViewModels { get; set; }
    }
}