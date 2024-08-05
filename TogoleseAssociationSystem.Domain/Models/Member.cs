namespace TogoleseAssociationSystem.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
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
        public string? PhotoUrl { get; set; } = "https://via.placeholder.com/300x300";
        public DateTime? MembershipDate { get; set; }
        public List<MembershipContribution>? Memberships { get; set; } = new List<MembershipContribution>();
        public List<HasRole>? HasRoles { get; set; } = new List<HasRole>();
    }
}
