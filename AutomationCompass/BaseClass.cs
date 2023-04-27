using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;

namespace AutomationCompass
{
    public class BaseClass
    {
        static IWebDriver Driver = TestSetup.Driver;
        public IWebElement Element;

        public WebDriverWait GetWebDriverWait() => new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        public Actions GetActions() => new Actions(Driver);
        public SelectElement Select(IWebElement element) => new SelectElement(element);

        public void NavigateToHomePage()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@id='Home-sidebar']")));
            GetActions().MoveToElement(Element).Click().Perform();
        }

        public void UserSignOutTest()
        {
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@id='divProfileHeaderImage']")));
            Element.Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Sign out')]")));
            Element.Click();
        }

        public IJavaScriptExecutor ExecuteJavaScript()
        {
            GetWebDriverWait().Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            return js;
        }

        public void Wait(int delay)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(delay));
            wait.Until(Driver => (DateTime.Now - now) - TimeSpan.FromSeconds(delay) > TimeSpan.Zero);
        }

        public static string GetDateAfterDays(int days)
        {
            DateTime currentDate = DateTime.Now;
            DateTime dateAfterDays = currentDate.AddDays(days);
            return dateAfterDays.ToString("MM/dd/yyyy");
        }

        public string FetchDataFromExcel(int row, int col)
        {
            try
            {
                string PathToDirectory = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
                FileStream file = new FileStream(PathToDirectory + @"/../../TestData/AutomationCompassTestData.xlsx", FileMode.Open, FileAccess.Read);
                XSSFWorkbook workbook = new XSSFWorkbook(file);
                ISheet sheet = workbook.GetSheet("Sheet1");
                IRow rowNumber = sheet.GetRow(row);
                string cellValue = "";
                if (rowNumber != null)
                {
                    ICell cell = rowNumber.GetCell(col);
                    if (cell != null)
                    {
                        cellValue = cell.ToString();
                        return cellValue;
                    }
                    else
                    {
                        throw new Exception("Cell is empty");
                    }
                }
                else
                {
                    throw new Exception("Row is empty");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }
        }

        public string SwitchToWindow(int index)
        {
            string mainWindowHandle = Driver.CurrentWindowHandle;
            string windowHandle = "";
            int i = 0;
            foreach (string handle in Driver.WindowHandles)
            {
                if (i == index)
                {
                    windowHandle = handle;
                    break;
                }
                i++;
            }
            Driver.SwitchTo().Window(windowHandle);
            return windowHandle;
        }
    }
}