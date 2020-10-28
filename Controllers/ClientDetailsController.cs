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
        // GET: ClientDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.ClientDetails.ToListAsync());
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

        // GET: ClientDetails/Create
        public ActionResult Create()
        {
            return View();
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
