using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LanguageDetectServiceTests.PageObjects
{
	class Dashboard
	{

		private IWebDriver _driver;

		public Dashboard(IWebDriver driver)
		{
			_driver = driver;
			PageFactory.InitElements(_driver, this);
		}

		//Dashboard Page Element Locators
		[FindsBy(How = How.ClassName, Using = "content")]
		private IWebElement contentArea { get; set; }

		[FindsBy(How = How.ClassName, Using = "alert-info")]
		private IWebElement alertArea { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@class=\'btn dropdown-toggle\']")]
		private IWebElement userDropDown { get; set; }

		[FindsBy(How = How.LinkText, Using = "Sign Out")]
		private IWebElement signOutLink { get; set; }




		//Dashboard Text Accessor
		public string alertHeader()
		{
			return alertArea.Text;
		}

		public string pageContentArea()
		{
			return contentArea.Text;
		}

		public string userDropDownText()
		{
			return userDropDown.Text;
		}

		//Dashboard Page Element Interaction Methods
		public void clickDropDown()
		{
			userDropDown.Click();
		}

		public void signOut()
		{
			userDropDown.Click();
			signOutLink.Click();
		}


	}
}
