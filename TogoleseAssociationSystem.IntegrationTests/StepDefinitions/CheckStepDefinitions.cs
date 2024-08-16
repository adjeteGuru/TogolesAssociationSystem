using AutoMapper.Execution;
using Newtonsoft.Json;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TogoleseAssociationSystem.IntegrationTests.Constants;

namespace TogoleseAssociationSystem.IntegrationTests.StepDefinitions
{
    [Binding]
    public class CheckStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public CheckStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        [Then(@"the response with '([^']*)' status code is returned")]
        public void ThenTheResponseWithStatusCodeIsReturned(HttpStatusCode statusCode)
        {
            var response = scenarioContext.Get<HttpResponseMessage>(ScenarioContextKeys.Response);
            response.StatusCode.Should().Be(statusCode);
        }

        [Then(@"the list of members is returned")]
        public async Task ThenTheListOfMembersIsReturned()
        {
            var response = scenarioContext.Get<HttpResponseMessage>(ScenarioContextKeys.Response);
            var content = await response.Content.ReadAsStringAsync();
            var members = JsonConvert.DeserializeObject<IEnumerable<Member>>(content);
            members.Should().NotBeNull();
            members.Count().Should().Be(1);
        }

        [Then(@"an alert message indicating that the member with name '([^']*)'was successfully created is displayed")]
        public void ThenAnAlertMessageIndicatingThatTheMemberWithNameWasSuccessfullyCreatedIsDisplayed(string test)
        {
            throw new PendingStepException();
        }

        [Then(@"the Member with the following properties is displayed on the list page")]
        public void ThenTheMemberWithTheFollowingPropertiesIsDisplayedOnTheListPage(Table table)
        {
            throw new PendingStepException();
        }
    }
}
