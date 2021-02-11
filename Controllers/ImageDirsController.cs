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
    [Authorize]
    public class ImageDirsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ImageDirs
        public async Task<ActionResult> Index()
        {
            return View(await db.ImageDirs.ToListAsync());
        }

        // GET: ImageDirs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageDir imageDir = await db.ImageDirs.FindAsync(id);
            if (imageDir == null)
            {
                return HttpNotFound();
            }
            return View(imageDir);
        }

        // GET: ImageDirs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageDirs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientNo,ContractNo,DocPath")] ImageDir imageDir)
        {
            if (ModelState.IsValid)
            {
                db.ImageDirs.Add(imageDir);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(imageDir);
        }

        // GET: ImageDirs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageDir imageDir = await db.ImageDirs.FindAsync(id);
            if (imageDir == null)
            {
                return HttpNotFound();
            }
            return View(imageDir);
        }

        // POST: ImageDirs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientNo,ContractNo,DocPath")] ImageDir imageDir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageDir).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(imageDir);
        }

        // GET: ImageDirs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageDir imageDir = await db.ImageDirs.FindAsync(id);
            if (imageDir == null)
            {
                return HttpNotFound();
            }
            return View(imageDir);
        }

        // POST: ImageDirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ImageDir imageDir = await db.ImageDirs.FindAsync(id);
            db.ImageDirs.Remove(imageDir);
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
