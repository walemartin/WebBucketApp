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
    public class UploadAuditTrailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UploadAuditTrails
        public async Task<ActionResult> Index()
        {
            return View(await db.UploadAuditTrails.ToListAsync());
        }

        // GET: UploadAuditTrails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadAuditTrail uploadAuditTrail = await db.UploadAuditTrails.FindAsync(id);
            if (uploadAuditTrail == null)
            {
                return HttpNotFound();
            }
            return View(uploadAuditTrail);
        }

        // GET: UploadAuditTrails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadAuditTrails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,file,CreatedBy,UserIPAddress,PostedDate")] UploadAuditTrail uploadAuditTrail)
        {
            if (ModelState.IsValid)
            {
                db.UploadAuditTrails.Add(uploadAuditTrail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uploadAuditTrail);
        }

        // GET: UploadAuditTrails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadAuditTrail uploadAuditTrail = await db.UploadAuditTrails.FindAsync(id);
            if (uploadAuditTrail == null)
            {
                return HttpNotFound();
            }
            return View(uploadAuditTrail);
        }

        // POST: UploadAuditTrails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,file,CreatedBy,UserIPAddress,PostedDate")] UploadAuditTrail uploadAuditTrail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploadAuditTrail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uploadAuditTrail);
        }

        // GET: UploadAuditTrails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadAuditTrail uploadAuditTrail = await db.UploadAuditTrails.FindAsync(id);
            if (uploadAuditTrail == null)
            {
                return HttpNotFound();
            }
            return View(uploadAuditTrail);
        }

        // POST: UploadAuditTrails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UploadAuditTrail uploadAuditTrail = await db.UploadAuditTrails.FindAsync(id);
            db.UploadAuditTrails.Remove(uploadAuditTrail);
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
