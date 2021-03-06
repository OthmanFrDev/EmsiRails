﻿using System;
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
    public class villesController : Controller
    {
        private ExpresstrainEntities db = new ExpresstrainEntities();

        // GET: villes
        public ActionResult Index()
        {
            return View(db.villes.ToList());
        }

        // GET: villes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ville ville = db.villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // GET: villes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: villes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idville,nom")] ville ville)
        {
            if (ModelState.IsValid)
            {
                db.villes.Add(ville);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ville);
        }

        // GET: villes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ville ville = db.villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: villes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idville,nom")] ville ville)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ville).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ville);
        }

        // GET: villes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ville ville = db.villes.Find(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: villes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ville ville = db.villes.Find(id);
            db.villes.Remove(ville);
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
