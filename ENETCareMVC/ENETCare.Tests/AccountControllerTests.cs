using BusinessLogicLayer.Models;
using ENETCareWebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Create_Get_LoginInViewBag()
        {
            using (var controller = new AccountController())
            {
                var result = (ViewResult)controller.Login("www.blah.com");

                Assert.IsNotNull(result);
                Assert.AreEqual("www.blah.com", result.ViewData["ReturnUrl"]);

            }
        }

        [TestMethod]
        public async Task Create_PostInvalid_NoRedirectAsync()
        {
            using (var controller = new AccountController())
            {
                var logonModel = new LoginViewModel() { AccountName = "Dean", Password = "123456" };
                // Add a validation error
                controller.ModelState.AddModelError("", "Validation Error");

                var result = await controller.Login(logonModel, "www.blah.com") as ViewResult;
                // Assert.IsTrue(result.ViewName == "" || result.ViewName == "Create");
                
                Assert.IsFalse(result.ViewData.ModelState.IsValid);
            }
        }



        //[TestMethod]
        //public void Create_PostOk_SavesToService()
        //{
        //    Mock<EmployeeService> service = new Mock<EmployeeService>();
        //    Employee saved = null;
        //    service.Setup(x => x.Add(It.IsAny<Employee>())).Returns<Employee>(e => { saved = e; return 100; });
        //    var controller = new EmployeeController(service.Object);

        //    Employee formData = new Employee
        //    {
        //        FirstName = "First",
        //        LastName = "Last",
        //        IsFullTime = true,
        //        DepartmentId = 5
        //    };
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(formData);

        //    Assert.AreSame(formData, saved);
        //    Assert.AreEqual(100, result.RouteValues["id"]);
        //    Assert.AreEqual("Details", result.RouteValues["action"]);
        //}
    }


}
