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
    public class LaundryPaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public LaundryPaymentsController()
        {
        }
        public LaundryPaymentsController(ApplicationUserManager userManager, ApplicationRoleManager
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
        // GET: LaundryPayments
        public async Task<ActionResult> Index()
        {
            return View(await db.LaundryPayments.ToListAsync());
        }

        // GET: LaundryPayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryPayment laundryPayment = await db.LaundryPayments.FindAsync(id);
            if (laundryPayment == null)
            {
                return HttpNotFound();
            }
            return View(laundryPayment);
        }

        // GET: LaundryPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LaundryPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientNo,ContractNo,ReceivedAmount,ExpectedAmount,Email,Branch,TrxnDate")] LaundryPayment laundryPayment)
        {
            if (ModelState.IsValid)
            {
                db.LaundryPayments.Add(laundryPayment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(laundryPayment);
        }

        // GET: LaundryPayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryPayment laundryPayment = await db.LaundryPayments.FindAsync(id);
            if (laundryPayment == null)
            {
                return HttpNotFound();
            }
            return View(laundryPayment);
        }

        // POST: LaundryPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientNo,ContractNo,ReceivedAmount,ExpectedAmount,Email,Branch,TrxnDate")] LaundryPayment laundryPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laundryPayment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(laundryPayment);
        }

        // GET: LaundryPayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaundryPayment laundryPayment = await db.LaundryPayments.FindAsync(id);
            if (laundryPayment == null)
            {
                return HttpNotFound();
            }
            return View(laundryPayment);
        }

        // POST: LaundryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LaundryPayment laundryPayment = await db.LaundryPayments.FindAsync(id);
            db.LaundryPayments.Remove(laundryPayment);
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
