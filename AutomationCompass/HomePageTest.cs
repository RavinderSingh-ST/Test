using AutomationCompass.Reporting;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace AutomationCompass
{
    [Order(2)]
    public class HomePageTest : BaseClass
    {
        static readonly IWebDriver Driver = TestSetup.Driver;

        //[Test, Order(1)]
        public void DashboardTest()
        {
            NavigateToDashboardPage();
            EditDashboardName();
            AddRowsToDashboard();
            InsertChartToRows();
            DeleteRowsFromDashboard();
            NavigateToHomePage();
        }

        //[Test, Order(2)]
        public void ManageProcessesTest()
        {
            NavigateToHomePage();
            NavigateToManageProcessesPage();
            //SearchProcessByname();
            //NavigateToProcessSelected();
            ProcessFilter();
        }

        //[Test, Order(3)]
        public void SelectPipelineTest()
        {
            NavigateToHomePage();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'cus-pipeline-dropdown')]")));
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);",Element);
            Element.Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@class='navi-link']/span[contains(text(),'Demo')]")));
            Element.Click();
        }

        [TearDown]
        public void StatusOfTestExecuted()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            ExtentTestManager.StatusOfTest();
        }

        public void AddRowsToDashboard()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='divGettingStartedDashboard1']/div[contains(@class,'dropleft')]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'btn-add-row-main') and contains(text(),'2')]"))).Click();
        }

        public void InsertChartToRows()
        {
            Element = Driver.FindElement(By.XPath("//div[contains(@class,'draggable-blank')][1]"));
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Element);
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[contains(@class,'addbutton')])[1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@chart-name='TotalPipelines']"))).Click();
            Element = Driver.FindElement(By.XPath("//div[contains(@class,'draggable-blank')][1]"));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'addbutton')]/i[contains(@class,'fa-plus-circle')][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@chart-name='opportunitybreakdown']"))).Click();
        }

        public void DeleteRowsFromDashboard()
        {
            IList<IWebElement> DashboardRows = Driver.FindElements(By.XPath("//div[contains(@class,'row draggable-container')]"));
            int RowCount = DashboardRows.Count;
            string RowToDeleteXpath = $"//div[contains(@class,'row draggable-container')][{RowCount}]";
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementExists(By.XPath(RowToDeleteXpath)));
            GetActions().MoveToElement(Element).Perform();
            string DeleteRowBtn = $"//div[contains(@class,'row draggable-container')][{RowCount}]//button[contains(@class,'btn-remove-row')]";
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(DeleteRowBtn))).Click();
        }

        public void EditDashboardName()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@onclick='EditDashboardName()']")));
            Wait(2);
            GetActions().MoveToElement(Element).Click().Perform();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Dashboard Name')]//following::input[1]")));
            string DashboardName = Element.GetAttribute("value");
            Element.Clear();
            Element.SendKeys(DashboardName);
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@onclick='SaveDashboardName()']")));
            Wait(2);
            Element.Click();
        }

        public void ProcessFilter()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@id='tblProjects']//th[contains(text(),'Process Name')]")));
            Assert.IsTrue(Element.Displayed);
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'buttons-colvis')]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a//span[contains(text(),'Process Name')]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//table[@id='tblProjects']//th[contains(text(),'Process Name')]")));
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a//span[contains(text(),'Process Name')]"))).Click();
        }

        public void SearchProcessByname()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='tblProjects_filter']//input")));
            string ProcessToSearch = "(Demo) AP Process";
            Element.Clear();
            Element.SendKeys(ProcessToSearch);
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td/a[contains(text(),'(Demo) AP Process')]")));
            Assert.AreEqual(ProcessToSearch, Element.Text);
        }

        public void NavigateToProcessSelected()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td/a[contains(text(),'(Demo) AP Process')]"))).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@id='txtSimulationName']")));
            Assert.AreEqual("(Demo) AP Process", Element.GetAttribute("value"));
            Driver.Navigate().Back();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='tblProjects_filter']//input")));
            Element.Clear();
            Element.SendKeys(Keys.Backspace);
        }

        public void NavigateToDashboardPage()
        {
            NavigateToHomePage();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'My Dashboard')]"))).Click();
        }

        public void NavigateToManageProcessesPage()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Manage Processes')]"))).Click();
        }
    }
}