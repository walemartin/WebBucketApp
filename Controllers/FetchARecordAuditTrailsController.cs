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

namespace WebBucketApp.Controllers
{
    public class FetchARecordAuditTrailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FetchARecordAuditTrails
        public async Task<ActionResult> Index()
        {
            return View(await db.FetchARecordAuditTrails.ToListAsync());
        }

        // GET: FetchARecordAuditTrails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FetchARecordAuditTrail fetchARecordAuditTrail = await db.FetchARecordAuditTrails.FindAsync(id);
            if (fetchARecordAuditTrail == null)
            {
                return HttpNotFound();
            }
            return View(fetchARecordAuditTrail);
        }

        // GET: FetchARecordAuditTrails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FetchARecordAuditTrails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientPhone,ClientEmail,CreatedBy,UserIPAddress,PostedDate")] FetchARecordAuditTrail fetchARecordAuditTrail)
        {
            if (ModelState.IsValid)
            {
                db.FetchARecordAuditTrails.Add(fetchARecordAuditTrail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fetchARecordAuditTrail);
        }

        // GET: FetchARecordAuditTrails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FetchARecordAuditTrail fetchARecordAuditTrail = await db.FetchARecordAuditTrails.FindAsync(id);
            if (fetchARecordAuditTrail == null)
            {
                return HttpNotFound();
            }
            return View(fetchARecordAuditTrail);
        }

        // POST: FetchARecordAuditTrails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientPhone,ClientEmail,CreatedBy,UserIPAddress,PostedDate")] FetchARecordAuditTrail fetchARecordAuditTrail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fetchARecordAuditTrail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fetchARecordAuditTrail);
        }

        // GET: FetchARecordAuditTrails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FetchARecordAuditTrail fetchARecordAuditTrail = await db.FetchARecordAuditTrails.FindAsync(id);
            if (fetchARecordAuditTrail == null)
            {
                return HttpNotFound();
            }
            return View(fetchARecordAuditTrail);
        }

        // POST: FetchARecordAuditTrails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FetchARecordAuditTrail fetchARecordAuditTrail = await db.FetchARecordAuditTrails.FindAsync(id);
            db.FetchARecordAuditTrails.Remove(fetchARecordAuditTrail);
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
