using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookstore_app.Models;

namespace bookstore_app.Controllers
{
    [Authorize]
    public class royschedsController : Controller
    {
        private pubsEntities2 db = new pubsEntities2();

        // GET: royscheds
        public ActionResult Index()
        {
            var roysched = db.roysched.Include(r => r.titles);
            return View(roysched.ToList());
        }

        // GET: royscheds/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
        }

        // GET: royscheds/Create
        public ActionResult Create()
        {
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: royscheds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,lorange,hirange,royalty")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.roysched.Add(roysched);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // GET: royscheds/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return HttpNotFound();
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // POST: royscheds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,lorange,hirange,royalty")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roysched).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // GET: royscheds/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
        }

        // POST: royscheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            roysched roysched = db.roysched.Find(id);
            db.roysched.Remove(roysched);
            db.SaveChanges();
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
