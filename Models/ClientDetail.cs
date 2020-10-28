using System;
using System.ComponentModel.DataAnnotations;

namespace WebBucketApp.Models
{
    public class ClientDetail
    {
        public ClientDetail()
        {
            TrxnDate = DateTime.Now;
            IsActive = true;
        }
        public int ID { get; set; }

        //auto generated
        [Display(Name = "Client ID")]
        public string ClientNo { get; set; }

       
        [Display(Name = "First name"), StringLength(50, MinimumLength = 0)]
        public string FirstName { get; set; }

        [Display(Name = "Surname"), StringLength(50, MinimumLength = 0)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Gender")]
        [DisplayFormat(NullDisplayText = "select gender")]
        public Gender? Gender { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText), MaxLength(250)]
        public string HomeAddress { get; set; }

        [Display(Name = "Client Email")]
        [DataType(DataType.EmailAddress)]
        public string CiientEmail { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Created by")]
        public string Email { get; set; }

        [Display(Name = "Office Branch")]
        public string Branch { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime TrxnDate { get; set; }
    }
    public enum Gender
    {
        Male,Female
    }
}