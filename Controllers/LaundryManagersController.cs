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
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using System.Text;

namespace WebBucketApp.Controllers
{
    [Authorize(Roles = "Admin,Company")]
    public class LaundryManagersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public LaundryManagersController()
        {
        }
        public LaundryManagersController(ApplicationUserManager userManager, ApplicationRoleManager
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
        // GET: LaundryManagers
        public async Task<ActionResult> Index()
        {
            return View(await db.LaundryManagers.ToListAsync());
        }

        // GET: LaundryManagers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryManager laundryManager = await db.LaundryManagers.FindAsync(id);
            if (laundryManager == null)
            {
                return HttpNotFound();
            }
            return View(laundryManager);
        }
        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) &&
            allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 190)
            {
                img.Resize(190, img.Height);
            }
            img.Save(Constantsx.ProductImagePath + file.FileName);
            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }
            img.Save(Constantsx.ProductThumbnailPath + file.FileName);
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
        public async Task<ActionResult> ClientQuery(string Search)
        {
            //System.Threading.Thread.Sleep(1000);
            var valMgts = db.LaundryManagers.Where(s =>  s.ClientNo.StartsWith(Search.Trim()) || s.ContractNo.StartsWith(Search.Trim()));

            //using (StreamWriter streamWriter = new StreamWriter(@"Z:\Logger\" + $"{DateTime.Now:ddMMyyy}" + ".txt", true))
            //{
            //    streamWriter.WriteLine(DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + " : " + Search + " was queried against the database by " + User.Identity.Name + " and  IP: " + Request.UserHostAddress);
            //}


            return View(await valMgts.ToListAsync());
        }
        public ActionResult ClientIndex()
        {
            return View();
        }
        // GET: LaundryManagers/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fg = db.ClientDetails.Find(id);
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            LaundryManager ng = new LaundryManager()
            {
                ClientNo = fg.ClientNo,
                FullName = fg.FullName,
                Email = user.Email,
                Branch=user.CompanyToken.Branch,
                ContractNo= RandomPassword(),
            };
            if (ng == null)
            {
                return HttpNotFound();
            }
            return View(ng);
        }

        // POST: LaundryManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientNo,ContractNo,FullName,ShirtNo,TrouserNo,JeanNo,AgbadaCompleteNo,ClientRemark,IsActive,Email,Branch,TrxnDate")] LaundryManager laundryManager)
        {
            if (ModelState.IsValid)
            {
                db.LaundryManagers.Add(laundryManager);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(laundryManager);
        }

        // GET: LaundryManagers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryManager laundryManager = await db.LaundryManagers.FindAsync(id);
            if (laundryManager == null)
            {
                return HttpNotFound();
            }
            return View(laundryManager);
        }

        // POST: LaundryManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientNo,ContractNo,FullName,ShirtNo,TrouserNo,JeanNo,AgbadaCompleteNo,ClientRemark,IsActive,Email,Branch,TrxnDate")] LaundryManager laundryManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laundryManager).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(laundryManager);
        }

        // GET: LaundryManagers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryManager laundryManager = await db.LaundryManagers.FindAsync(id);
            if (laundryManager == null)
            {
                return HttpNotFound();
            }
            return View(laundryManager);
        }

        // POST: LaundryManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LaundryManager laundryManager = await db.LaundryManagers.FindAsync(id);
            db.LaundryManagers.Remove(laundryManager);
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
