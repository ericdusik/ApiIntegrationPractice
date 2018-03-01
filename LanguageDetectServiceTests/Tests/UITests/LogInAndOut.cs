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
	//1. Write a better wait method
	//2. Tagging
	//3. Do more front end work
	//4. Make a PoM class for the menu.
	//5. Move utilities out of SeleniumHelper to Utility.


	class LogInAndOut
	{
		//Declare WebDriver and Page Objects
		private IWebDriver _driver;
		private Home _home;
		private SignIn _signIn;
		private Dashboard _dashboard;


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


		[Test]
		public void LogInToSite()
		{
			//ARRANGE
			//Instanciate Page Objects
			Home _home = new Home(_driver);
			SignIn _signIn = new SignIn(_driver);
			Dashboard _dashboard = new Dashboard(_driver);


			SeleniumHelper.goToURL(_driver, WebConfigurationManager.AppSettings["aut"]);
			_home.ClickSignIn();
			Thread.Sleep(5000);

			//ACT
			_signIn.EnterEmail(WebConfigurationManager.AppSettings["userID"]);
			_signIn.EnterPassword(WebConfigurationManager.AppSettings["userPassword"]);
			_signIn.checkRememberMe();
			_signIn.ClickSignIn();
			Thread.Sleep(5000);

			//ASSERT
			Assert.AreEqual("Signed in successfully.", _dashboard.alertHeader());
			Assert.IsTrue(_dashboard.pageContentArea().Contains(WebConfigurationManager.AppSettings["apiKey"]));
			Assert.IsTrue(_dashboard.userDropDownText().Contains(WebConfigurationManager.AppSettings["userID"]));

		}

		[Test]
		public void LogOutOfSite()
		{
			//ARRANGE
			//Instanciate Page Objects
			Home _home = new Home(_driver);
			Dashboard _dashboard = new Dashboard(_driver);

			//ACT
			_dashboard.signOut();
			Thread.Sleep(5000);

			//ASSERT
			Assert.AreEqual("Signed out successfully.", _home.alertHeader());
		}

	}
}
