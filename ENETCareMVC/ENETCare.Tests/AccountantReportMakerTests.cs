using BusinessLogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using BusinessLogicLayer.Models;

namespace ENETCare.Tests
{
    /// <summary>
    /// Summary description for AccountantReportMakerTests
    /// </summary>
    [TestClass]
    public class AccountantReportMakerTests
    {
        public AccountantReportMakerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Additional test attributes

        public List<EnetIntervention> interventionsDummyData = new List<EnetIntervention>();
        public List<EnetIntervention> clientInterventionDummyData = new List<EnetIntervention>();
        private List<EnetIntervention> InterventionDummyData = new List<EnetIntervention>();

        private EnetUser Gary = new EnetUser { Name = "Gary Smith"};
        private EnetUser Anthony = new EnetUser { Name = "Anthony Man"};
        private EnetUser Tam = new EnetUser { Name = "Tam George" };
        private EnetUser Mcgoo = new EnetUser { Name = "Mcgoo Man" };

        private EnetDistrict uIndonesia = new EnetDistrict { DistrictId = 1, District = "Urban Indonesia" };
        private EnetDistrict pNewGuinea = new EnetDistrict { DistrictId = 2, District = "Urban Papua New Guinea" };
        private EnetDistrict sydney = new EnetDistrict { DistrictId = 2, District = "Sydney" };

        //Initialize the test, intervention1 will always have intervention 1 in it
        [TestInitialize]
        public void CreateDummyDataForAccountantReportTests()
        {
            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetUser1 = Gary;
            intervention1.Cost = 4000;
            intervention1.LabourHours = 8;
            intervention1.State = State.Complete;

            interventionsDummyData.Add(intervention1);
        }

        //first test send GetTotalCostPerEngineer one list of data, checks results are correct
        [TestMethod]
        public void AccountantGetTotalCostPerEngineerTest_OneIntervention_OnSucsess_ReturnCorrectNameAndCost()
        {
            AccountantReportMaker testreportmaker1 = new AccountantReportMaker();
            DataTable testvalues = testreportmaker1.GetTotalCostPerEngineer(interventionsDummyData);

            Assert.AreEqual("Gary Smith", testvalues.Rows[0][0]);
            Assert.AreEqual("4000", testvalues.Rows[0][1]);
            Assert.AreEqual("8", testvalues.Rows[0][2]);
        }


        //Second test adds another intervention to the list checks it works
        [TestMethod]
        public void AccountantGetTotalCostPerEngineerTest_TwoInterventions_OnSuccess_ReturnCorrectNameAndCost()
        {

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Gary;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostPerEngineer(interventionsDummyData);
            string testresult2 = (string)testValues.Rows[0][0];
            Assert.AreEqual("Gary Smith", testresult2);
            Assert.AreEqual("11000", testValues.Rows[0][1]);
        }

        //As this is a new test method intervention 2 is forgotten, has to be re-added in the method
        [TestMethod]
        public void AccountantGetTotalCostPerEngineerTest_FourInterventions_OnSuccess_ReturnCorrectNameAndCost()
        {
            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Gary;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetUser1 = Anthony;
            intervention3.Cost = 7000;
            intervention3.LabourHours = 10;
            intervention3.State = State.Complete;

            interventionsDummyData.Add(intervention3);

            EnetIntervention intervention4 = new EnetIntervention();
            intervention4.EnetUser1 = Tam;
            intervention4.Cost = 14000;
            intervention4.LabourHours = 10;
            intervention4.State = State.Complete;

            interventionsDummyData.Add(intervention4);


            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostPerEngineer(interventionsDummyData);

            Assert.AreEqual("Anthony Man", testValues.Rows[0][0]);
            Assert.AreEqual("7000", testValues.Rows[0][1]);
            Assert.AreEqual("10", testValues.Rows[0][2]);

            Assert.AreEqual("Gary Smith", testValues.Rows[1][0]);
            Assert.AreEqual("11000", testValues.Rows[1][1]);
            Assert.AreEqual("18", testValues.Rows[1][2]);

            Assert.AreEqual("Tam George", testValues.Rows[2][0]);
            Assert.AreEqual("14000", testValues.Rows[2][1]);
            Assert.AreEqual("10", testValues.Rows[2][2]);
        }

        [TestMethod]
        public void AccountantGetTotalCostPerEngineerTest_SixInterventions_OnSuccess_ReturnCorrectNameAndCost()
        {
            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Gary;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetUser1 = Anthony;
            intervention3.Cost = 7000;
            intervention3.LabourHours = 10;
            intervention3.State = State.Complete;

            interventionsDummyData.Add(intervention3);

            EnetIntervention intervention4 = new EnetIntervention();
            intervention4.EnetUser1 = Tam;
            intervention4.Cost = 14000;
            intervention4.LabourHours = 10;
            intervention4.State = State.Complete;

            interventionsDummyData.Add(intervention4);

            EnetIntervention intervention5 = new EnetIntervention();
            intervention5.EnetUser1 = Mcgoo;
            intervention5.Cost = 500;
            intervention5.LabourHours = 2;
            intervention5.State = State.Complete;

            interventionsDummyData.Add(intervention5);

            EnetIntervention intervention6 = new EnetIntervention();
            intervention6.EnetUser1 = Tam;
            intervention6.Cost = 30000;
            intervention6.LabourHours = 20;
            intervention6.State = State.Complete;

            interventionsDummyData.Add(intervention6);


            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostPerEngineer(interventionsDummyData);

            Assert.AreEqual("Anthony Man", testValues.Rows[0][0]);
            Assert.AreEqual("7000", testValues.Rows[0][1]);
            Assert.AreEqual("10", testValues.Rows[0][2]);

            Assert.AreEqual("Gary Smith", testValues.Rows[1][0]);
            Assert.AreEqual("11000", testValues.Rows[1][1]);
            Assert.AreEqual("18", testValues.Rows[1][2]);

            Assert.AreEqual("Mcgoo Man", testValues.Rows[2][0]);
            Assert.AreEqual("500", testValues.Rows[2][1]);
            Assert.AreEqual("2", testValues.Rows[2][2]);

            Assert.AreEqual("Tam George", testValues.Rows[3][0]);
            Assert.AreEqual("44000", testValues.Rows[3][1]);
            Assert.AreEqual("30", testValues.Rows[3][2]);
        }

        [TestMethod]
        public void AccountantGetTotalCostPerEngineerTest_EnsureOnlyCompletedReturned_OnSuccessReturnCorrectNameAndCost()
        {

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Anthony;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Approved;

            interventionsDummyData.Add(intervention2);

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetUser1 = Tam;
            intervention3.Cost = 14000;
            intervention3.LabourHours = 10;
            intervention3.State = State.Complete;


            interventionsDummyData.Add(intervention3);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostPerEngineer(interventionsDummyData);

            Assert.AreNotEqual("Anthony Man", testValues.Rows[0][0]);
            Assert.AreNotEqual("7000", testValues.Rows[0][1]);
            Assert.AreNotEqual("10", testValues.Rows[0][2]);

            Assert.AreEqual("Gary Smith", testValues.Rows[0][0]);
            Assert.AreEqual("4000", testValues.Rows[0][1]);
            Assert.AreEqual("8", testValues.Rows[0][2]);

            Assert.AreEqual("Tam George", testValues.Rows[1][0]);
            Assert.AreEqual("14000", testValues.Rows[1][1]);
            Assert.AreEqual("10", testValues.Rows[1][2]);
        }

        [TestMethod]
        public void AccountantGetAverageCostHoursPerEngineerTest_TestWithOneEngineerTwoEntries_OnSuccessReturnCorrectNameAndCost()
        {
            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Gary;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetAverageCostNHoursPerEngineer(interventionsDummyData);
            Assert.AreEqual("Gary Smith", testValues.Rows[0][0]);
            Assert.AreEqual("5500", testValues.Rows[0][1]);
            Assert.AreEqual("9", testValues.Rows[0][2]);
        }

        [TestMethod]
        public void AccountantGetAverageCostHoursPerEngineerTest_TestWithOneEngineerThreeEntries_OnSuccessReturnCorrectNameAndCost()
        {
            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Gary;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetUser1 = Gary;
            intervention3.Cost = 10000;
            intervention3.LabourHours = 18;
            intervention3.State = State.Complete;

            interventionsDummyData.Add(intervention3);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetAverageCostNHoursPerEngineer(interventionsDummyData);
            Assert.AreEqual("Gary Smith", testValues.Rows[0][0]);
            Assert.AreEqual("7000", testValues.Rows[0][1]);
            Assert.AreEqual("12", testValues.Rows[0][2]);
        }

        [TestMethod]
        public void AccountantGetAverageCostHoursPerEngineerTest_TestWithThreeEngineer_OnSuccessReturnCorrectNameAndCost()
        {

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Tam;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetUser1 = Anthony;
            intervention3.Cost = 10000;
            intervention3.LabourHours = 18;
            intervention3.State = State.Complete;

            interventionsDummyData.Add(intervention3);


            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetAverageCostNHoursPerEngineer(interventionsDummyData);

            Assert.AreEqual("Anthony Man", testValues.Rows[0][0]);
            Assert.AreEqual("10000", testValues.Rows[0][1]);
            Assert.AreEqual("18", testValues.Rows[0][2]);
            Assert.AreEqual("Gary Smith", testValues.Rows[1][0]);
            Assert.AreEqual("4000", testValues.Rows[1][1]);
            Assert.AreEqual("8", testValues.Rows[1][2]);
            Assert.AreEqual("Tam George", testValues.Rows[2][0]);
            Assert.AreEqual("7000", testValues.Rows[2][1]);
            Assert.AreEqual("10", testValues.Rows[2][2]);
        }

        [TestMethod]
        public void AccountantGetAverageCostHoursPerEngineerTest_TestWithThreeEngineerFiveEntries_OnSuccessReturnCorrectNameAndCost()
        {
            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetUser1 = Gary;
            intervention2.Cost = 7000;
            intervention2.LabourHours = 10;
            intervention2.State = State.Complete;

            interventionsDummyData.Add(intervention2);

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetUser1 = Anthony;
            intervention3.Cost = 10000;
            intervention3.LabourHours = 18;
            intervention3.State = State.Complete;

            interventionsDummyData.Add(intervention3);

            EnetIntervention intervention4 = new EnetIntervention();
            intervention4.EnetUser1 = Tam;
            intervention4.Cost = 9000;
            intervention4.LabourHours = 12;
            intervention4.State = State.Complete;

            interventionsDummyData.Add(intervention4);

            EnetIntervention intervention5 = new EnetIntervention();
            intervention5.EnetUser1 = Anthony;
            intervention5.Cost = 20000;
            intervention5.LabourHours = 12;
            intervention5.State = State.Complete;

            interventionsDummyData.Add(intervention5);



            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetAverageCostNHoursPerEngineer(interventionsDummyData);

            Assert.AreEqual("Anthony Man", testValues.Rows[0][0]);
            Assert.AreEqual("15000", testValues.Rows[0][1]);
            Assert.AreEqual("15", testValues.Rows[0][2]);
            Assert.AreEqual("Gary Smith", testValues.Rows[1][0]);
            Assert.AreEqual("5500", testValues.Rows[1][1]);
            Assert.AreEqual("9", testValues.Rows[1][2]);
            Assert.AreEqual("Tam George", testValues.Rows[2][0]);
            Assert.AreEqual("9000", testValues.Rows[2][1]);
            Assert.AreEqual("12", testValues.Rows[2][2]);
        }

        [TestMethod]
        public void AccountantGetTotalCostPerDistrictTest_WithOneDistrict_OnSuccessReturnDistrictNCost()
        {

            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetDistrict = uIndonesia;
            intervention1.State = State.Complete;
            intervention1.Cost = 12000; 

            clientInterventionDummyData.Add(intervention1);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostForDistricts(clientInterventionDummyData);
            Assert.AreEqual("Urban Indonesia", testValues.Rows[0][0]);
            Assert.AreEqual("12000", testValues.Rows[0][1]);
        }

        [TestMethod]
        public void AccountantGetTotalCostPerDistrictTest_WithThreeDistrict_OnSuccesReturnDistrictNCost()
        {

            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetDistrict = uIndonesia;
            intervention1.State = State.Complete;
            intervention1.Cost = 12000;

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetDistrict = uIndonesia;
            intervention2.State = State.Complete;
            intervention2.Cost = 15000;

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetDistrict = uIndonesia;
            intervention3.State = State.Complete;
            intervention3.Cost = 16000;


            clientInterventionDummyData.Add(intervention1);
            clientInterventionDummyData.Add(intervention2);
            clientInterventionDummyData.Add(intervention3);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostForDistricts(clientInterventionDummyData);
            Assert.AreEqual("Urban Indonesia", testValues.Rows[0][0]);
            Assert.AreEqual("43000", testValues.Rows[0][1]);
        }

        [TestMethod]
        public void AccountantGetTotalCostPerDistrictTest_WithThreeDistrictSixEntriees_OnSuccesReturnDistrictNCost()
        {

            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetDistrict = uIndonesia;
            intervention1.State = State.Complete;
            intervention1.Cost = 12000;

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetDistrict = pNewGuinea;
            intervention2.State = State.Complete;
            intervention2.Cost = 15000;

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetDistrict = uIndonesia;
            intervention3.State = State.Complete;
            intervention3.Cost = 16000;

            EnetIntervention intervention4 = new EnetIntervention();
            intervention4.EnetDistrict = sydney;
            intervention4.State = State.Complete;
            intervention4.Cost = 12000;

            EnetIntervention intervention5 = new EnetIntervention();
            intervention5.EnetDistrict = pNewGuinea;
            intervention5.State = State.Complete;
            intervention5.Cost = 13050;

            EnetIntervention intervention6 = new EnetIntervention();
            intervention6.EnetDistrict = uIndonesia;
            intervention6.State = State.Complete;
            intervention6.Cost = 2000;

            clientInterventionDummyData.Add(intervention1);
            clientInterventionDummyData.Add(intervention2);
            clientInterventionDummyData.Add(intervention3);
            clientInterventionDummyData.Add(intervention4);
            clientInterventionDummyData.Add(intervention5);
            clientInterventionDummyData.Add(intervention6);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostForDistricts(clientInterventionDummyData);
            Assert.AreEqual("Urban Indonesia", testValues.Rows[0][0]);
            Assert.AreEqual("30000", testValues.Rows[0][1]);
            Assert.AreEqual("Urban Papua New Guinea", testValues.Rows[1][0]);
            Assert.AreEqual("28050", testValues.Rows[1][1]);
            Assert.AreEqual("Sydney", testValues.Rows[2][0]);
            Assert.AreEqual("12000", testValues.Rows[2][1]);
        }

        [TestMethod]
        public void AccountantGetTotalCostPerDistrictTest_CheckNotCompletedExcluded_OnSuccesReturnDistrictNCost()
        {

            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetDistrict = uIndonesia;
            intervention1.State =State.Complete;
            intervention1.Cost = 12000;

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetDistrict = pNewGuinea;
            intervention2.State = State.Approved;
            intervention2.Cost = 15000;

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetDistrict = sydney;
            intervention3.State = State.Complete;
            intervention3.Cost = 16000;


            clientInterventionDummyData.Add(intervention1);
            clientInterventionDummyData.Add(intervention2);
            clientInterventionDummyData.Add(intervention3);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testValues = testReportMaker1.GetTotalCostForDistricts(clientInterventionDummyData);
            Assert.AreEqual("Urban Indonesia", testValues.Rows[0][0]); ;
            Assert.AreEqual("Sydney", testValues.Rows[1][0]);
            Assert.AreEqual(2, testValues.Rows.Count);
        }

        [TestMethod]
        public void AccountantGetTotalCostForAllDistrict_CheckTotal_OnSuccessChangValue()
        {

            Decimal testResult;

            DataTable dummyTable = new DataTable();

            dummyTable.Columns.Add("District");
            dummyTable.Columns.Add("Total Cost");

            dummyTable.Rows.Add("Urban Indonesia", "12000");
            dummyTable.Rows.Add("Urban Papua New Guinea", "15000");
            dummyTable.Rows.Add("Sydney", "16000"); 


            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            testResult = testReportMaker1.GetCostForAllDistricts(dummyTable);
            Assert.AreEqual(43000, testResult);
        }

        [TestMethod]
        public void AccountantTotalCostPerMonthTest_CheckCorrectOutput_ThreeInterventions()
        {

            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetDistrict = uIndonesia;
            intervention1.State = State.Complete;
            intervention1.DateToPerform = "12/04/2018";
            intervention1.Cost = 12000;

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetDistrict = uIndonesia;
            intervention2.State = State.Complete;
            intervention2.DateToPerform = "06/04/2018";
            intervention2.Cost = 15000;

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetDistrict = sydney;
            intervention3.DateToPerform = "03/03/2018"; 
            intervention3.State = State.Complete;
            intervention3.Cost = 16000;


            InterventionDummyData.Add(intervention1);
            InterventionDummyData.Add(intervention2);
            InterventionDummyData.Add(intervention3);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testResult = testReportMaker1.GetTotalCostForEachMonth(InterventionDummyData, 2018);
            Assert.AreEqual("4", testResult.Rows[3][0]);
            Assert.AreEqual("2018", testResult.Rows[3][1]);
            Assert.AreEqual("27000", testResult.Rows[3][2]);
            Assert.AreEqual("3", testResult.Rows[2][0]);
            Assert.AreEqual("2018", testResult.Rows[2][1]);
            Assert.AreEqual("16000", testResult.Rows[2][2]);
        }

        [TestMethod]
        public void AccountantTotalCostPerMonthTest_CheckCorrectOutput_SixInterventions_CheckYear ()
        {
            EnetIntervention intervention1 = new EnetIntervention();
            intervention1.EnetDistrict = uIndonesia;
            intervention1.State = State.Complete;
            intervention1.DateToPerform = "12/04/2018";
            intervention1.Cost = 12000;

            EnetIntervention intervention2 = new EnetIntervention();
            intervention2.EnetDistrict = uIndonesia;
            intervention2.State = State.Complete;
            intervention2.DateToPerform = "06/04/2018";
            intervention2.Cost = 15000;

            EnetIntervention intervention3 = new EnetIntervention();
            intervention3.EnetDistrict = sydney;
            intervention3.DateToPerform = "03/03/2018";
            intervention3.State = State.Complete;
            intervention3.Cost = 16000;

            EnetIntervention intervention4 = new EnetIntervention();
            intervention4.EnetDistrict = sydney;
            intervention4.DateToPerform = "03/01/2018";
            intervention4.State = State.Complete;
            intervention4.Cost = 16000;

            EnetIntervention intervention5 = new EnetIntervention();
            intervention5.EnetDistrict = sydney;
            intervention5.DateToPerform = "03/01/2017";
            intervention5.State = State.Complete;
            intervention5.Cost = 16000;

            EnetIntervention intervention6 = new EnetIntervention();
            intervention6.EnetDistrict = sydney;
            intervention6.DateToPerform = "03/01/2018";
            intervention6.State = State.Complete;
            intervention6.Cost = 16000;

            InterventionDummyData.Add(intervention1);
            InterventionDummyData.Add(intervention2);
            InterventionDummyData.Add(intervention3);
            InterventionDummyData.Add(intervention4);
            InterventionDummyData.Add(intervention5);
            InterventionDummyData.Add(intervention6);

            AccountantReportMaker testReportMaker1 = new AccountantReportMaker();
            DataTable testResult = testReportMaker1.GetTotalCostForEachMonth(InterventionDummyData, 2018);
            Assert.AreEqual("1", testResult.Rows[0][0]);
            Assert.AreEqual("2018", testResult.Rows[0][1]);
            Assert.AreEqual("32000", testResult.Rows[0][2]);

            Assert.AreEqual("3", testResult.Rows[2][0]);
            Assert.AreEqual("2018", testResult.Rows[2][1]);
            Assert.AreEqual("16000", testResult.Rows[2][2]);

            Assert.AreEqual("4", testResult.Rows[3][0]);
            Assert.AreEqual("2018", testResult.Rows[3][1]);
            Assert.AreEqual("27000", testResult.Rows[3][2]); 

        }
    }
}