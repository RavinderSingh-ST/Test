using AutomationCompass.Reporting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationCompass
{
    [Order(3)]
    public class DashboardPageTest : BaseClass
    {
        static readonly IWebDriver Driver = TestSetup.Driver;

        //[Test, Order(1)]
        public void SetDefaultDashboard()
        {
            NavigateToDefaultDashboardPopup();
            Element = Driver.FindElement(By.XPath("//h5[contains(text(),'My Dashboards  ')]//following::button[contains(text(),'×')][1]"));
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Element);
            Wait(4);
            GetActions().MoveToElement(Element).Click().Perform();
        }

        //[Test, Order(2)]
        public void NavigateToSpecificDashboardTest()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='Dashboard-sidebar']")));
            GetActions().MoveToElement(Element).Click().Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Select Dashboard')]"))).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//ul[@id='mnuDashboardOpenExisting']//a[contains(text(),'Created dashboard')]")));
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Element);
            Element.Click();
            GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//span[@id='divDashboardName']"), "Created dashboard"));
            Assert.AreEqual("Created dashboard", Driver.FindElement(By.XPath("//span[@id='divDashboardName']")).Text);
        }

        //[Test, Order(3)]
        public void CreateNewDashboardTest()
        {
            NavigateToCreateDashboardPage();
            EditDashboardName();
            SelectDashboardLayout();
            InsertChartFirstWidget();
            InsertChartSecondWidget();
            InsertChartThirdWidget();
            InsertChartFourthWidget();
            InsertRowAfterSecondRow();
            MoveRow();
            DeleteChartAndRowAdded();
            DeleteDashboard();
            Wait(5);
        }

        [TearDown]
        public void StatusOfTestExecuted()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            ExtentTestManager.StatusOfTest();
        }

        public void NavigateToDefaultDashboardPopup()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='Dashboard-sidebar']")));
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='Dashboard-sidebar']")));
            GetActions().MoveToElement(Element).Perform();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Set Your Default Dashboard')]")));
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Set Your Default Dashboard')]")));
            Element.Click();
        }

        public void NavigateToCreateDashboardPage()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='Dashboard-sidebar']")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@onclick='CreateNewDashboard()']"))).Click();
        }

        public void SelectDashboardLayout()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@onclick='ShowSelectLayout()']"))).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='divPreviewLayouts3']//div[@preview-data='[1,2,1]']")));
            GetActions().MoveToElement(Element).Click().Perform();
        }

        public void EditDashboardName()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@onclick='EditDashboardName()']"))).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Dashboard Name')]//following::input[1]")));
            Element.Clear();
            Element.SendKeys("Demo ST");
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@onclick='SaveDashboardName()']")));
            GetActions().Click(Element).Perform();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@id='divDashboardName']")));
            Assert.AreEqual("Demo ST", Element.Text);
        }

        public void InsertChartFirstWidget()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')][1]")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[contains(@class,'fa-plus-circle')][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Artifact Counts')]//following::button[@chart-name='TotalOpportunities']"))).Click();
        }

        public void InsertChartSecondWidget()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')][1]")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[contains(@class,'fa-plus-circle')][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Pipeline/Opportunity')]//following::button[@chart-name='impactcomplexity']"))).Click();
        }

        public void InsertChartThirdWidget()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')][1]")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[contains(@class,'fa-plus-circle')][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Processes')]//following::button[@chart-name='experimentsummary']"))).Click();
        }

        public void InsertChartFourthWidget()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')][1]")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[contains(@class,'fa-plus-circle')][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Other')]//following::button[@chart-name='PainvsGain']"))).Click();
        }

        public void InsertRowAfterSecondRow()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@title='Process Summary']")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@title='Process Summary']//following::button[@data-original-title='Add Row'][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[contains(@class,'btn-add-row') and contains(text(),'1')])[3]"))).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')][1]")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//i[contains(@class,'fa-plus-circle')][1]"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(),'Pipeline/Opportunity')]//following::button[@title='Pipeline Summary']"))).Click();
        }

        public void MoveRow()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@title='Pipeline Summary']")));
            GetActions().MoveToElement(Element).Perform();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//h3[contains(text(),'Pipeline Summary')]//following::button[@data-original-title='Move Entire Row'][1]")));
            GetActions().MoveToElement(Element)
            .ClickAndHold(Element)
            .MoveByOffset(-70, -90)
            .MoveByOffset(-70, -90)
            .Release().Perform();
        }

        public void DeleteChartAndRowAdded()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//h3[contains(text(),'Pipeline Summary')]//following::button[@onclick='DeleteChart(this);'][1]"))).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')]")));
            GetActions().MoveToElement(Element).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(@class,'draggable-blank')]//following::button[@data-original-title='Remove Row'][1]"))).Click();
        }

        public void DeleteDashboard()
        {
            Wait(3);
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@onclick='DeleteDashboard()']"))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Yes, do it!')]"))).Click();
        }
    }
}