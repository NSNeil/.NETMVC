using System;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENETCare.Tests
{
    [TestClass]
    public class UpdateInterventionTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void EngineerApproveIntervention_OnSuccess_ThrowNoException()
        {
            Exception exception = null;
            EnetUser currentUser = new EnetUser
            {
                EnetDistrict = new EnetDistrict {
                    District = "Rural Indonesia",
                    DistrictId = 1
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1
            };
            EnetInterventionType type = new EnetInterventionType
            {
                InterventionTypeId = 1,
                Cost = 10000,
                LabourHours = 100,
                Name = "Install net"
            };
            try
            {
                new InterventionManager().EvaluateUpdates(currentUser, 1, 1, State.Approved, State.Proposed, "Rural Indonesia", 100, 10000,type);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNull(exception);
        }

        [TestMethod]
        public void EngineerApproveIntervention_OnUserDistrictDifferentFromIntervention_ThrowException()
        {
            Exception exception = null;
            EnetUser currentUser = new EnetUser
            {
                EnetDistrict = new EnetDistrict
                {
                    District = "Sydney",
                    DistrictId = 1
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1
            };
            EnetInterventionType type = new EnetInterventionType
            {
                InterventionTypeId = 1,
                Cost = 10000,
                LabourHours = 100,
                Name = "Install net"
            };
            try
            {
                new InterventionManager().EvaluateUpdates(currentUser, 1, 1, State.Approved, State.Proposed, "Rural Indonesia", 100, 10000, type);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void EngineerApproveIntervention_OnEnginnerMaxCostInsufficient_ThrowException()
        {
            Exception exception = null;

            EnetUser currentUser = new EnetUser
            {
                EnetDistrict = new EnetDistrict
                {
                    District = "Rural Indonesia",
                    DistrictId = 1
                },
                MaxHours = 1000,
                MaxCost = 10,
                Name = "Neil",
                UserId = 1
            };
            EnetInterventionType type = new EnetInterventionType
            {
                InterventionTypeId = 1,
                Cost = 10000,
                LabourHours = 100,
                Name = "Install net"
            };
            try
            {
                new InterventionManager().EvaluateUpdates(currentUser, 1, 1, State.Approved, State.Proposed, "Rural Indonesia", 100, 10000, type);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void EngineerApproveIntervention_OnEnginnerMaxLabourHoursInsufficient_ThrowException()
        {
            Exception exception = null;

            EnetUser currentUser = new EnetUser
            {
                EnetDistrict = new EnetDistrict
                {
                    District = "Rural Indonesia",
                    DistrictId = 1
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1
            };
            EnetInterventionType type = new EnetInterventionType
            {
                InterventionTypeId = 1,
                Cost = 10000,
                LabourHours = 100,
                Name = "Install net"
            };
            try
            {
                new InterventionManager().EvaluateUpdates(currentUser, 1, 1, State.Approved, State.Proposed, "Rural Indonesia", 100, 10000, type);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void EngineerApproveIntervention_OnLifeInputInvalid_ThrowException()
        {
            Exception exception = null;
            EnetUser currentUser = new EnetUser
            {
                EnetDistrict = new EnetDistrict
                {
                    District = "Rural Indonesia",
                    DistrictId = 1
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1
            };
            EnetInterventionType type = new EnetInterventionType
            {
                InterventionTypeId = 1,
                Cost = 10000,
                LabourHours = 100,
                Name = "Install net"
            };
            try
            {
                new InterventionManager().EvaluateUpdates(currentUser, 1, -1, State.Approved, State.Proposed, "Rural Indonesia", 100, 10000, type);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void EngineerCompleteIntervention_OnCurrentEngineerDifferentThanProposedBy_ThrowException()
        {
            Exception exception = null;
            EnetUser currentUser = new EnetUser
            {
                EnetDistrict = new EnetDistrict
                {
                    District = "Rural Indonesia",
                    DistrictId = 1
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1
            };
            EnetInterventionType type = new EnetInterventionType
            {
                InterventionTypeId = 1,
                Cost = 10000,
                LabourHours = 100,
                Name = "Install net"
            };
            try
            {
                new InterventionManager().EvaluateUpdates(currentUser, 1, 3, State.Approved, State.Proposed, "Rural Indonesia", 100, 10000, type);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
        }
    }
}