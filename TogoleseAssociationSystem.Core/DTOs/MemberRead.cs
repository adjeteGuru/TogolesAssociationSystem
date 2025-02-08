namespace TogoleseAssociationSystem.Core.DTOs
{
    public class MemberRead
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
        public string? City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsChair { get; set; } = false;
        public byte[]? PhotoUrl { get; set; }
        public DateTime? MembershipDate { get; set; }
        public string? NextOfKin { get; set; }
        public string? NextOfKinContact { get; set; }
        public string? Relationship { get; set; }
        public List<MembershipContributionReadDto>? Memberships { get; set; }   
        public List<ClaimReadDto>? Claims { get; set; }
        public MemberRead()
        {
            Memberships = new List<MembershipContributionReadDto>();
            Claims = new List<ClaimReadDto>();
        }
    }
}
