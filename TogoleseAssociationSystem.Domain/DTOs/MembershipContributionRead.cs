namespace TogoleseAssociationSystem.Domain.DTOs
{
    public class MembershipContributionRead
    {
        public Guid Id { get; set; }
        public string? ContributionName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOfContribution { get; set; }
        public bool? IsAnnualContribution { get; set; }
        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }       
    }
}
