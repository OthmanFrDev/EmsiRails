using EMSIRails.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EMSIRails.Controllers
{
    public class HomeController : Controller
    {
        private ExpresstrainEntities db = new ExpresstrainEntities();

        public ActionResult Index()
        {
            var gares = db.gares;
            return View(gares.ToList());
        }
        public ActionResult Rechercher(int Depart, int Arrive, DateTime d)
        {
            var voyageSimple = db.voyages.Include(g => g.gare).Where(a => a.GareDepart == Depart && a.gareArrive == Arrive && a.dateDepart == d);
            /* */
            if (voyageSimple.Count() == 0 ) {
                List<Correspandance> listeCorrespandance = new List<Correspandance>();
                var voyageCorrespandanceDepart = db.voyages.Include(g => g.gare).Where(a => a.GareDepart == Depart && a.dateDepart == d).ToList();
                foreach (var item in voyageCorrespandanceDepart)
                {
                    var voyageCorrespandanceArrive = db.voyages.Include(g => g.gare).Where(a => a.GareDepart == item.gareArrive && a.dateDepart == d && a.gareArrive == Arrive).FirstOrDefault();
                    listeCorrespandance.Add(new Correspandance()
                    {
                        departCor = item,
                        ArriveCor = voyageCorrespandanceArrive
                    });
                }
                ViewBag.correspandance = listeCorrespandance.ToList();
            }
            if (voyageSimple.Count() != 0)
            {
                ViewBag.voyageSimple = voyageSimple.ToList();
            }
            else
            {
                var voyageArret = (from v in db.voyages
                                   join a in db.infosarrets
                                   on v.idvoyage equals a.idvoyage
                                   where a.idgare == Depart || a.idgare == Arrive
                                   group v by new { v.idvoyage, v.idtrain } into newVoyage
                                   where newVoyage.Count() == 2
                                   select new VoyageArret()
                                   {
                                       idvoyage = (int)newVoyage.Key.idvoyage,
                                       idtrain = (int)newVoyage.Key.idtrain
                                   }).ToList();
                List<VoyageArret> listeVA = new List<VoyageArret>();
                foreach (var item in voyageArret)
                {
                    var voyageEscaleD = db.infosarrets.Where(ve => ve.idvoyage == item.idvoyage && ve.idgare == Depart).FirstOrDefault();
                    var voyageEscaleA = db.infosarrets.Where(ve => ve.idvoyage == item.idvoyage && ve.idgare == Arrive).FirstOrDefault();
                    if (voyageEscaleA.idarret > voyageEscaleD.idarret)
                    {
                        listeVA.Add(new VoyageArret()
                        {
                            idvoyage = item.idvoyage,
                            idtrain = item.idtrain,
                            villeDepart = (from g in db.gares where g.idgare == voyageEscaleD.idgare select g.libelle).FirstOrDefault(),
                            villeArrive = (from g in db.gares where g.idgare == voyageEscaleA.idgare select g.libelle).FirstOrDefault(),
                            heureDepart = voyageEscaleD.heureArrive,
                            heureArrive = voyageEscaleA.heureArrive
                        });
                    }
                }
                ViewBag.listeva = listeVA.ToList();
            }
            return View();
        }

    }
}