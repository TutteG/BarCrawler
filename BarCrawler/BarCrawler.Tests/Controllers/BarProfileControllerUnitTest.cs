﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using BarCrawler.Controllers;
using BarCrawler.Migrations;
using BarCrawler.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using BarCrawler.ViewModels;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BarCrawler.Tests.Controllers
{

    /*******************************************************
     
         READ ME GOD DAMMIT
        Forsøg at gå i krig med covertest først og fremmest, hvis der er tid og/eller overskud,
        så kan vi overveje om vi skal lave nogle test på data, men cover tager absolut prioritet.

         
 ************************************/



    [TestFixture]
    public class BarProfileControllerUnitTest
    {
        private BarProfilController controller;


        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new BarCrawlerContextInitializer<BarCrawlerContext>());
            controller = new BarProfilController();
        }


        #region Index

        [Test]
        public void Index_NotNull_ExpectedTrue()
        {
            // Arrange

            // Act
            ViewResult result = controller.Index(1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Index_BarIdDoesNotExistViewIsNull_ExpectedTrue()
        {
            // Arrange

            // Act
            ViewResult result = controller.Index(3) as ViewResult;
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Index_BarIdDoesNotExistHttpStatusCodeNotFound_ExpectedTrue()
        {
            // Act
            var result = controller.Index(3) as HttpStatusCodeResult;
            // Assert
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }


        [Test]
        public void Index_Null_ExpectedBadRequest()
        {
            var result = controller.Index(null) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        //Lav kontrol af at der er det rigtige antal drinks, events og feeds


        #endregion

        #region Feed

        #region Create Feed

        [Test]
        public void CreateFeed_CreateFeedWithText_ExpectTrue()
        {
            var result = controller.CreateFeed(1, "Unit test of Create Feed") as ActionResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateFeed_FeedRedirectedToActionWith2Parameters_ExpectTrue()
        {
            var result = controller.CreateFeed(1, "Unit test of Create Feed") as RedirectToRouteResult;

            Assert.That(result.RouteValues.Values.Count, Is.EqualTo(2));
        }

        [Test]
        public void CreateFeed_RedirectedToActionWithCorrectId_ExpectedTrue()
        {
            const int id = 1;
            var result = controller.CreateFeed(id, "Unit test of Create Feed") as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue(id), Is.True);
        }

        [Test]
        public void CreateFeed_RedirectedToCorrectAction_ExpectedTrue()
        {
            const string str = "Index";
            var result = controller.CreateFeed(1, "Unit test of Create Feed") as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue(str), Is.True);
        }

        /*[Test]
        public void CreateFeed_NumberOfFeedsIs2_ExpectTrue()
        {
            const int BarId = 1;
            controller.CreateFeed(BarId, "Unit test of Create Feed");
            var result = controller.Index(BarId) as ViewResult;

            var model = (BarModel) result.Model;

            Assert.That(model.Feeds.Count, Is.EqualTo(2));
        }*/

        [Test]
        public void CreateFeed_StringToWriteIsEmptyRedirectToCorrectAction_ExpectTrue()
        {
            var result = controller.CreateFeed(1, "") as RedirectToRouteResult;
            Assert.That(result.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void CreateFeed_StringToWriteIsEmptyRedirectWithCorrectId_ExpectTrue()
        {
            const int id = 1;
            var result = controller.CreateFeed(id, "") as RedirectToRouteResult;
            Assert.That(result.RouteValues.ContainsValue(id), Is.True);
        }

        /*[Test]
        public void CreateFeed_StringToWriteIsEmptyNumberOfFeedsIs1_ExpectTrue()
        {
            const int id = 1;
            controller.CreateFeed(id, "");
            var result = controller.Index(id) as ViewResult;

            var model = (BarModel)result.Model;

            Assert.That(model.Feeds.Count, Is.EqualTo(1));
        }*/

        #endregion

        #region Delete Feed

        /*[Test]
        public void DeleteFeed_Delete1Feed0FeedsLeftInDatabase_ExpectTrue()
        {
            const int BarID = 1;
            controller.DeleteFeed(BarID, 1);
            var result = controller.Index(BarID) as ViewResult;

            var model = (BarModel)result.Model;
            
            Assert.That(model.Feeds.Count, Is.EqualTo(0));
        }*/


        [Test]
        public void DeleteFeed_FeedRedirectedToActionWith2Parameters_ExpectTrue()
        {
            var result = controller.DeleteFeed(1, 1) as RedirectToRouteResult;

            Assert.That(result.RouteValues.Values.Count, Is.EqualTo(2));
        }

        //[Test]
        //public void DeleteFeed_RedirectedToActionWithCorrectId_ExpectedTrue()
        //{
        //    const int id = 1;
        //    var result = controller.CreateFeed(id, "Unit test of Create Feed") as RedirectToRouteResult;

        //    Assert.That(result.RouteValues.ContainsValue(id), Is.True);
        //}

        //[Test]
        //public void DeleteFeed_RedirectedToCorrectAction_ExpectedTrue()
        //{
        //    const string str = "Index";
        //    var result = controller.CreateFeed(1, "Unit test of Create Feed") as RedirectToRouteResult;

        //    Assert.That(result.RouteValues.ContainsValue(str), Is.True);
        //}

        #endregion

        #endregion

        #region Drink

        #region Create Drink

        #region Make View

        private ApplicationSignInManager _signInManager;

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}



        /*[Test]
        public void CreateDrink_CreateDrinkViewForActualBar_ExpectTrue()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            var roleStore = new RoleStore<IdentityRole>(applicationDbContext);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(applicationDbContext);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var signIn = new ApplicationSignInManager(userManager);

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            var restult = controller.CreateDrink(1) as ViewResult;

            Assert.IsNotNull(restult);
        }*/

        [Test]
        public void CreateDrink_CreateDrinkViewForNotExistingBar_ExpectTrue()
        {
            var restult = controller.CreateDrink(0) as HttpStatusCodeResult;

            Assert.That(restult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }


        #endregion

        #region Save Data

        [Test]
        public void CreateDrink_SaveEnteredDrinkMeetsRequirementsRedirectToIndex_ExpectTrue()
        {
            DrinkModel drink = new DrinkModel()
            {
                BarID = 1,
                Description = "Text goes here",
                Price = 60,
                Title = "Memory leak"
            };

            var result = controller.CreateDrink(drink) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void CreateDrink_SaveEnteredDrinkMeetsRequirementsRedirectToCorrectBar_ExpectTrue()
        {
            DrinkModel drink = new DrinkModel()
            {
                BarID = 1,
                Description = "Text goes here",
                Price = 60,
                Title = "Memory leak"
            };

            var result = controller.CreateDrink(drink) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue(1), Is.True);
        }

        [Test]
        public void CreateDrink_SaveEnteredDrinkMeetsRequirementsBarIdDoesNotExist_ExpectTrue()
        {
            DrinkModel drink = new DrinkModel()
            {
                BarID = 3,
                Description = "Text goes here",
                Price = 60,
                Title = "Memory leak"
            };

            var result = controller.CreateDrink(drink) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        #endregion

        #endregion

        #region Edit Drink

        #region Create View

        /*[Test]

        public void EditDrink_ValidDrinkIdValidBarIdNotLoggedIn_ExpectTrue()
        {
            var result = controller.EditDrink(1, 1) as HttpStatusCodeResult;

            int ting = (int)HttpStatusCode.BadRequest;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }*/

        [Test]
        public void EditDrink_ValidDrinkIdNotValidBarId_ExpectTrue()
        {
            var result = controller.EditDrink(1, 0) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public void EditDrink_NotValidDrinkIdValidBarId_ExpectTrue()
        {
            var result = controller.EditDrink(0, 1) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        [Test]
        public void EditDrink_NotValidDrinkIdNotValidBarId_ExpectTrue()
        {
            var result = controller.EditDrink(0, 0) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        [Test]
        public void EditDrink_DrinkIdIsNullValidBarId_ExpectTrue()
        {
            var result = controller.EditDrink(null, 1) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public void EditDrink_ValidDrinkIdBarIdIsNull_ExpectTrue()
        {
            var result = controller.EditDrink(1, null) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public void EditDrink_DrinkIdIsNullBarIdIsNull_ExpectTrue()
        {
            var result = controller.EditDrink(null, null) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        #endregion

        #region Save Data

        [Test]
        public void EditDrink_DrinkViewModelIsValid_ExpectIndexReturned()
        {
            DrinkViewModel model = new DrinkViewModel()
            {
                Title = "Memory Leak",
                BarID = 1,
                DrinkID = 1,
                Description = "It is leaking",
                Price = 60
            };

            var result = controller.EditDrink(model) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void EditDrink_DrinkViewModelIsValid_ExpectCorrectBarIdReturned()
        {
            DrinkViewModel model = new DrinkViewModel()
            {
                Title = "Memory Leak",
                BarID = 1,
                DrinkID = 1,
                Description = "It is leaking",
                Price = 60
            };

            var result = controller.EditDrink(model) as RedirectToRouteResult;

            Assert.That(result.RouteValues.ContainsValue("Index"), Is.True);
        }

        [Test]
        public void EditDrink_DrinkIdDoesNotExist_ExpectTrue()
        {
            DrinkViewModel model = new DrinkViewModel()
            {
                Title = "Memory Leak",
                BarID = 1,
                DrinkID = 0,
                Description = "It is leaking",
                Price = 60
            };

            var result = controller.EditDrink(model) as HttpStatusCodeResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        [Test]
        [PostTest]
        public void EditDrink_DrinkViewModelIsNotValidPriceIsNull_Expect()
        {
            DrinkViewModel model = new DrinkViewModel()
            {
                Title = "Memory Leak",
                BarID = 1,
                DrinkID = 1,
                Description = "It is leaking"
            };
            controller.ModelState.AddModelError("Test", "Errormessage goes here");
            var result = controller.EditDrink(model) as ViewResult;

            Assert.IsNotNull(result);
        }


        #endregion

        #endregion

        #region Delete Drink

        //Skal først laves når Vi har fundet af hvordan man fikser databasen, sådan at den er nulstillet hver gang

        #endregion



        #endregion


        [Test]
        public void BadRequest_IsNotNull_ExpectedTrue()
        {
            ViewResult result = controller.BadRequestView() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BarLink_UserNotLoggedIn_ThrowException()
        {
            Assert.Throws<NullReferenceException>(() => controller.BarLink());
        }

        [Test]
        public void BarLink_UserLoggedIn_ExpectSomethingWhoKnowWhat()
        {

            //Lav noget med at logge ind som en bruger.
            //Log ind som kk@ase.au.dk
            //Prøv herefter at kalde barlink
            //Hvis det ikke virker, så bare kom ned på værkstedet. Keder mig sikkert ;P
        }
    }
}
