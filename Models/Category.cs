using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBucketApp.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
    public class Sales
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Person { get; set; }
        public string Item { get; set; }
        public string Units { get; set; }
        public string UnitCost { get; set; }
        public string Total { get; set; }
        public string AddedOn { get; set; }
        public DataTable GetAllSale()
        {
            DataTable dt = new DataTable();
            //string strConString = @"Data Source=WELCOME-PC\SQLSERVER2008;Initial Catalog=MyDB;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from sale", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
    public class FetchARecordAuditTrail
    {
        public int ID { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string CreatedBy { get; set; }
        public string UserIPAddress { get; set; }
        public DateTime PostedDate { get; set; }

    }
    public class UploadAuditTrail
    {
        public int ID { get; set; }
        public string file { get; set; }
        public string CreatedBy { get; set; }
        public string UserIPAddress { get; set; }
        public DateTime PostedDate { get; set; }

    }
    public class FileExt : ValidationAttribute
    {
        public string Allow;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string extension = ((HttpPostedFileBase)value).FileName.Split('.')[1];
                if (Allow.Contains(extension))
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessage);
            }
            else
                return ValidationResult.Success;
        }
    }
    public class ImportExcel
    {
        [Required(ErrorMessage = "Please select file")]
        [FileExt(Allow = ".xls,.xlsx", ErrorMessage = "Only excel file")]
        public HttpPostedFileBase file { get; set; }
    }
}