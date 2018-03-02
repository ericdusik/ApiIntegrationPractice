using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Configuration;
using LanguageDetectServiceTests.Helpers;
using LanguageDetectServiceTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LanguageDetectServiceTests.Tests.UITests
{

	//TO DO:
	//4. Make a PoM class for the menu.



	class LogInAndOut
	{
		//Declare WebDriver and Page Objects
		private IWebDriver _driver;
		private Home _home;
		private SignIn _signIn;
		private Dashboard _dashboard;

        string _userID =    WebConfigurationManager.AppSettings["userID"];
        string _password = WebConfigurationManager.AppSettings["userPassword"];
        string _apiKey = WebConfigurationManager.AppSettings["apiKey"];
        string _siteURL = WebConfigurationManager.AppSettings["siteURL"];

        [OneTimeSetUp]
		public void oneTimeSetup()
		{
			//Instanciate our WebDriver
			_driver = SeleniumHelper.CreateChromeWebDriver();

        }

		[OneTimeTearDown]
		public void oneTimeTearDown()
		{
			_driver.Quit();
		}

		[TearDown]
		public void CleanUpForEveryTest()
		{
			SeleniumHelper.TakeScreenShotOnTestFail(_driver);
		}


        [Category("Feature.UserLogin")]
        [Category("TestType.UI")]
        [Test, Description("Verify that a user can log in successfully via the front end."), Order(1)]
		public void LogInToSite()
		{
			//ARRANGE
			//Instanciate Page Objects
			Home _home = new Home(_driver);
			SignIn _signIn = new SignIn(_driver);
			Dashboard _dashboard = new Dashboard(_driver);


			SeleniumHelper.goToURL(_driver, _siteURL);
			_home.ClickSignIn();

			//ACT
			_signIn.EnterEmail(_userID);
			_signIn.EnterPassword(_password);
			_signIn.checkRememberMe();
			_signIn.ClickSignIn();
			Thread.Sleep(5000);


			//ASSERT
			Assert.AreEqual("Signed in successfully.", _dashboard.alertHeader());
			Assert.IsTrue(_dashboard.pageContentArea().Contains(_apiKey));
			Assert.IsTrue(_dashboard.userDropDownText().Contains(_userID));

		}

        [Category("Feature.UserLogout")]
        [Category("TestType.UI")]
        [Test, Description("Verify that a user can log out successfully via the front end."), Order(2)]

		public void LogOutOfSite()
		{
			//ARRANGE
			//Instanciate Page Objects
			Home _home = new Home(_driver);
			Dashboard _dashboard = new Dashboard(_driver);

			//ACT
			_dashboard.signOut();

			//ASSERT
			Assert.AreEqual("Signed out successfully.", _home.alertHeader());
		}


        [Category("Feature.UserLogout")]
        [Category("TestType.UI")]
        [Test, Description("Verify that a user is provided the correct error message after timing out."), Order(3)]

        public void ForceLogOut()
        {
            //ARRANGE
            //Instanciate Page Objects
            Home _home = new Home(_driver);
            SignIn _signIn = new SignIn(_driver);

            SeleniumHelper.goToURL(_driver, _siteURL);
            _home.ClickSignIn();
            _signIn.FillOutLoginForm(_userID, _password, false);

            //ACT
            //Delete all cookies (including session) to simulate a user time out (expiration) then refresh the page
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().Refresh();

            //ASSERT
            Assert.AreEqual("You need to sign in or sign up before continuing.", _home.errorHeader());


            
        }

    }
}
