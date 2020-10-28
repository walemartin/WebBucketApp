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

namespace WebBucketApp.Controllers
{
    public class CompanyTokensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public CompanyTokensController()
        {
        }
        public CompanyTokensController(ApplicationUserManager userManager, ApplicationRoleManager
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
        // GET: CompanyTokens
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyTokens.ToListAsync());
        }

        // GET: CompanyTokens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyToken companyToken = await db.CompanyTokens.FindAsync(id);
            if (companyToken == null)
            {
                return HttpNotFound();
            }
            return View(companyToken);
        }

        // GET: CompanyTokens/Create
        public ActionResult Create()
        {
            CompanyToken jk = new CompanyToken()
            {
                TrxnDate = DateTime.Now,
            };
            return View(jk);
        }

        // POST: CompanyTokens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Branch,CompToken,ExpirationDate,Email,TrxnDate")] CompanyToken companyToken)
        {
            if (ModelState.IsValid)
            {
                db.CompanyTokens.Add(companyToken);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyToken);
        }

        // GET: CompanyTokens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyToken companyToken = await db.CompanyTokens.FindAsync(id);
            if (companyToken == null)
            {
                return HttpNotFound();
            }
            return View(companyToken);
        }

        // POST: CompanyTokens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Branch,CompToken,ExpirationDate,Email,TrxnDate")] CompanyToken companyToken)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyToken).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyToken);
        }

        // GET: CompanyTokens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyToken companyToken = await db.CompanyTokens.FindAsync(id);
            if (companyToken == null)
            {
                return HttpNotFound();
            }
            return View(companyToken);
        }

        // POST: CompanyTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyToken companyToken = await db.CompanyTokens.FindAsync(id);
            db.CompanyTokens.Remove(companyToken);
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
