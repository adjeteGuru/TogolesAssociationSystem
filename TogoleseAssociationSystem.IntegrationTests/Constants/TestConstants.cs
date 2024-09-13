namespace TogoleseAssociationSystem.IntegrationTests.Constants
{
    public static class TestConstants
    {
        public static class ScenarioContextKeys
        {
            public const string AllCreatedMembers = "allCreatedMembers";
        }

        public static class UiRoutes
        {
            public const string MemberGrid = "member";
            public const string MemberDetails = "member/details";
        }

        public static class Helpers
        {
            public const int ShortWaitSeconds = 30;
            public const int MediumWaitSeconds = 60;
            public const int DurationBetweenRetriesMilliseconds = 500;

            public const int MaxRetry = 10;
        }

        public static class AlertMessages
        {
            public const string MemberCreated = "Member {0} is successfully created";
        }
    }
}
