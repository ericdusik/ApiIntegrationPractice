using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace LanguageDetectServiceTests.PageObjects
{
	class Home
	{

		private IWebDriver _driver;

		public Home(IWebDriver driver)
		{
			_driver = driver;
			PageFactory.InitElements(_driver, this);
		}

		//Home Page Element Locators
		[FindsBy(How = How.LinkText, Using = "Sign In")]
		private IWebElement signInButton { get; set; }

		[FindsBy(How = How.ClassName, Using = "alert-info")]
		private IWebElement alertArea { get; set; }

        [FindsBy(How = How.ClassName, Using = "alert-error")]
        private IWebElement alertError { get; set; }



		//Home Page Element Interaction Methods
		public void ClickSignIn()
		{
			signInButton.Click();
		}

		public string alertHeader()
		{
			return alertArea.Text;
		}

        public string errorHeader()
        {
            return alertError.Text;
        }


	}
}
