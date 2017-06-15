using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer
{
    public class AccountantReportMaker
    {
        public DataTable GetTotalCostPerEngineer(List<EnetIntervention> engineerData)
        {
            //Create a datatable that will be returned with a name and total cost
            DataTable returnData = new DataTable();
            returnData.Clear();

            returnData.Columns.Add("Name");
            returnData.Columns.Add("Total Cost");
            returnData.Columns.Add("Total Hours");

            decimal costSum = 0;
            int labourSum = 0;


            //Do a groupBy (essentially) so that all the hypothetical rows in intervention that have
            //the same SiteEngineer Name stick together, order by ascending
            var result =
                from EnetIntervention in engineerData
                where EnetIntervention.State == State.Complete
                orderby EnetIntervention.EnetUser1.Name ascending
                group EnetIntervention by EnetIntervention.EnetUser1.Name;

            //iterate through these groups by SiteEngineer Name
            foreach (var group in result)
            {
                //get the sum for each "row"
                foreach (EnetIntervention element in group)
                {
                    costSum += (decimal)element.Cost;
                }

                foreach (EnetIntervention element in group)
                {
                    labourSum += (int)element.LabourHours;
                }

                decimal returnSum = Decimal.Round(costSum, 2);
                //add the name of the group (the site engineer name)
                //and the sum of all the cost rows together on a row in the new table
                //then the sum of all the hours
                returnData.Rows.Add(group.Key, costSum, labourSum);
                costSum = 0;
                labourSum = 0;
            }

            return returnData;
        }

        //Method which returns a list returning the average cost and Labour for each engineer

        public DataTable GetAverageCostNHoursPerEngineer(List<EnetIntervention> engineerData)
        {
            DataTable returnData = new DataTable();
            returnData.Clear();

            returnData.Columns.Add("Name");
            returnData.Columns.Add("Average Cost");
            returnData.Columns.Add("Average Hours");

            //Linq query that groups data by engineer, suming the cost and labour hours for each row that contains the engineer's id
            var result =
            from EnetIntervention in engineerData
            where EnetIntervention.State == State.Complete
            orderby EnetIntervention.EnetUser1.Name ascending
            group EnetIntervention by new { Name = EnetIntervention.EnetUser1.Name } into names
            select new
            {
                Name = names.Key.Name,
                AverageHours = names.Average(x => x.LabourHours),
                AverageCost = names.Average(z => z.Cost)
            };

            foreach (var Name in result)
            {
                Decimal nameAverageCost = (decimal)Name.AverageCost;
                Decimal nameAverageHours = (decimal)Name.AverageHours;
                nameAverageCost = decimal.Round(nameAverageCost, 2);
                nameAverageHours = decimal.Round(nameAverageHours, 2);
                returnData.Rows.Add(Name.Name, nameAverageCost, nameAverageHours);
            }

            return returnData;
        }

        //Method that gets the total cost of all districts and the total cost for all enet districts
        public DataTable GetTotalCostForDistricts(List<EnetIntervention> clientData)
        {
            DataTable returnData = new DataTable();
            returnData.Clear();

            returnData.Columns.Add("District");
            returnData.Columns.Add("Total Cost");

            //select district and total cost for that district
            var result =
                from EnetIntervention in clientData
                where EnetIntervention.State == State.Complete
                group EnetIntervention by new { District = EnetIntervention.EnetDistrict.District } into districts
                select new
                {
                    District = districts.Key.District,
                    TotalCost = districts.Sum(x => x.Cost)
                };

            foreach (var District in result)
            {
                Decimal districtTotalCost = (Decimal)District.TotalCost;
                districtTotalCost = decimal.Round(districtTotalCost, 2);
                returnData.Rows.Add(District.District, districtTotalCost);
            }

            //add all district total costs to reference variable, to get total cost for all districts


            return returnData;
        }


        public Decimal GetCostForAllDistricts(DataTable data)
        {
            Decimal returnSum = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                returnSum += Decimal.Parse(data.Rows[i].Field<string>(1));
            }

            return returnSum;
        }

        //struct which stores data for method below
        private struct InterventionWithDateTime
        {
            private decimal cost;
            private DateTime time;
            private State state;

            public decimal Cost
            {
                get
                {
                    return this.cost;
                }

                set
                {
                    this.cost = value;
                }
            }

            public DateTime Time
            {
                get
                {
                    return this.time;
                }

                set
                {
                    this.time = value;
                }
            }

            public State State
            {
                get
                {
                    return this.state;
                }

                set
                {
                    this.state = value;
                }
            }
        }

        public DataTable GetTotalCostForEachMonth(List<EnetIntervention> interventionDateData, int year)

        {

            DataTable returnData = new DataTable();

            returnData.Clear();



            returnData.Columns.Add("Month");

            returnData.Columns.Add("Year");

            returnData.Columns.Add("TotalCost");



            //create a list of structs that have a dateTime format and not a string for thier time field, copy needed info and DateTo

            //Perform into structs and add those structs to a list. Makes it easier for linq query

            List<InterventionWithDateTime> dateTimeInterventionList = new List<InterventionWithDateTime>();

            foreach (EnetIntervention intervention in interventionDateData)

            {

                InterventionWithDateTime tempDateTimeIntervention = new InterventionWithDateTime();

                tempDateTimeIntervention.Cost = (decimal)intervention.Cost;

                string temp = intervention.DateToPerform.Trim();

                tempDateTimeIntervention.Time = DateTime.ParseExact(temp, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                tempDateTimeIntervention.State = intervention.State;

                dateTimeInterventionList.Add(tempDateTimeIntervention);

            }



            //use the list of structs to carry out linq query and read data into dataTable, which is returned with the data for the report

            var result =

                from InterventionWithDateTime in dateTimeInterventionList

                where InterventionWithDateTime.State == State.Complete && InterventionWithDateTime.Time.Year == year

                group InterventionWithDateTime by new { Performed = InterventionWithDateTime.Time.Month, Year = InterventionWithDateTime.Time.Year } into monthPerformed

                select new

                {

                    MonthPerformed = monthPerformed.Key.Performed,

                    Year = monthPerformed.Key.Year,

                    TotalCost = monthPerformed.Sum(x => x.Cost)

                };
            Boolean isValueForMonth;


            for (int i = 1; i < 13; i++)
            {
                isValueForMonth = false;
                foreach (var Performed in result)

                {
                    if (Performed.MonthPerformed == i)
                    {
                        returnData.Rows.Add(Performed.MonthPerformed.ToString(), Performed.Year.ToString(), Performed.TotalCost);
                        isValueForMonth = true;
                    }


                }

                if (isValueForMonth == false)
                {
                    returnData.Rows.Add(i.ToString(), year.ToString(), "0");
                }

            }


            return returnData;

        }

    }

}
