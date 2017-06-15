using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Models;
using DatabaseAccessLayer;

namespace ENETCareWebApp.Controllers
{
    public class EnetInterventionTypesController : Controller
    {
        private EnetCareEntities db = new EnetCareEntities();
        private InterventionService interventions;

        public EnetInterventionTypesController()
        {
            interventions = new InterventionService();
        }

        // GET: EnetInterventionTypes
        public ActionResult Index()
        {
            return View(db.EnetInterventionTypes.ToList());
        }

        // GET: EnetInterventionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetInterventionType enetInterventionType = db.EnetInterventionTypes.Find(id);
            if (enetInterventionType == null)
            {
                return HttpNotFound();
            }
            return View(enetInterventionType);
        }

        // GET: EnetInterventionTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnetInterventionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterventionTypeId,Name,LabourHours,Cost")] EnetInterventionType enetInterventionType)
        {
            if (ModelState.IsValid)
            {
                db.EnetInterventionTypes.Add(enetInterventionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enetInterventionType);
        }

        // GET: EnetInterventionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetInterventionType enetInterventionType = db.EnetInterventionTypes.Find(id);
            if (enetInterventionType == null)
            {
                return HttpNotFound();
            }
            return View(enetInterventionType);
        }

        // POST: EnetInterventionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterventionTypeId,Name,LabourHours,Cost")] EnetInterventionType enetInterventionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enetInterventionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enetInterventionType);
        }

        // GET: EnetInterventionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetInterventionType enetInterventionType = db.EnetInterventionTypes.Find(id);
            if (enetInterventionType == null)
            {
                return HttpNotFound();
            }
            return View(enetInterventionType);
        }

        // POST: EnetInterventionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnetInterventionType enetInterventionType = db.EnetInterventionTypes.Find(id);
            db.EnetInterventionTypes.Remove(enetInterventionType);
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
