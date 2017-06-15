using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DatabaseAccessLayer;
using System.Data.SqlClient;
using BusinessLogicLayer.Models;

namespace ENETCare.DatabaseAccessLayer
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }


        public static void InitializeIdentityForEF(ApplicationDbContext context)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "SiteEngineer"},
                new IdentityRole { Name = "Manager"},
                new IdentityRole { Name = "Accountant"}

            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            foreach (var role in roles)
            {
                roleManager.Create(role);
            }


            var users = new List<ApplicationUser>
                {
                    new ApplicationUser { UserName = "Dean" },
                    new ApplicationUser { UserName = "Kim" },
                    new ApplicationUser { UserName = "Sam" },
                    new ApplicationUser { UserName = "Mark" }
                };

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            foreach (var user in users)
            {
                userManager.Create(user, "123456");
            }
            
            userManager.AddToRole(userManager.FindByName("Dean").Id, "SiteEngineer");
            userManager.AddToRole(userManager.FindByName("Kim").Id, "SiteEngineer");
            userManager.AddToRole(userManager.FindByName("Sam").Id, "Manager");
            userManager.AddToRole(userManager.FindByName("Mark").Id, "Accountant");
        }
    }
}