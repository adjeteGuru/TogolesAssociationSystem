namespace TogoleseAssociationSystem.Domain.Models
{
    public class MembershipContribution : BaseEntity
    {      
        public string? ContributionName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOfContribution { get; set; }
        public Guid MemberId { get; set; }
    }
}
