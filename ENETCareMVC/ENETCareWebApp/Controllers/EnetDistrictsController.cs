using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Models;

namespace ENETCareWebApp.Controllers
{
    public class EnetDistrictsController : Controller
    {
        private EnetCareEntities db = new EnetCareEntities();

        // GET: EnetDistricts
        public ActionResult Index()
        {
            return View(db.EnetDistricts.ToList());
        }

        // GET: EnetDistricts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetDistrict enetDistrict = db.EnetDistricts.Find(id);
            if (enetDistrict == null)
            {
                return HttpNotFound();
            }
            return View(enetDistrict);
        }

        // GET: EnetDistricts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnetDistricts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistrictId,District")] EnetDistrict enetDistrict)
        {
            if (ModelState.IsValid)
            {
                db.EnetDistricts.Add(enetDistrict);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enetDistrict);
        }

        // GET: EnetDistricts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetDistrict enetDistrict = db.EnetDistricts.Find(id);
            if (enetDistrict == null)
            {
                return HttpNotFound();
            }
            return View(enetDistrict);
        }

        // POST: EnetDistricts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DistrictId,District")] EnetDistrict enetDistrict)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enetDistrict).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enetDistrict);
        }

        // GET: EnetDistricts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetDistrict enetDistrict = db.EnetDistricts.Find(id);
            if (enetDistrict == null)
            {
                return HttpNotFound();
            }
            return View(enetDistrict);
        }

        // POST: EnetDistricts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnetDistrict enetDistrict = db.EnetDistricts.Find(id);
            db.EnetDistricts.Remove(enetDistrict);
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
