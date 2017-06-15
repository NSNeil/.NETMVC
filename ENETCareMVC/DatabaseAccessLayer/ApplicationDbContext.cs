using ENETCare.DatabaseAccessLayer;
using Microsoft.AspNet.Identity.EntityFramework;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DatabaseAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("EnetIdentityDatabaseContext", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // SqlConnection.ClearAllPools()
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
