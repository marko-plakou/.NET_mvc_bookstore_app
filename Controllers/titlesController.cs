using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookstore_app.Models;
using Microsoft.Win32;

namespace bookstore_app.Controllers

{
    [Authorize]
    public class titlesController : Controller
    {
        private pubsEntities2 db = new pubsEntities2();

        // GET: titles
        public ActionResult Index()
        {
            var titles = db.titles.Include(t => t.publishers).Include(t => t.roysched);
            return View(titles.ToList());
        }

        // GET: titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // GET: titles/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id");
            return View();
        }

        // POST: titles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,title,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] titles titles)
        {
            if (ModelState.IsValid)
            {
                db.titles.Add(titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // GET: titles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // POST: titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,title,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] titles titles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // GET: titles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // POST: titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            titles titles = db.titles.Find(id);
            db.titles.Remove(titles);
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
