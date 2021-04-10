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
    public class storesController : Controller
    {
        private pubsEntities2 db = new pubsEntities2();

        // GET: stores
        public ActionResult Index()
        {
            return View(db.stores.ToList());
        }

        // GET: stores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // GET: stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.stores.Add(stores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stores);
        }

        // GET: stores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stores);
        }

        // GET: stores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            stores stores = db.stores.Find(id);
            db.stores.Remove(stores);
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
