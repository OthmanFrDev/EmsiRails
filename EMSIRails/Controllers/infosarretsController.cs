using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMSIRails.Models;

namespace EMSIRails.Controllers
{
    public class infosarretsController : Controller
    {
        private ExpresstrainEntities db = new ExpresstrainEntities();

        // GET: infosarrets
        public ActionResult Index()
        {
            var infosarrets = db.infosarrets.Include(i => i.gare).Include(i => i.voyage);
            return View(infosarrets.ToList());
        }

        // GET: infosarrets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infosarret infosarret = db.infosarrets.Find(id);
            if (infosarret == null)
            {
                return HttpNotFound();
            }
            return View(infosarret);
        }

        // GET: infosarrets/Create
        public ActionResult Create()
        {
            ViewBag.idgare = new SelectList(db.gares, "idgare", "libelle");
            ViewBag.idvoyage = new SelectList(db.voyages, "idvoyage", "idvoyage");
            return View();
        }

        // POST: infosarrets/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idarret,idvoyage,idgare,heureArrive,heureDepart")] infosarret infosarret)
        {
            if (ModelState.IsValid)
            {
                db.infosarrets.Add(infosarret);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idgare = new SelectList(db.gares, "idgare", "libelle", infosarret.idgare);
            ViewBag.idvoyage = new SelectList(db.voyages, "idvoyage", "idvoyage", infosarret.idvoyage);
            return View(infosarret);
        }

        // GET: infosarrets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infosarret infosarret = db.infosarrets.Find(id);
            if (infosarret == null)
            {
                return HttpNotFound();
            }
            ViewBag.idgare = new SelectList(db.gares, "idgare", "libelle", infosarret.idgare);
            ViewBag.idvoyage = new SelectList(db.voyages, "idvoyage", "idvoyage", infosarret.idvoyage);
            return View(infosarret);
        }

        // POST: infosarrets/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idarret,idvoyage,idgare,heureArrive,heureDepart")] infosarret infosarret)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infosarret).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idgare = new SelectList(db.gares, "idgare", "libelle", infosarret.idgare);
            ViewBag.idvoyage = new SelectList(db.voyages, "idvoyage", "idvoyage", infosarret.idvoyage);
            return View(infosarret);
        }

        // GET: infosarrets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            infosarret infosarret = db.infosarrets.Find(id);
            if (infosarret == null)
            {
                return HttpNotFound();
            }
            return View(infosarret);
        }

        // POST: infosarrets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            infosarret infosarret = db.infosarrets.Find(id);
            db.infosarrets.Remove(infosarret);
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
