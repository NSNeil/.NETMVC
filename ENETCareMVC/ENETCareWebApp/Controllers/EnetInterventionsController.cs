
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccessLayer;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNet.Identity;

namespace ENETCareWebApp.Controllers
{
    public class EnetInterventionsController : Controller
    {
        private InterventionService interventionService;
        private UserService userService;
        private ClientService clientService;
        private InterventionTypeService interventionTypeService;

        public EnetInterventionsController()
        {
            interventionTypeService = new InterventionTypeService();
            userService = new UserService();
            interventionService = new InterventionService();
            clientService = new ClientService();
        }
        private EnetCareEntities db = new EnetCareEntities();

        // GET: EnetInterventions
        public ActionResult Index()
        {
            string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            if (User.IsInRole("SiteEngineer"))
            {
                string district = userService.GetUserByName(name).EnetDistrict.District;
                return View(interventionService.GetEnetInterventionByDistrict(district));
            }
            else
            {
                return View(interventionService.GetApprovableInterventionForManager(userService.GetUserByName(name)));
            }
        }

        // GET: EnetInterventions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetIntervention enetIntervention = db.EnetInterventions.Find(id);
            if (enetIntervention == null)
            {
                return HttpNotFound();
            }
            return View(enetIntervention);
        }

        // GET: EnetInterventions/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.EnetClients, "ClientId", "Name");
            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District");
            ViewBag.ApprovedBy = new SelectList(db.EnetUsers, "UserId", "LoginName");
            ViewBag.ProposedBy = new SelectList(db.EnetUsers, "UserId", "LoginName");

            string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            Session["CurrentSE"] = userService.GetUserByName(name).Name;
            Session["CurrentSEId"] = userService.GetUserByName(name).UserId;
            Session["CurrentDistrict"] = userService.GetUserByName(name).EnetDistrict.District;
            Session["CurrentDistrictId"] = userService.GetUserByName(name).DistrictId;
            string district = userService.GetUserByName(name).EnetDistrict.District;
            Session["CurrentInterventionTypes"] = db.EnetInterventionTypes.ToList();
            Session["CurrentClients"] = db.EnetClients.ToList();
            
            return View();
        }

        // POST: EnetInterventions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterventionId,InterventionTypeId,ClientId,DistrictId,ProposedBy,ApprovedBy,DateToPerform,MostRecentVisitDate,LabourHours,Cost,Life,State,Note")] EnetIntervention enetIntervention)
        {
            string ss = enetIntervention.MostRecentVisitDate;
            if (ModelState.IsValid)
            {
                string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
                EnetUser currentUser = userService.GetUserByName(name);
                try
                {
                    try
                    {
                        DateTime dt = DateTime.Now;
                        try
                        {
                            dt = DateTime.Parse(enetIntervention.DateToPerform);
                        }
                        catch { throw new ArgumentException("Date value is not recognizable"); }
                        new InterventionManager().CreateIntervention(currentUser, interventionTypeService.GetInterventionById(enetIntervention.InterventionTypeId),
                    enetIntervention.State, enetIntervention.LabourHours, enetIntervention.Cost, enetIntervention.Note,
                    dt, clientService.GetClientById(enetIntervention.ClientId));
                    }
                    catch (Exception e)
                    {
                        ViewBag.ErrorMsg = e.Message;
                        return View(enetIntervention);
                    }
                }
                catch (ArgumentException e)
                {
                    ViewBag.ErrorMsg = e.Message;
                    return View(enetIntervention);
                }
                db.EnetInterventions.Add(enetIntervention);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.EnetClients, "ClientId", "Name", enetIntervention.ClientId);
            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District", enetIntervention.DistrictId);
            ViewBag.ApprovedBy = new SelectList(db.EnetUsers, "UserId", "LoginName", enetIntervention.ApprovedBy);
            ViewBag.InterventionTypeId = new SelectList(db.EnetInterventionTypes, "InterventionTypeId", "Name", enetIntervention.InterventionTypeId);
            ViewBag.ProposedBy = new SelectList(db.EnetUsers, "UserId", "LoginName", enetIntervention.ProposedBy);
            return View(enetIntervention);
        }

        // GET: EnetInterventions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetIntervention enetIntervention = db.EnetInterventions.Find(id);
            if (enetIntervention == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.EnetClients, "ClientId", "Name", enetIntervention.ClientId);
            ViewBag.DistrictId = new SelectList(db.EnetDistricts, "DistrictId", "District",
                enetIntervention.DistrictId);
            ViewBag.ApprovedBy = new SelectList(db.EnetUsers, "UserId", "LoginName", enetIntervention.ApprovedBy);
            ViewBag.InterventionTypeId = new SelectList(db.EnetInterventionTypes, "InterventionTypeId", "Name",
                enetIntervention.InterventionTypeId);
            ViewBag.ProposedBy = new SelectList(db.EnetUsers, "UserId", "LoginName", enetIntervention.ProposedBy);

            return View(enetIntervention);
        }

        public ActionResult Approve(int? id)
        {
            string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            interventionService.ApproveIntervention((int)id, userService.GetUserByName(name));
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int? id)
        {
            string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
            interventionService.CancelIntervention((int)id, userService.GetUserByName(name));
            return RedirectToAction("Index");
        }

        // POST: EnetInterventions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterventionId,InterventionTypeId,ClientId,DistrictId,ProposedBy,ApprovedBy,DateToPerform,MostRecentVisitDate,LabourHours,Cost,Life,State,Note")] EnetIntervention enetIntervention)
        {
                if (ModelState.IsValid)
                {
                    string name = System.Web.HttpContext.Current.User.Identity.GetUserName();
                    EnetUser currentUser = userService.GetUserByName(name);
                    try
                    {
                        DateTime dt = DateTime.Now;
                        try
                        {
                            dt = DateTime.Parse(enetIntervention.MostRecentVisitDate);
                        }
                    catch { throw new ArgumentException("Date value is not recognizable"); }
                    if (dt == DateTime.MinValue || dt < DateTime.Now)
                        {
                            ViewBag.ErrorMsg = "Invalid Date";
                            return View(enetIntervention);
                        }
                        var cost = enetIntervention.Cost;
                        new InterventionManager().EvaluateUpdates(currentUser, enetIntervention.ProposedBy,
                            enetIntervention.Life, enetIntervention.State,
                            interventionService.GetInterventionById(enetIntervention.InterventionId).State,
                            interventionService.GetInterventionById(enetIntervention.InterventionId)
                                .EnetDistrict.District,
                            enetIntervention.LabourHours, enetIntervention.Cost, interventionTypeService.GetInterventionById(enetIntervention.InterventionTypeId));
                    }
                    catch (Exception e)
                    {
                        ViewBag.ErrorMsg = e.Message;
                        return View(enetIntervention);
                    }
                    if (enetIntervention.State == State.Approved)
                    {
                    enetIntervention.ApprovedBy = currentUser.UserId;
                }
                db.Entry(enetIntervention).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enetIntervention);
        }

        // GET: EnetInterventions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnetIntervention enetIntervention = db.EnetInterventions.Find(id);
            if (enetIntervention == null)
            {
                return HttpNotFound();
            }
            return View(enetIntervention);
        }

        // POST: EnetInterventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnetIntervention enetIntervention = db.EnetInterventions.Find(id);
            db.EnetInterventions.Remove(enetIntervention);
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
