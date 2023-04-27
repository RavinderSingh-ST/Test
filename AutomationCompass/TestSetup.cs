using AutomationCompass.Reporting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationCompass
{
    [SetUpFixture]
    public class TestSetup
    {
        public static IWebDriver Driver;
        private const string Password = "Jax37786";

        [OneTimeSetUp]
        public void Setup()
        {
            //string browserName = TestContext.Parameters.Get("ChromeBrowser");
            //switch (browserName.ToLower())
            //{
            //    case "chrome":
            //        ChromeOptions options = new ChromeOptions();
            //        options.AddArgument("--headless");
            //        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //        Driver = new ChromeDriver(options);
            //        Driver.Manage().Window.Maximize();
            //        Driver.Navigate().GoToUrl(TestContext.Parameters.Get("AppUrl"));
            //        Driver.FindElement(By.XPath("//a[contains(text(),'Login')]")).Click();
            //        break;

            //    case "firefox":
            //        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //        Driver = new FirefoxDriver();
            //        Driver.Manage().Window.Maximize();
            //        Driver.Navigate().GoToUrl(TestContext.Parameters.Get("AppUrl"));
            //        Driver.FindElement(By.XPath("//a[contains(text(),'Login')]")).Click();
            //        break;
            //    default:
            //        throw new ArgumentException($"Invalid browser name: {browserName}");
            //}
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://dev.myautomationcompass.com");
            Driver.FindElement(By.XPath("//a[contains(text(),'Login')]")).Click();
        }

        public void SendMailWithReport()
        {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ravinder.singh@supremetechnologiesindia.com");
                message.To.Add(new MailAddress("singhravinder1808999@gmail.com"));
                message.Subject = "Test Results";
                message.Body = "Please see attached test report.";
                string PathToDirectory = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
                string ReportPath = PathToDirectory + @"/../../Report/index.html";
                Attachment attachment = new Attachment(ReportPath);
                message.Attachments.Add(attachment);

                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("ravinder.singh@supremetechnologiesindia.com", Password);
                client.Send(message);
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            //Driver.Quit();
            ExtentManager.GetExtent().Flush();
            //System.Diagnostics.Process.Start(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory) + @"/../../Report/index.html");
            //SendMailWithReport();
        }
    }
}