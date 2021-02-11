using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBucketApp.Models;

namespace WebBucketApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FetchSales(int customerId)
        {
            string query = "SELECT FROM Sale WHERE Id=@CustomerId";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Sales employee = GetSaleData(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        //Get the details of a particular employee  
        public Sales GetSaleData(int? id)
        {
            Sales DataSales = new Sales();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sqlQuery = "SELECT * FROM Sale WHERE Id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();



                while (rdr.Read())
                {
                    DataSales.Id = Convert.ToInt32(rdr["Id"]);
                    DataSales.Region = rdr["Region"].ToString();
                    DataSales.Person = rdr["Person"].ToString();
                    DataSales.Item = rdr["Item"].ToString();
                    DataSales.Units = rdr["Units"].ToString();
                    DataSales.UnitCost = rdr["UnitCost"].ToString();
                    DataSales.Total = rdr["Total"].ToString();
                    DataSales.AddedOn = rdr["AddedOn"].ToString();
                }

            }
            return DataSales;
        }
        public ActionResult SheetUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SheetUpload(ImportExcel importExcel)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Upload/" + importExcel.file.FileName);
                importExcel.file.SaveAs(path);

                string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

                //Sheet Name
                excelConnection.Open();
                string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                excelConnection.Close();
                //End

                OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);

                excelConnection.Open();

                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
                {

                    //Give your Destination table name
                    DestinationTableName = "sale"
                };

                //Mappings
                sqlBulk.ColumnMappings.Add("Date", "AddedOn");
                sqlBulk.ColumnMappings.Add("Region", "Region");
                sqlBulk.ColumnMappings.Add("Person", "Person");
                sqlBulk.ColumnMappings.Add("Item", "Item");
                sqlBulk.ColumnMappings.Add("Units", "Units");
                sqlBulk.ColumnMappings.Add("Unit Cost", "UnitCost");
                sqlBulk.ColumnMappings.Add("Total", "Total");

                sqlBulk.WriteToServer(dReader);
                excelConnection.Close();

                ViewBag.Result = "Successfully Imported";
            }
            return View();
        }
    }
}