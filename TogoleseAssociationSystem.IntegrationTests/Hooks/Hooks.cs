using TogoleseSolidarity.IntegrationTests.Constants;
using TogoleseSolidarity.IntegrationTests.Helpers;

namespace TogoleseSolidarity.IntegrationTests.Hooks
{
    public class Hooks
    {
        [BeforeTestRun]
        public static void EnsureMembershipContributionExistInDatabase()
        {
            DatabaseHelper.EnsureMembershipContributionExistInDatabase("Annuals", "Funerals");
        }

        [BeforeScenario(Order = 0)]
        public void StartWebBrower()
        {
            BrowserHelper.StartBrowser();
            BrowserHelper.WebDriver.Manage().Cookies.DeleteAllCookies();
            BrowserHelper.WebDriver.Navigate().GoToUrl(ConfigurationHelper.GetSetting("BaseUiUrl"));
        }
        [BeforeScenario(Order = 1)]
        public void CreateListsForCreatedMembers(ScenarioContext scenarioContext)
        {
            scenarioContext.Add(TestConstants.ScenarioContextKeys.AllCreatedMembers, new List<string>());

        }

        [AfterScenario(Order = 0)]
        public void StopWebBrower(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                ScreenshotHelper.TakeScreenshot(scenarioContext);
            }
            BrowserHelper.ClearCookies();
            BrowserHelper.StopBrowser();
        }

        [AfterScenario(Order = 1)]
        public async Task RemoveMemberFromTheDatabase(ScenarioContext scenarioContext)
        {
            var createdMembers = scenarioContext.Get<List<string>>(TestConstants.ScenarioContextKeys.AllCreatedMembers);
            foreach (var createdMember in createdMembers)
            {
                await DatabaseHelper.RemoveTheMemberDataByPartialName(createdMember);
            }
        }

        [AfterTestRun]
        public static void CloseDatabaseConnection()
        {
            DatabaseHelper.CloseConnection();
        }
    }
}
