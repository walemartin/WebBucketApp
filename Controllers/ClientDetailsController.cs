using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBucketApp;
using WebBucketApp.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Text;

namespace WebBucketApp.Controllers
{
    [Authorize(Roles = "Admin,Company")]
    public class ClientDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ClientDetailsController()
        {
        }
        public ClientDetailsController(ApplicationUserManager userManager, ApplicationRoleManager
        roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //public ActionResult Index(string category, string search)
        //{
           
        //    if (!String.IsNullOrEmpty(category))
        //    {
        //        var lk = db.CompanyTokens.Where(a => a.Branch == category);
        //    }
        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        var po = db.ClientDetails.Where(p => p.ClientNo.Contains(search) ||
        //        p.FullName.Contains(search) ||
        //        p.PhoneNumber.Contains(search));
        //        ViewBag.Search = search;
        //    }
        //    var categories = db.CompanyTokens.OrderBy(p => p.Branch).Select(p =>
        //    p.Branch).Distinct();
        //    if (!String.IsNullOrEmpty(category))
        //    {
        //        var kp = db.ClientDetails.Where(a => a.Branch == category);
        //    }
        //    ViewBag.Category = new SelectList(categories);
        //    return View(categories.ToList());
        //}
        public async Task<ActionResult> ClientQuery(string Search)
        {
            //System.Threading.Thread.Sleep(1000);
            var valMgts = db.ClientDetails.Where(s => s.LastName.StartsWith(Search.Trim()) || s.ClientNo.StartsWith(Search.Trim()) || s.FirstName.StartsWith(Search.Trim()) || s.PhoneNumber.StartsWith(Search.Trim()));

            //using (StreamWriter streamWriter = new StreamWriter(@"Z:\Logger\" + $"{DateTime.Now:ddMMyyy}" + ".txt", true))
            //{
            //    streamWriter.WriteLine(DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + " : " + Search + " was queried against the database by " + User.Identity.Name + " and  IP: " + Request.UserHostAddress);
            //}


            return View(await valMgts.ToListAsync());
        }

        // GET: ClientDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.ClientDetails.ToListAsync());
        }
        public ActionResult ClientIndex()
        {
            return View();
        }
        // GET: ClientDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = await db.ClientDetails.FindAsync(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // Instantiate random number generator.  
        // It is better to keep a single Random instance 
        // and keep using Next on the same instance.  
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        // Generates a random password.  
        // 4-LowerCase + 4-Digits + 2-UpperCase  
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }

        // GET: ClientDetails/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            
            ClientDetail ng = new ClientDetail()
            {
                TrxnDate = DateTime.Today,
                Branch = user.CompanyToken.Branch,
                Email = user.Email,
                ClientNo= RandomString(10),
            };
            return View(ng);
        }

        // POST: ClientDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientNo,FirstName,LastName,Gender,HomeAddress,CiientEmail,PhoneNumber,IsActive,Email,Branch,TrxnDate")] ClientDetail clientDetail)
        {
            if (ModelState.IsValid)
            {
                db.ClientDetails.Add(clientDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clientDetail);
        }

        // GET: ClientDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = await db.ClientDetails.FindAsync(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // POST: ClientDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientNo,FirstName,LastName,Gender,HomeAddress,CiientEmail,PhoneNumber,IsActive,Email,Branch,TrxnDate")] ClientDetail clientDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clientDetail);
        }

        // GET: ClientDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDetail clientDetail = await db.ClientDetails.FindAsync(id);
            if (clientDetail == null)
            {
                return HttpNotFound();
            }
            return View(clientDetail);
        }

        // POST: ClientDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClientDetail clientDetail = await db.ClientDetails.FindAsync(id);
            db.ClientDetails.Remove(clientDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
