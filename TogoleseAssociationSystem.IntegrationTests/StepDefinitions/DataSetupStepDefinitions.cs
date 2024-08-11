using TogoleseAssociationSystem.IntegrationTests.Helpers;

namespace TogoleseAssociationSystem.IntegrationTests.StepDefinitions
{
    [Binding]
    public class DataSetupStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly DatabaseHelper databaseHelper;

        public DataSetupStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            databaseHelper = new DatabaseHelper();
        }       

        [Given(@"the following members exist")]
        public async Task GivenTheFollowingMembersExist(Table table)
        {            
            await databaseHelper.CreateMembersAsync(table);
        }
    }
}
