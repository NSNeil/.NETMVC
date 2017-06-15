
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccessLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNet.Identity;

namespace ENETCareWebApp.Controllers
{
    public class EnetClientsController : Controller
    {
        private EnetCareEntities db = new EnetCareEntities();
        private UserService userService;
        public EnetClientsController()
        {
            userService = new UserService();
        }
        // GET: EnetClients
        public ActionResult Index()
        {
            var enetClients = db.EnetClients.Include(e => e.EnetDistrict);
            return View(enetClients.ToList());
        }

        // GET: EnetClients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetClient enetClient = db.EnetClients.Find(id);
            if (enetClient == null)
            {
                return HttpNotFound();
            }
            return View(enetClient);
        }

        // GET: EnetClients/Create
        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District");
            string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            EnetDistrict district = userService.GetUserByName(name).EnetDistrict;
            Session["DistrictForCurrentEngineer"] = district.District;
            Session["DistrictIdForCurrentEngineer"] = district.DistrictId;

            return View();
        }

        // POST: EnetClients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistrictId,Name,Address")] EnetClient enetClient)
        {
            if (ModelState.IsValid)
            {
                if (enetClient.DistrictId == null)
                {
                    ViewBag.ErrorMsg = "Can't create client without district";
                    return View(enetClient);
                }
                else if (enetClient.Name == null)
                {
                    ViewBag.ErrorMsg = "Client name can't be empty";
                    return View(enetClient);
                }
                db.EnetClients.Add(enetClient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District", enetClient.DistrictId);
            return View(enetClient);
        }

        // GET: EnetClients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetClient enetClient = db.EnetClients.Find(id);
            if (enetClient == null)
            {
                return HttpNotFound();
            }

            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District", enetClient.DistrictId);
            string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            EnetDistrict district = userService.GetUserByName(name).EnetDistrict;
            Session["DistrictForCurrentEngineer"] = district.District;
            Session["DistrictIdForCurrentEngineer"] = district.DistrictId;
            return View(enetClient);
        }

        // POST: EnetClients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,DistrictId,Name,Address")] EnetClient enetClient)
        {
            if (ModelState.IsValid)
            {
                if (enetClient.DistrictId == null)
                {
                    ViewBag.ErrorMsg = "Can't create client without district";
                    return View(enetClient);
                }
                else if (enetClient.Name == null)
                {
                    ViewBag.ErrorMsg = "Client name can't be empty";
                    return View(enetClient);
                }
                db.Entry(enetClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District", enetClient.DistrictId);
            return View(enetClient);
        }

        // GET: EnetClients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetClient enetClient = db.EnetClients.Find(id);
            if (enetClient == null)
            {
                return HttpNotFound();
            }
            return View(enetClient);
        }

        // POST: EnetClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnetClient enetClient = db.EnetClients.Find(id);
            db.EnetClients.Remove(enetClient);
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
