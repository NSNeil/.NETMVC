using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DatabaseAccessLayer;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;

using System.Net;
using System.Net.Mail;

namespace DatabaseAccessLayer
{
    public class InterventionService : BaseService
    {
        public InterventionService() : base() { }
        public InterventionService(EnetCareEntities context) : base(context) { }

        public virtual int Add(EnetIntervention intervention)
        {
            context.EnetInterventions.Add(intervention);
            context.SaveChanges();
            return intervention.InterventionId;
        }

        public virtual ICollection<EnetIntervention> EnetInterventions
        {
            get
            {
                return context.EnetInterventions.OrderBy(x => x.InterventionId).ToList();
            }
        }
        public virtual List<EnetIntervention> GetEnetInterventionByDistrict(string district)
        {
            return context.Set<EnetIntervention>().Where(i => i.EnetDistrict.District == district).ToList();
        }

        public virtual List<EnetIntervention> GetApprovableInterventionForManager(EnetUser manager)
        {
            return context.Set<EnetIntervention>().Where(i => i.EnetDistrict.District == manager.EnetDistrict.District)
                .Where(i => i.State == State.Proposed || i.State == State.Approved)
                .Where(i => i.Cost <= manager.MaxCost)
                .Where(i => i.LabourHours <= manager.MaxHours)
                .ToList();
        }

        public virtual void CancelIntervention(int id, EnetUser manager)
        {
            EnetIntervention intervention = GetInterventionById(id);
            intervention.State = State.Cancelled;
            intervention.ApprovedBy = manager.UserId;
            context.Entry(intervention).State = EntityState.Modified;
            context.SaveChanges();

            try
            {
                SendMail("blank.riza@gmail.com", "Canceled", "Your intervention " + intervention.InterventionId + " has been canceled");
            }
            catch { }
        }

        public virtual void ApproveIntervention(int id, EnetUser manager)
        {
            EnetIntervention intervention = GetInterventionById(id);
            intervention.State = State.Approved;
            intervention.ApprovedBy = manager.UserId;
            context.Entry(intervention).State = EntityState.Modified;
            context.SaveChanges();

            try
            {
                SendMail("blank.riza@gmail.com", "Approved", "Your intervention " + intervention.InterventionId + " has been approved");
            }
            catch { }
            
        }

        void SendMail(string destAddress, string inputSubject, string inputBody) {
            

            var fromAddress = new MailAddress("rifai.riza.kelvin.uts@gmail.com", "Manager");
            var toAddress = new MailAddress(destAddress, destAddress);
            string fromPassword = "PassworD";
            string subject = inputSubject;
            string body = inputBody;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public virtual EnetIntervention GetInterventionById(int id)
        {
            return context.Set<EnetIntervention>().FirstOrDefault(i => i.InterventionId == id);
        }

        public virtual List<EnetIntervention> GetAllInterventions()
        {
            return context.Set<EnetIntervention>().ToList(); 
        }
    }
}