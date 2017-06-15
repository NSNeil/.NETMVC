using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ENETCare.IMS.BusinessLogicLayer;
using ENETCare.IMS.BusinessLogicLayer.Models;
using ENETCare.IMS.DatabaseAccessLayer;

namespace ENETCareWebApp.Controllers
{
    public class AccountantController : Controller
    {



        AccountantReportMaker reportMaker1 = new AccountantReportMaker(); 

        InterventionService interventionService = new InterventionService();
        


        private EnetCareEntities db = new EnetCareEntities();

        // GET: Accountant
        public ActionResult Index()
        {

            List<EnetIntervention> allInterventions = new List<EnetIntervention>();
            allInterventions = interventionService.GetAllInterventions();
            DataTable forDisplay = reportMaker1.GetTotalCostPerEngineer(allInterventions); 
            return View(forDisplay);



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