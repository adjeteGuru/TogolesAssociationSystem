namespace TogoleseAssociationSystem.Domain.Models
{
    public class Member : BaseEntity
    {       
        public string? Title { get; set; }    
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
        public string? City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsEligibleToClaim { get; set; } = true;
        public byte[]? PhotoUrl { get; set; }
        public string? NextOfKin { get; set; }
        public string? NextOfKinContact { get; set; }
        public string? Relationship { get; set; }
        public DateTime? MembershipDate { get; set; }
        public List<Claim>? Claims { get; set; }
        public List<MembershipContribution>? Memberships { get; set; }      
        public Member()
        {
            Memberships = new List<MembershipContribution>();
            Claims = new List<Claim>();
        }
    }
}
