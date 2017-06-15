using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IntegrationTests
{
    [TestClass]
    public class SetupDBs
    {
        [AssemblyInitialize]
        public static void SetupDataDirectory(TestContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(@"..\..\..\ENETCareWebApp\App_Data\"));
            AppDomain.CurrentDomain.SetData("EnetCareEntities", Path.GetFullPath(@"..\..\..\ENETCareWebApp\App_Data\"));
        }
    }
}