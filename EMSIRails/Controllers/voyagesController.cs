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
    public class voyagesController : Controller
    {
        private ExpresstrainEntities db = new ExpresstrainEntities();

        // GET: voyages
        public ActionResult Index()
        {
            var voyages = db.voyages.Include(v => v.gare).Include(v => v.gare1).Include(v => v.train);
            return View(voyages.ToList());
        }

        // GET: voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voyage voyage = db.voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // GET: voyages/Create
        public ActionResult Create()
        {
            ViewBag.gareArrive = new SelectList(db.gares, "idgare", "libelle");
            ViewBag.GareDepart = new SelectList(db.gares, "idgare", "libelle");
            ViewBag.idtrain = new SelectList(db.trains, "idtrain", "libelle");
            return View();
        }

        // POST: voyages/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idvoyage,GareDepart,gareArrive,dateDepart,dateArrive,HeureDepart,heureArrive,idtrain")] voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.voyages.Add(voyage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gareArrive = new SelectList(db.gares, "idgare", "libelle", voyage.gareArrive);
            ViewBag.GareDepart = new SelectList(db.gares, "idgare", "libelle", voyage.GareDepart);
            ViewBag.idtrain = new SelectList(db.trains, "idtrain", "libelle", voyage.idtrain);
            return View(voyage);
        }

        // GET: voyages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voyage voyage = db.voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            ViewBag.gareArrive = new SelectList(db.gares, "idgare", "libelle", voyage.gareArrive);
            ViewBag.GareDepart = new SelectList(db.gares, "idgare", "libelle", voyage.GareDepart);
            ViewBag.idtrain = new SelectList(db.trains, "idtrain", "libelle", voyage.idtrain);
            return View(voyage);
        }

        // POST: voyages/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idvoyage,GareDepart,gareArrive,dateDepart,dateArrive,HeureDepart,heureArrive,idtrain")] voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voyage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gareArrive = new SelectList(db.gares, "idgare", "libelle", voyage.gareArrive);
            ViewBag.GareDepart = new SelectList(db.gares, "idgare", "libelle", voyage.GareDepart);
            ViewBag.idtrain = new SelectList(db.trains, "idtrain", "libelle", voyage.idtrain);
            return View(voyage);
        }

        // GET: voyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voyage voyage = db.voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // POST: voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            voyage voyage = db.voyages.Find(id);
            db.voyages.Remove(voyage);
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
