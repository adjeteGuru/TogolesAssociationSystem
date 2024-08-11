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
    }
}
