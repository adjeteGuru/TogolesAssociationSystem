using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogoleseSolidarity.Domain.Models;
using TogoleseSolidarity.IntegrationTests.Constants;
using TogoleseSolidarity.IntegrationTests.Pages;

namespace TogoleseSolidarity.IntegrationTests.StepDefinitions
{
    public class BaseMemberCreationSteps :BaseSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly BaseMemberCreationSidebar memberCreationSidebar;

        public BaseMemberCreationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            memberCreationSidebar = new BaseMemberCreationSidebar();
        }

        protected void AddMembershipContributions(Member member)
        {
            foreach (var membership in member.Memberships)
            {
                memberCreationSidebar.ContributionNameInput.SendKeys(membership.ContributionName);
                memberCreationSidebar.ContributionNameInput.Click();
                WaitForSelectableMembershipContribution();
            }
        }

        protected void AddMembership(string dataType)
        {
            var id = scenarioContext.Get<List<string>>(TestConstants.ScenarioContextKeys.AllCreatedMembers)
                .Single(x => x.StartsWith(dataType, StringComparison.InvariantCultureIgnoreCase));
        }

        protected void WaitForSelectableMembershipContribution() 
        { }

    }
}
