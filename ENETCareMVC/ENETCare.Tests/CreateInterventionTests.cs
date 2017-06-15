using BusinessLogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BusinessLogicLayer.Models;

namespace ENET.Tests
{
    [TestClass]
    public class CreateInterventionTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void CreateIntervention_OnSuccess_ReturnTrue()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Proposed, 100, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateIntervention_OnStateInvalid_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, (State) 10, 100, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateIntervention_OnApprovedStateWithInsufficientMaxLabourHours_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Approved, 100, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateIntervention_OnApprovedStateWithInsufficientMaxCost_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 10,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Approved, 100, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateIntervention_OnDateToPerformIsEarlierThanNow_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(1991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Proposed, 100, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateIntervention_OnClientDistrictDifferentThanEngineer_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 2,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "New York"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Proposed, 100, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateIntervention_OnCostInputLowerThanDefault_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Proposed, 100, 10,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateIntervention_OnLabourHoursInputLowerThanDefault_ReturnFalse()
        {
            bool result = false;
            EnetInterventionType type = new EnetInterventionType
            {
                Cost = 10000,
                LabourHours = 100,
                InterventionTypeId = 1,
                Name = "Supply and Install Portable Toilet"
            };
            EnetClient client = new EnetClient
            {
                Address = "Pitt St",
                ClientId = 1,
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                Name = "John Lennon"
            };
            EnetUser currentUser = new EnetUser
            {
                DistrictId = 1,
                EnetDistrict = new EnetDistrict
                {
                    DistrictId = 1,
                    District = "Sydney"
                },
                MaxHours = 1000,
                MaxCost = 100000,
                Name = "Neil",
                UserId = 1,
            };
            DateTime dateToPerform = new DateTime(2991, 9, 23);
            try
            {
                result = new InterventionManager().CreateIntervention(currentUser, type, State.Proposed, 1, 10000,
                "OK", dateToPerform, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.IsFalse(result);
        }
    }
}