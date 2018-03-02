using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;


namespace LanguageDetectServiceTests.Helpers
{
	class SeleniumHelper
	{

		public static IWebDriver CreateChromeWebDriver()
		{
			var options = new ChromeOptions();
			options.AddArgument("ignore-certificate-errors");
			options.AddArgument("no-sandbox");
			options.AddArgument("--start-maximized");

			//Disable the "Chrome is being controlled by automated test software" bar
			options.AddArgument("disable-infobars");
			//Disable the "would you like to save your password for this site" flyout
			options.AddUserProfilePreference("credentials_enable_service", false);

			IWebDriver driver = new ChromeDriver();
			return driver;

		}

		//Setup + Teardown Utility Methods. Only used with Selenium tests.
		public static void TakeScreenShot(IWebDriver driver, string fileName)
		{
			string today = DateTime.Now.Date.ToString("yyyy_MM_dd");

			string filePath = Path.Combine($"C://API_TEST_LOGS//{today}", DateTime.Now.Date.ToString("yyyy_MM_dd")) +
							  DateTime.Now.ToString("yy-MM-dd-hh-mm ") + fileName + ".jpg";
			Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
			ss.SaveAsFile(filePath, ScreenshotImageFormat.Jpeg);
		}

		public static void TakeScreenShotOnTestFail(IWebDriver driver)
		{
			if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
			{
				SeleniumHelper.TakeScreenShot(driver, TestContext.CurrentContext.Test.Name);
			}
		}


		//Page State + Control Methods
		public static void goToURL(IWebDriver driver, string URL)
		{
			driver.Navigate().GoToUrl(URL);
		}






	}
}
