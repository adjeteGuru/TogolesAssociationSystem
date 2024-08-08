namespace TogoleseAssociationSystem.Core.DTOs
{
    public class MembershipContributionToAdd
    {
        public string? ContributionName { get; set; }       
        public decimal Amount { get; set; }
        public DateTime? DateOfContribution { get; set; }
        public bool IsAnnualContribution { get; set; }
        public Guid MemberId { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
    }
}
