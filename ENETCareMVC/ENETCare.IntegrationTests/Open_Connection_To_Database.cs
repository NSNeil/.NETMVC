using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using ENETCareWebApp.Controllers;
using BusinessLogicLayer.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ENETCare.IntegrationTests
{
    [TestClass]
    public class Open_Connection_To_Database
    {
        [TestMethod()]
        public void IdentityDb_Connection_Should_OpenClose_And_Not_Throw_An_Error()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnetIdentityDatabaseContext"].ConnectionString;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                conn.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod()]
        public void EnetCareEntitiesDB_Connection_Should_OpenClose_And_Not_Throw_An_Error()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnetCareEntities"].ConnectionString;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                conn.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
