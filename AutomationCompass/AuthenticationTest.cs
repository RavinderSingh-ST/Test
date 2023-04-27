using AutomationCompass.Reporting;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationCompass
{
    [Order(1)]
    public class AuthenticationTest : BaseClass
    {
        static readonly IWebDriver Driver = TestSetup.Driver;
        static readonly ExtentTest Test = ExtentTestManager.Test;

        [SetUp]
        public void FetchTestName()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            SwitchToWindow(1);
            Driver.FindElement(By.XPath("//input[@name='txtLoginEmail']")).SendKeys(FetchDataFromExcel(1, 0));
            ExtentTestManager.TestSteps("Enter valid username");
            Driver.FindElement(By.XPath("//input[@name='txtLoginPassword']")).SendKeys(FetchDataFromExcel(1, 1));
            ExtentTestManager.TestSteps("Enter valid password");
            Driver.FindElement(By.XPath("//button[contains(text(),'Log In')]")).Click();
            ExtentTestManager.TestSteps("Click login button");
            GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[contains(text(),'Powered by')]"), "Powered by"));
        }

        [TearDown]
        public void StatusOfTestExecuted()
        {
            ExtentTestManager.StatusOfTest();
        }
    }
}