using BoDi;
using System.Text;
using TogoleseAssociationSystem.IntegrationTests.Constants;
using TogoleseAssociationSystem.IntegrationTests.Helpers;

namespace TogoleseAssociationSystem.IntegrationTests.StepDefinitions
{
    [Binding]
    public class ActionStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IObjectContainer objectContainer;
        private readonly DatabaseHelper databaseHelper;

        public ActionStepDefinitions(ScenarioContext scenarioContext, IObjectContainer objectContainer)
        {
            this.scenarioContext = scenarioContext;
            this.objectContainer = objectContainer;
            databaseHelper = new DatabaseHelper();
        }
        [When(@"the user sends request to get members")]
        public async Task WhenTheUserSendsRequestToGetMembers()
        {
            //var webRequestHelper = objectContainer.Resolve<WebRequestHelper>();
            //var uri = new Uri("/api/member");
            //var content = new StringContent(string.Empty, Encoding.UTF8, "text/plain");
            //var response = await webRequestHelper.PostAsync(uri, content);
            //scenarioContext.Add(ScenarioContextKeys.Response, response);
        }


        [When(@"the user creates a member with the following properties")]
        public void WhenTheUserCreatesAMemberWithTheFollowingProperties(Table table)
        {
            //BaseSteps.NavigateToPage(TestConstants.UiRoutes.MemberGrid);
        }
    }
}
