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
	class SignIn
	{

		private IWebDriver _driver;

		public SignIn(IWebDriver driver)
		{
			_driver = driver;
			PageFactory.InitElements(_driver, this);
		}

		//Home Page Element Locators
		[FindsBy(How = How.Id, Using = "user_email")]
		private IWebElement emailInput { get; set; }

		[FindsBy(How = How.Id, Using = "user_password")]
		private IWebElement passwordInput { get; set; }

		[FindsBy(How = How.Id, Using = "user_remember_me")]
		private IWebElement rememberMeCheckBox { get; set; }

		[FindsBy(How = How.CssSelector, Using = "input[name='commit']")]
		private IWebElement signInButton { get; set; }


		//Home Page Element Interaction Methods
		public void EnterEmail(string email)
		{
			emailInput.SendKeys(email);
		}

		public void EnterPassword(string password)
		{
			passwordInput.SendKeys(password);
		}

		public void checkRememberMe()
		{
			rememberMeCheckBox.Click();
		}

		public void ClickSignIn()
		{
			signInButton.Click();
		}
	}
}
