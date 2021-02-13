using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBucketApp.Models;

namespace WebBucketApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
        public PartialViewResult ReportPage() 
        {
            Thread.Sleep(4000);
            return PartialView("_LabReport");
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
        

        public ActionResult GoogleSheetCall()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
             string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
             string ApplicationName = "Google Sheets API .NET Quickstart";

           
                UserCredential credential;

                using (var stream =
                    new FileStream(Server.MapPath(@"~/credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = Server.MapPath(@"~/token.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Response.Write("Credential file saved to: " + credPath);
                }

                // Create Google Sheets API service.
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define request parameters.
                String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
                String range = "Class Data!A2:E";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);

                // Prints the names and majors of students in a sample spreadsheet:
                // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
                ValueRange response = request.Execute();
            //IList<IList<Object>> values = response.Values;
            return View();
                
        }
      
        public ActionResult SheetSearchById()
        {
            return View();
        }
       
        public ActionResult SheetUpload()
        {
            return View();
        }
       
        public ActionResult LabRepots()
        {
            Sales model = new Sales();
            DataTable dt = model.GetAllSale();
            return View("Home", dt);
        }
        [HttpPost]
        public ActionResult SheetUpload(ImportExcel importExcel, UploadAuditTrail uploadAuditTrail)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Upload/" + importExcel.file.FileName);
                uploadAuditTrail.file = importExcel.file.FileName;
                uploadAuditTrail.PostedDate = DateTime.Now;
                uploadAuditTrail.UserIPAddress = Request.UserHostAddress;
                uploadAuditTrail.CreatedBy = "wale";
                db.UploadAuditTrails.Add(uploadAuditTrail);
                db.SaveChanges();
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //Fetch the File Name.

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