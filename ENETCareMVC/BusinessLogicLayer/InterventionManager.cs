using System;
using BusinessLogicLayer.Models;
using LiteGuard;
using Microsoft.AspNet.Identity;

namespace BusinessLogicLayer
{
    public class InterventionManager
    {
        public InterventionManager()
        {
            // What is this constructor doing here?
        }
        
        public bool CreateIntervention(EnetUser proposedBy, EnetInterventionType interventionType, State state, int? labourHours,
            decimal? cost, string note, DateTime dateToPerform, EnetClient client)
        {
            if (client == null)
            {
                throw new ArgumentException("Please choose a client!");
            }
            if (client.EnetDistrict.District != proposedBy.EnetDistrict.District)
            {
                throw new ArgumentException("Client is not in your district");
            }
            if (dateToPerform < DateTime.Now)
            {
                throw new ArgumentException("Date is in the past, please change to current date or later");
            }
            if (labourHours == null
                || labourHours < interventionType.LabourHours)
            {
                throw new ArgumentException("Labour hour is less than the default hour of this type or invalid number");
            }
            if (cost == null
                || cost < interventionType.Cost)
            {
                throw new ArgumentException("Cost is less than the default cost of this type or invalid number");
            }
            if (dateToPerform < DateTime.Now)
            {
                throw new ArgumentException("Date is wrong");
            }
            if (state == State.Approved || state == State.Complete)
            {
                if (labourHours > proposedBy.MaxHours || cost > proposedBy.MaxCost)
                {
                    throw new ArgumentException("Cost or Labour Hours is beyond your limit");
                }
            }
            else if (state != State.Proposed)
            {
                throw new ArgumentException("Invalid state");
            }
            //if (client.EnetDistrict.District != proposedBy.EnetDistrict.District)
            //{
            //    throw new ArgumentException("Client is not in your district");
            //}
            //EnetIntervention newIntervention = new EnetIntervention
            //{
            //    InterventionTypeId = interventionType.InterventionTypeId,
            //    LabourHours = labourHours,
            //    Cost = cost,
            //    ProposedBy = proposedBy.UserId,
            //    Note = note,
            //    DateToPerform = dateToPerform.ToShortDateString(),
            //    ClientId = client.ClientId,
            //    MostRecentVisitDate = DateTime.Now.ToShortDateString(),
            //    ApprovedBy = null,
            //    State = int.Parse(state),
            //    DistrictId = client.DistrictId,
            //    Life = 0
            //};

            return true;
        }

        /// <summary>
        /// This method is only used by the site engineer to evaluate whether the update inputs are valid
        /// </summary>
        /// <param name="proposedById"></param>
        /// <param name="life"></param>
        /// <param name="newState"></param>
        /// <param name="oldState"></param>
        /// <param name="interventionDistrict">District of the intervention</param>
        /// <param name="labourHours">The new labour hour</param>
        /// <param name="cost">The new cost</param>
        /// <returns></returns>
        public void EvaluateUpdates(EnetUser currentUser, int? proposedById, double? life, State newState, State oldState, string interventionDistrict, int? labourHours, decimal? cost, EnetInterventionType interventionType)
        {
            if (labourHours == null
                || labourHours < interventionType.LabourHours)
            {
                throw new ArgumentException("Labour hour is less than the default hour of this type or invalid number");
            }
            if (cost == null
                || cost < interventionType.Cost)
            {
                throw new ArgumentException("Cost is less than the default cost of this type or invalid number");
            }
            if (life > 1 || life < 0)
            {
                throw new ArgumentException("Invalid life! Please enter a number larger than 0 and less than 1");
            }
            if (oldState == State.Approved)
            {
                if (newState == State.Proposed)
                {
                    throw new ArgumentException("Can't change state backwards!");
                }
                if (newState == State.Cancelled || newState == State.Complete)
                {
                    if (currentUser.UserId != proposedById)
                    {
                        throw new ArgumentException("An intervention that has been “Approved” can only be “Cancelled” or “Completed” by the Site Engineer that proposed the intervention.");
                    }
                }
            }
            if (oldState == State.Complete)
            {
                if (newState == State.Proposed || newState == State.Approved)
                {
                    throw new ArgumentException("Can't change state backwards!");
                }
            }
            if (oldState == State.Proposed)
            {
                if (newState == State.Approved || newState == State.Complete || newState == State.Cancelled)
                {
                    if (currentUser.EnetDistrict.District != interventionDistrict)
                    {
                        throw new ArgumentException("Intervention not in your district");
                    }
                    if (currentUser.MaxHours < labourHours)
                    {
                        throw new ArgumentException("You can't perform this action due to hour limit");
                    }
                    if (currentUser.MaxCost < cost)
                    {
                        throw new ArgumentException("You can't perform this action due to cost limit");
                    }
                }
            }
        }
    }
}