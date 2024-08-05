namespace TogoleseAssociationSystem.Domain.DTOs
{
    public class MembershipContributionRead
    {
        public int Id { get; set; }
        public string? ContributionName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOfContribution { get; set; }
        public bool? IsAnnualContribution { get; set; }
        public int MemberId { get; set; }
        public string? MemberName { get; set; }       
    }
}
