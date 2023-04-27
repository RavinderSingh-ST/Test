using AutomationCompass.Reporting;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace AutomationCompass
{
    [Order(4)]
    class EndToEndTest : BaseClass
    {
        static readonly IWebDriver Driver = TestSetup.Driver;

        string[] GainGoals = { "None", "A Little", "A Moderate Contribution", "A Large Contribution", "A Significant Contribution", "A Large Contribution", "A Moderate Contribution", "A Little" };
        string[] PainGoals = { "Always", "Mostly", "Sometimes", "Rarely", "Never", "Mostly", "Sometimes" };
        string[] SimulationActivities = { "Post external", "Use external firm?", "Extend offer" };
        string[] NumberOfResources = { "1", "2", "1", "1", "2" };
        string[] CostOfResources = { "50", "50", "200", "100", "50" };

        string GainDropdowns = "(//select[contains(@class,'gain-select2')])[{0}]";
        string PaindDropdowns = "(//select[contains(@class,'pain-select2')])[{0}]";
        string ScenarioActivitiesCheckbox = "//div[@id='checkBoxes']//div[contains(text(),'{0}')]";
        string ActivityDetailTimeInputBox = "//input[@id='{0}']";
        string ActivityDetailTimeUnitDropdown = "//select[@id='{0}']";
        string SimulationAdvanceToggleSwitch = "//input[@id='{0}']//following::label[1]";
        string SimulationScheduleMonths = "//div[contains(@class,'schedulePickMonths')]//p[contains(text(),'{0}')]";
        string SimulationWorkingTime = "//select[contains(@class,'{0}')]";
        string SimulationWorkingDays = "//input[@id='{0}']//following::label[1]";
        string EditScheduleBtn = "//div[contains(text(),'{0}')]//following::div[contains(@class,'editSchedule')][{1}]";
        string DuplicateScheduleBtn = "//div[contains(text(),'{0}')]//following::div[contains(@class,'duplicateSchedule')][{1}]";
        string DeleteScheduleBtn = "//div[contains(text(),'{0}')]//following::div[contains(@class,'deleteSchedule')][{1}]";
        string ResourceEditButton = "//table[contains(@class,'resources__table')]//tbody//tr[{0 }]//span";
        string NameOfResource = "(//table[contains(@class,'resources__table')]//td[@class='sorting_1'])[{0}]";

        By ProcessSideMenuOption = By.XPath("//div[@id='Process-sidebar']");
        By CreateNewProcessDropdown = By.XPath("//span[contains(text(),'Create New Process')]");
        By StartFromTemplateDropdownOption = By.XPath("//a[contains(text(),'Start from a Template')]");
        By FilterTemplateInputBox = By.XPath("//input[@id='txtTemplateFilters']");
        By GeneralHiringTemplate = By.XPath("//h4[contains(text(),'Import a Pre-Built Template')]//following::div[contains(text(),'Hiring - General')][2]");
        By ProcessNameInputBox = By.XPath("//label[contains(text(),'Process Name')]//following::input[1]");
        By ProcessOwnerInputBox = By.XPath("(//input[@class='select2-search__field'])[1]");
        By CapabilityDropdown = By.XPath("//select[@id='selectSimulationCapability']");
        By CategorySelectDropown = By.XPath("//select[@id='selectSimulationCategory']");
        By SaveAllBtn = By.XPath("//a[@id='saveAll']");
        By AssessmentTabOption = By.XPath("//a[contains(text(),'Assessment')]");
        By PainBtnLink = By.XPath("//a[contains(text(),'Pain')]");
        By OpportunityTabOption = By.XPath("(//a[contains(text(),'Opportunities')])[2]");
        By AddOpportunityButton = By.XPath("//div[@id='divOpportunityCard']//a[contains(@class,'opprotunity-btn')]");
        By PipelineSelectDropdown = By.XPath("//select[@id='ddlPipeline']");
        By SourceDropdown = By.XPath("//select[@name='Source']");
        By TeamDropdown = By.XPath("//select[@name='Team']");
        By CoreSystemDropdown = By.XPath("//select[@name='CoreSystem']");
        By AutomationAlignmentDropdown = By.XPath("//select[@name='Capability']");
        By BusinessBenefitDropdown = By.XPath("//select[@name='BusinessBenefit']");
        By OrganizationGoalsDropdown = By.XPath("//select[@id='txtOrganizationalGoal']");
        By EstimatedRelativeImpactSlider = By.XPath("//label[contains(text(),'Estimated Relative Impact')]//following::span[contains(@class,'irs-handle')][1]");
        By EstimatedRelativeComplexitySlider = By.XPath("//label[contains(text(),'Estimated Relative Complexity')]//following::span[contains(@class,'irs-handle')]");
        By ApprovalDateInputBox = By.XPath("//input[@name='ApprovalDate']");
        By StartDateInputBox = By.XPath("//input[@name='StartDate']");
        By CompletionDateInputBox = By.XPath("//input[@name='CompletionDate']");
        By SaveOpportunityButton = By.XPath("//div[@id='btnSaveOpportunity']//button[1]");
        By OpportunityTitle = By.XPath("//h3[@id='divOppTitle']");
        By DiscoveryTabOption = By.XPath("//span[contains(text(),'Discovery')]");
        By DiscoverAutomationButton = By.XPath("//a[@id='btnDiscoveryTab']");
        By AutomationTaskCheckbox1 = By.XPath("//div[@id='discoveryCards-divOpportunitiesTasks']//input//following::span[1]");
        By AutomationTaskCheckbox2 = By.XPath("(//div[@id='divGettingStartedOpportunities2']//input//following::span)[2]");
        By SaveAutomationToPiplelineButton = By.XPath("//button[contains(text(),'Save Changes')]");
        By SimulationTabOption = By.XPath("//a[@id='tab-Simulation']");
        By RunSimulationButton = By.XPath("//h4[contains(text(),'Run Simulation')]//following::a[contains(text(),'Run Simulation')]");
        By NextSimulationStepButton = By.XPath("//button[@id='nextStep']");
        By ShowHideScenarioBtn = By.XPath("//div[contains(text(),'Future')]//following::div[@data-original-title='Dispaly / hide scenario']");
        By AddNewScenarioBtn = By.XPath("//div[text()='Add Scenario']");
        By ScenarioNameInputBox = By.XPath("//input[@id='newScenarioName']");
        By AddScenarioSaveBtn = By.XPath("(//label[contains(text(),'Scenario name')]//following::div[contains(text(),'Save')])[1]");
        By DeleteScenarioBtn = By.XPath("//div[contains(text(),'Recruit')]//following::div[contains(@class,'deleteScenario')]");
        By ConfirmDeleteBtn = By.XPath("//button[contains(text(),'Yes, delete it!')]");
        By CompareScenarioBtn = By.XPath("(//div[text()='Compare Scenarios'])[1]");
        By CloseCompareScenarioModel = By.XPath("//div[contains(@class,'closeCompareScenarios') and text()='Cancel']");
        By SearchScenarioActivitiesInputBox = By.XPath("(//label[text()='Search:']//input)[1]");
        By EditActivityBtn = By.XPath("(//div[contains(@class,'editStep')]//span)[1]");
        By ActivityTypeDetailDropdown = By.XPath("//select[@id='ddlStepDetailsStepType']");
        By SaveActiviyDetailsBtn = By.XPath("//h4[@id='divEditStepTitle']//following::div[contains(text(),'Save')][1]");
        By AddScheduleButton = By.XPath("//div[contains(text(),'Add Schedule')]");
        By ScheduleNameInputBox = By.XPath("//label[contains(text(),'Schedule Name')]//following::input[1]");
        By SaveScheduleBtn = By.XPath("//button[contains(@class,'saveScheduleBtn')]");
        By ResourceDataTableRows = By.XPath("//table[contains(@class,'resources__table')]//tbody//tr");
        By NumberOfResourceInputBox = By.XPath("//input[@id='txtResourceNumber']");
        By CostOfResourceInputBox = By.XPath("//input[@id='txtResourceCost']");
        By ResourceWorkSchedule = By.XPath("//input[@id='toggleWorkScheduleForResources']//following::label[1]");
        By ResourceScheduleSelectDropdown = By.XPath("//select[@id='resourceModalSchedules']");
        By ResourceCostFrequencyUnitDropdown = By.XPath("//select[@id='ddlResourceCostUnit']");
        By SaveResourceInfoButton = By.XPath("//div[@class='scheduleCardsModalWrapper']//following::div[contains(text(),'Save')][1]");
        By WarmUpTimeInputBox = By.XPath("//input[@id='txtWarmUpTime']");
        By WarmUpTimeUnitDropdown = By.XPath("//select[@id='ddlWarmUpUnit']");
        By NumberOfInstancesSlider = By.XPath("//label[contains(text(),'Number of Instances ')]//following::span[@class='irs-handle single'][1]");
        By ProcessStartDurationSlider = By.XPath("//label[contains(text(),'Process Start Duration')]//following::span[contains(@class,'irs-handle')][1]");
        By SimulationSpeedSlider = By.XPath("(//label[contains(text(),'Simulation Speed')]//following::span[contains(@class,'irs-handle')])[1]");

        [Test]
        public void EndToEndTestCase()
        {
            SelectProcessTemplate();
            EnterProcessDetails();
            PainAndGainAssessment();
            AddOpportunityToProcess();
            EnterOppotunityInfo();
            DiscoverAutomation();
            SimulationScenarioSetup();
            SimulationAlignResources();
            SimulationOptions();
        }

        [TearDown]
        public void StatusOfTestExecuted()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            ExtentTestManager.StatusOfTest();
        }

        public void SelectProcessTemplate()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ProcessSideMenuOption));
            GetActions().MoveToElement(Driver.FindElement(ProcessSideMenuOption)).Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(CreateNewProcessDropdown)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(StartFromTemplateDropdownOption)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(FilterTemplateInputBox));
            GetActions().MoveToElement(Driver.FindElement(FilterTemplateInputBox)).SendKeys("Hiring").Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(GeneralHiringTemplate)).Click();
        }

        public void EnterProcessDetails()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(ProcessNameInputBox)).Clear();
            Driver.FindElement(ProcessNameInputBox).SendKeys("Demo ST");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ProcessOwnerInputBox)).Click();
            Driver.FindElement(ProcessOwnerInputBox).SendKeys("Demo Test");
            GetActions().SendKeys(Keys.Enter).Perform();
            //Select(Driver.FindElement(CapabilityDropdown)).SelectByText("Recruitment");
            Select(Driver.FindElement(CategorySelectDropown)).SelectByText("Guiding");
        }

        public void PainAndGainAssessment()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(AssessmentTabOption)).Click();
            IList<IWebElement> AllGainDropdowns = Driver.FindElements(By.XPath("//select[contains(@class,'gain-select2')]"));
            GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//h4[contains(text(),'Gain Assessment')]"), "Gain Assessment"));
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(By.XPath("//table[@id='tblGaint']")));

            int GainDropdownsCount = AllGainDropdowns.Count;
            for (int i = 1; i <= GainDropdownsCount; i++)
            {
                Select(Driver.FindElement(By.XPath(string.Format(GainDropdowns, i)))).SelectByText(GainGoals[i]);
            }

            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(PainBtnLink)).Click();
            IList<IWebElement> AllPainDropdowns = Driver.FindElements(By.XPath("//select[contains(@class,'pain-select2')]"));
            GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//h4[contains(text(),'Pain Assessment')]"), "Pain Assessment"));
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(By.XPath("//table[@id='tblGaint']")));
            int PainDropdownsCount = AllPainDropdowns.Count;
            for (int i = 1; i <= PainDropdownsCount; i++)
            {
                Select(Driver.FindElement(By.XPath(string.Format(PaindDropdowns, i)))).SelectByText(PainGoals[i]);
            }
            Driver.FindElement(SaveAllBtn).Click();
            Wait(4);
        }

        public void AddOpportunityToProcess()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(OpportunityTabOption));
            Driver.FindElement(OpportunityTabOption).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(AddOpportunityButton));
            Driver.FindElement(AddOpportunityButton).Click();
        }

        public void EnterOppotunityInfo()
        {
            GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//h3[@id='divOppTitle']"), "Demo ST"));
            Select(Driver.FindElement(PipelineSelectDropdown)).SelectByText("Demo");
            Select(Driver.FindElement(SourceDropdown)).SelectByText("Analysis");
            Select(Driver.FindElement(TeamDropdown)).SelectByText("Human Resources");
            Select(Driver.FindElement(CoreSystemDropdown)).SelectByText("Email");
            Select(Driver.FindElement(AutomationAlignmentDropdown)).SelectByText("Workflow");
            Select(Driver.FindElement(BusinessBenefitDropdown)).SelectByText("Hours to the Business");
            Select(Driver.FindElement(BusinessBenefitDropdown)).SelectByText("Accuracy");
            Select(Driver.FindElement(OrganizationGoalsDropdown)).SelectByText("Have a skilled sales force");
            IWebElement RelativeImpactSlider = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(EstimatedRelativeImpactSlider));
            GetActions().ClickAndHold(RelativeImpactSlider).MoveByOffset(30, 0).MoveByOffset(30, 0).Release().Perform();
            IWebElement RealtiveComplexitySlider = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(EstimatedRelativeComplexitySlider));
            GetActions().ClickAndHold(RealtiveComplexitySlider).MoveByOffset(-30, 0).MoveByOffset(-30, 0).Release().Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(ApprovalDateInputBox)).Clear();
            Driver.FindElement(ApprovalDateInputBox).SendKeys(GetDateAfterDays(10));
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(StartDateInputBox)).Clear();
            Driver.FindElement(StartDateInputBox).SendKeys(GetDateAfterDays(20));
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(CompletionDateInputBox)).Clear();
            Driver.FindElement(CompletionDateInputBox).SendKeys(GetDateAfterDays(50));
            SaveOpprotunity();
        }

        public void DiscoverAutomation()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(DiscoveryTabOption)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(DiscoverAutomationButton)).Click();
            Wait(4);
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(AutomationTaskCheckbox1)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(AutomationTaskCheckbox2)).Click();
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(SaveAutomationToPiplelineButton));
            Driver.FindElement(SaveAutomationToPiplelineButton).Click();
            SaveOpprotunity();
        }

        public void SimulationScenarioSetup()
        {
            Wait(2);
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SimulationTabOption)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(RunSimulationButton)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(NextSimulationStepButton)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(SimulationAdvanceToggleSwitch, "toggleAdvanced-1")))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ShowHideScenarioBtn)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ShowHideScenarioBtn)).Click();

            // Add new scenario
            Driver.FindElement(AddNewScenarioBtn).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ScenarioNameInputBox)).SendKeys("Recruit");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(AddScenarioSaveBtn)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(DeleteScenarioBtn)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ConfirmDeleteBtn)).Click();

            // Compare scenario
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(CompareScenarioBtn)).Click();
            ExecuteJavaScript().ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(CloseCompareScenarioModel));
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(CloseCompareScenarioModel));
            GetActions().Click(Driver.FindElement(CloseCompareScenarioModel)).Perform();

            // Select and deselect activities
            for (int i = 0; i < SimulationActivities.Length; i++)
            {
                GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(ScenarioActivitiesCheckbox, SimulationActivities[i])))).Click();
            }

            // Search scenario activities and add details
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SearchScenarioActivitiesInputBox)).SendKeys("Research salaries");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(EditActivityBtn)).Click();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(ActivityDetailTimeInputBox, "txtWorkTime1"))));
            ExecuteJavaScript().ExecuteScript("arguments[0].value=''", Element);
            Element.SendKeys("1");
            Select(Driver.FindElement(By.XPath(string.Format(ActivityDetailTimeUnitDropdown, "ddlWorkTimeUnit")))).SelectByText("Weeks");
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(ActivityDetailTimeInputBox, "txtWaitTime1"))));
            ExecuteJavaScript().ExecuteScript("arguments[0].value=''", Element);
            Element.SendKeys("3");
            Select(Driver.FindElement(By.XPath(string.Format(ActivityDetailTimeUnitDropdown, "ddlWaitTimeUnit")))).SelectByText("Weeks");
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(ActivityDetailTimeInputBox, "txtCost1"))));
            ExecuteJavaScript().ExecuteScript("arguments[0].value=''", Element);
            Element.SendKeys("50");
            Select(Driver.FindElement(By.XPath(string.Format(ActivityDetailTimeUnitDropdown, "ddlCostUnit")))).SelectByText("USD");
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(ActivityDetailTimeInputBox, "txtStepDetails1"))));
            ExecuteJavaScript().ExecuteScript("arguments[0].value=''", Element);
            Element.SendKeys("3");
            Select(Driver.FindElement(By.XPath(string.Format(ActivityDetailTimeUnitDropdown, "ddlStepDetailsUnit")))).SelectByText("Weeks");
            Select(Driver.FindElement(ActivityTypeDetailDropdown)).SelectByText("Automatic");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SaveActiviyDetailsBtn)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(NextSimulationStepButton)).Click();
        }

        public void SimulationAlignResources()
        {
            // Add new schedule
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(Driver.FindElement(By.XPath(string.Format(SimulationAdvanceToggleSwitch, "toggleAdvanced-2"))))).Click();
            Wait(2);
            GetActions().MoveToElement(Driver.FindElement(AddScheduleButton)).Click().Perform();
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(ScheduleNameInputBox)).Clear();
            Driver.FindElement(ScheduleNameInputBox).SendKeys("Demo Schedule");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(SimulationScheduleMonths, "August")))).Click();
            Select(Driver.FindElement(By.XPath(string.Format(SimulationWorkingTime, "pickHoursFrom")))).SelectByText("10:00");
            Select(Driver.FindElement(By.XPath(string.Format(SimulationWorkingTime, "pickHoursTo")))).SelectByText("18:00");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(SimulationWorkingDays, "pickDaysSaturday")))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SaveScheduleBtn)).Click();

            // Duplicate schedule
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(DuplicateScheduleBtn, "Temporary Schedule", "1")))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(EditScheduleBtn, "Temporary Schedule", "3")))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(ScheduleNameInputBox)).Clear();
            Driver.FindElement(ScheduleNameInputBox).SendKeys("Demo Schedule 2");
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SaveScheduleBtn)).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format(DeleteScheduleBtn, "Demo Schedule 2", "1")))).Click();
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ConfirmDeleteBtn)).Click();

            // Assign work schedule to resources
            int NumberOfResourceRows = Driver.FindElements(ResourceDataTableRows).Count;
            for (int i = 1; i <= NumberOfResourceRows; i++)
            {
                string ResourceName = Driver.FindElement(By.XPath(string.Format(NameOfResource, i))).Text;
                GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(Driver.FindElement(By.XPath(string.Format(ResourceEditButton, i))))).Click();
                Wait(2);
                ExecuteJavaScript().ExecuteScript("arguments[0].value=''", Driver.FindElement(NumberOfResourceInputBox));
                Driver.FindElement(NumberOfResourceInputBox).SendKeys(NumberOfResources[i]);
                ExecuteJavaScript().ExecuteScript("arguments[0].value=''", Driver.FindElement(CostOfResourceInputBox));
                Driver.FindElement(CostOfResourceInputBox).SendKeys(CostOfResources[i]);
                if (ResourceName == "Hiring Manager")
                {
                    GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ResourceCostFrequencyUnitDropdown));
                    Select(Driver.FindElement(ResourceCostFrequencyUnitDropdown)).SelectByText("Per Day");
                    GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ResourceWorkSchedule)).Click();
                }
                else if (ResourceName == "HR Manager")
                {
                    GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ResourceScheduleSelectDropdown));
                    Select(Driver.FindElement(ResourceScheduleSelectDropdown)).SelectByText("Demo Schedule");
                }
                GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SaveResourceInfoButton)).Click();
            }
        }

        public void SimulationOptions()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(NextSimulationStepButton)).Click();

            // Set warm-up between activities
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(Driver.FindElement(By.XPath(string.Format(SimulationAdvanceToggleSwitch, "toggleAdvanced-3"))))).Click();
            Wait(2);
            GetWebDriverWait().Until(ExpectedConditions.ElementExists(WarmUpTimeInputBox)).Clear();
            Driver.FindElement(WarmUpTimeInputBox).SendKeys("1");
            Select(Driver.FindElement(WarmUpTimeUnitDropdown)).SelectByText("Minutes");
            
            // Set simulation start
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(NumberOfInstancesSlider));
            GetActions().MoveToElement(Element).Click().SendKeys(Keys.ArrowRight).Perform();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(ProcessStartDurationSlider));
            GetActions().MoveToElement(Element).Click().SendKeys(Keys.ArrowLeft).Perform();
            Element = GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SimulationSpeedSlider));
            GetActions().MoveToElement(Element).Click().SendKeys(Keys.ArrowRight).SendKeys(Keys.ArrowRight).Perform();
            Wait(1);
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(NextSimulationStepButton)).Click();
        }

        public void SaveOpprotunity()
        {
            GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(SaveOpportunityButton)).Click();
            GetWebDriverWait().Until(ExpectedConditions.TextToBePresentInElementLocated(OpportunityTitle, "Demo ST"));
            Wait(5);
        }
    }
}