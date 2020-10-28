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
    public class WashWorkFlowsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public WashWorkFlowsController()
        {
        }
        public WashWorkFlowsController(ApplicationUserManager userManager, ApplicationRoleManager
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
        // GET: WashWorkFlows
        public async Task<ActionResult> Index()
        {
            return View(await db.WashWorkFlows.ToListAsync());
        }

        // GET: WashWorkFlows/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WashWorkFlow washWorkFlow = await db.WashWorkFlows.FindAsync(id);
            if (washWorkFlow == null)
            {
                return HttpNotFound();
            }
            return View(washWorkFlow);
        }

        // GET: WashWorkFlows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WashWorkFlows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientNo,ContractNo,WorkStatus,StartDate,EndDate,Email,Branch,TrxnDate")] WashWorkFlow washWorkFlow)
        {
            if (ModelState.IsValid)
            {
                db.WashWorkFlows.Add(washWorkFlow);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(washWorkFlow);
        }

        // GET: WashWorkFlows/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WashWorkFlow washWorkFlow = await db.WashWorkFlows.FindAsync(id);
            if (washWorkFlow == null)
            {
                return HttpNotFound();
            }
            return View(washWorkFlow);
        }

        // POST: WashWorkFlows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientNo,ContractNo,WorkStatus,StartDate,EndDate,Email,Branch,TrxnDate")] WashWorkFlow washWorkFlow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(washWorkFlow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(washWorkFlow);
        }

        // GET: WashWorkFlows/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WashWorkFlow washWorkFlow = await db.WashWorkFlows.FindAsync(id);
            if (washWorkFlow == null)
            {
                return HttpNotFound();
            }
            return View(washWorkFlow);
        }

        // POST: WashWorkFlows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WashWorkFlow washWorkFlow = await db.WashWorkFlows.FindAsync(id);
            db.WashWorkFlows.Remove(washWorkFlow);
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
