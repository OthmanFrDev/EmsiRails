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
    public class garesController : Controller
    {
        private ExpresstrainEntities db = new ExpresstrainEntities();

        // GET: gares
        public ActionResult Index()
        {
            var gares = db.gares.Include(g => g.ville);
            return View(gares.ToList());
        }

        // GET: gares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gare gare = db.gares.Find(id);
            if (gare == null)
            {
                return HttpNotFound();
            }
            return View(gare);
        }

        // GET: gares/Create
        public ActionResult Create()
        {
            ViewBag.idville = new SelectList(db.villes, "idville", "nom");
            return View();
        }

        // POST: gares/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idgare,libelle,idville")] gare gare)
        {
            if (ModelState.IsValid)
            {
                db.gares.Add(gare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idville = new SelectList(db.villes, "idville", "nom", gare.idville);
            return View(gare);
        }

        // GET: gares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gare gare = db.gares.Find(id);
            if (gare == null)
            {
                return HttpNotFound();
            }
            ViewBag.idville = new SelectList(db.villes, "idville", "nom", gare.idville);
            return View(gare);
        }

        // POST: gares/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idgare,libelle,idville")] gare gare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idville = new SelectList(db.villes, "idville", "nom", gare.idville);
            return View(gare);
        }

        // GET: gares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gare gare = db.gares.Find(id);
            if (gare == null)
            {
                return HttpNotFound();
            }
            return View(gare);
        }

        // POST: gares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gare gare = db.gares.Find(id);
            db.gares.Remove(gare);
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
