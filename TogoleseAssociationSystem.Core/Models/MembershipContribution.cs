namespace TogoleseAssociationSystem.Core.Models
{
    public class MembershipContribution : BaseEntity
    {
        //public int Id { get; set; }
        public string? ContributionName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOfContribution { get; set; }
        public bool? IsAnnualContribution { get; set; }
        public Guid MemberId { get; set; }       
    }
}
