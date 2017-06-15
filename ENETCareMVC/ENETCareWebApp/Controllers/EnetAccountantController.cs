using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using DatabaseAccessLayer;


namespace ENETCareWebApp.Controllers
{

   
    public class EnetAccountantController : Controller

    {

        AccountantReportMaker reportMaker1 = new AccountantReportMaker();

        InterventionService interventionService = new InterventionService();
        private UserService userService;
        private EnetUser enetUser;
        private EnetDistrictService enetDistrictService;

        // GET: Accountant
        public ActionResult GetTotalForEngineer()
        {
            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetTotalCostPerEngineer(allInterventions); 

            return View(forDisplay);
        }

        public ActionResult GetAverageForEngineer()
        {
            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetAverageCostNHoursPerEngineer(allInterventions);

            return View(forDisplay);
        }
        
        public ActionResult GetTotalCostForEachDistrict()
        {
            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetTotalCostForDistricts(allInterventions);
            Decimal totalGlobalCost = reportMaker1.GetCostForAllDistricts(forDisplay);

            AccountantViewModels view = new AccountantViewModels();

            view.ForDisplay = forDisplay;
            view.TotalForAllDistricts = totalGlobalCost;
            return View(view);
        }

        public ActionResult ListUsers()
        {
            userService = new UserService();
            var enetUsers = userService.GetUsers();
            enetUser = new EnetUser();
            var engineerAndManager = enetUser.GetSiteEngineerAndManagers(enetUsers.ToList());

            return View(engineerAndManager);
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            userService = new UserService();
            enetUser = userService.GetUserById(id.Value);
            if (enetUser == null)
            {
                return HttpNotFound();
            }
            enetDistrictService = new EnetDistrictService();
            ViewBag.DistrictId = new SelectList(enetDistrictService.EnetDistricts, "DistrictId", "District", enetUser.DistrictId);
            return View(enetUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "UserId,LoginName,Name,Role,DistrictId,MaxCost,MaxHours")] EnetUser enetUser)
        {
            if (ModelState.IsValid)
            {
                userService = new UserService();
                userService.UpdateUser(enetUser);
                return RedirectToAction("ListUsers");
            }
            ViewBag.DistrictId = new SelectList(enetDistrictService.EnetDistricts, "DistrictId", "District", enetUser.DistrictId);
            return View(enetUser);
        }



        public ActionResult GetMonthlyCosts2017()
        {
            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetTotalCostForEachMonth(allInterventions, 2017);
            return View(forDisplay); 

        }

        public ActionResult GetMonthlyCosts2018()
        {
            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetTotalCostForEachMonth(allInterventions, 2018);
            return View(forDisplay);

        }

        public ActionResult GetMonthlyCosts2019()
        {
            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetTotalCostForEachMonth(allInterventions, 2019);
            return View(forDisplay);

        }

    }
}