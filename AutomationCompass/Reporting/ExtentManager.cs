using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.IO;

namespace AutomationCompass.Reporting
{
    public class ExtentManager
    {
        private static ExtentReports extent;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                string PathToDirectory = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
                Console.WriteLine(PathToDirectory);
                string ReportPath = PathToDirectory + @"/../../Report/Report.html";
                var htmlReporter = new ExtentHtmlReporter(ReportPath);
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);

                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                htmlReporter.Config.DocumentTitle = "Extent Report";
                htmlReporter.Config.ReportName = "Automation compass test";
                htmlReporter.Config.EnableTimeline = true;
            }
            return extent;
        }
    }
}