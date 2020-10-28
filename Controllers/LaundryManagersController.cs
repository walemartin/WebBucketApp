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

        // GET: LaundryManagers/Create
        public ActionResult Create()
        {
            return View();
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
