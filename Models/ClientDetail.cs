using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBucketApp.Models
{
    public class ClientDetail
    {
        public ClientDetail()
        {
            PostDate = DateTime.Now;
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

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Office Branch")]
        public string Branch { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        public DateTime PostDate { get; set; }
    }
    public enum Gender
    {
        Male,Female
    }
}