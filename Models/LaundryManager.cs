using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBucketApp.Models
{
    public class LaundryManager
    {
        public LaundryManager()
        {
            TrxnDate = DateTime.Now;
            
            Guid nv = new Guid();

            ContractNo = nv.ToString();
            ShirtNo = 0;
            TrouserNo = 0;
            JeanNo = 0;
            AgbadaCompleteNo = 0;
        }
        public int ID { get; set; }

        [Display(Name = "Client ID")]
        public string ClientNo { get; set; }

        //auto generated
        [Display(Name = "Contract ID")]
        public string ContractNo { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "How many shirts?")]
        public byte ShirtNo { get; set; }

        [Display(Name = "How many trousers?")]
        public byte TrouserNo { get; set; }

        [Display(Name = "How many jeans?")]
        public byte JeanNo { get; set; }
        [Display(Name = "How many complete Agbada?")]
        public byte AgbadaCompleteNo { get; set; }

        [Display(Name = "Remark")]
        [DataType(DataType.MultilineText), MaxLength(400)]
        public string ClientRemark { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public IList<ImageDir> Imagelist { get; set; }

        [Display(Name = "Created by")]
        public string Email { get; set; }

        [Display(Name = "Office")]
        public string Branch { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime TrxnDate { get; set; }

    }
    public static class Constantsx
    {
        public const string ProductImagePath = "~/PropertyImages/";
        public const string ProductThumbnailPath = "~/PropertyImages/Thumbnails/";
    }
    public class ImageDir
    {
        public int ID { get; set; }

        [Display(Name = "Client ID")]
        public string ClientNo { get; set; }

       
        [Display(Name = "Contract ID")]
        public string ContractNo { get; set; }

        [Display(Name = "File Name")]
        public string DocPath { get; set; }

    }
    public class LaundryPayment
    {
        public LaundryPayment()
        {
            TrxnDate = DateTime.Now;
        }
        public int ID { get; set; }

        [Display(Name = "Client ID")]
        public string ClientNo { get; set; }

        [Display(Name = "Contract ID")]
        public string ContractNo { get; set; }
        [Display(Name = "Received Amount")]
        public decimal ReceivedAmount { get; set; }

        [Display(Name = "Amount Due")]
        public decimal ExpectedAmount { get; set; }

        [Display(Name = "Created by")]
        public string Email { get; set; }

        [Display(Name = "Office Branch")]
        public string Branch { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime TrxnDate { get; set; }
    }
    public class WashWorkFlow
    {
        public WashWorkFlow()
        {
            TrxnDate = DateTime.Now;
            StartDate = DateTime.Now;
        }
        public int ID { get; set; }

        [Display(Name = "Client ID")]
        public string ClientNo { get; set; }

        //auto generated
        [Display(Name = "Contract ID")]
        public string ContractNo { get; set; }

        [Display(Name = "status")]
        [DisplayFormat(NullDisplayText = "select status")]
        public WorkStatus? WorkStatus { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Created by")]
        public string Email { get; set; }

        [Display(Name = "Company")]
        public string Branch { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime TrxnDate { get; set; }
    }
    public enum WorkStatus
    {
        Pending,Washed,starched,Ironed,Completed
    }
}