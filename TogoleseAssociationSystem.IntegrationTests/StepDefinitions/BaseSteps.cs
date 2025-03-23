using TogoleseSolidarity.IntegrationTests.Helpers;

namespace TogoleseSolidarity.IntegrationTests.StepDefinitions
{
    public class BaseSteps
    {
        private string BaseUrl { get; }
        protected BaseSteps()
        {
            BaseUrl = $"{ConfigurationHelper.GetSetting("BaseUiUrl")}/member";
        }

        protected void NavigateToPage(string page)
        {
            BrowserHelper.WebDriver.Navigate().GoToUrl($"{BaseUrl}/{page}");
        }
    }
}
