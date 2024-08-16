using System.Globalization;
using TechTalk.SpecFlow.Assist;
using TogoleseAssociationSystem.Domain.Models;
using TogoleseAssociationSystem.IntegrationTests.Helpers;

namespace TogoleseAssociationSystem.IntegrationTests.StepDefinitions
{
    [Binding]
    public class DataSetupStepDefinitions : BaseMemberCreationSteps
    {
        private readonly ScenarioContext scenarioContext;
       
        public DataSetupStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            this.scenarioContext = scenarioContext;           
        }       

        [Given(@"the following members exist")]
        public async Task GivenTheFollowingMembersExist(Table table)
        {            
           
        }

        [Given(@"the following contribution to associate with member exist")]
        public void GivenTheFollowingContributionToAssociateWithMemberExist(Table table)
        {
            var member = table.CreateInstance<Member>();
            var id = Guid.NewGuid();
            //member.MembershipDate = DateTime.UtcNow;
            DatabaseHelper.InsertMember(member, id, scenarioContext);
        }      
    }
}
