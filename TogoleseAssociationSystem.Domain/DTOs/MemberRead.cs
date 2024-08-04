namespace TogoleseAssociationSystem.Domain.DTOs
{
    public class MemberRead
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsChair { get; set; } = false;
        public string? PhotoUrl { get; set; } = "https://via.placeholder.com/300x300";
        public DateTime? MembershipDate { get; set; }      
        public List<MembershipContributionRead>? Memberships { get; set; } = new List<MembershipContributionRead>();
        public List<HasRoleRead>? HasRoles { get; set; } = new List<HasRoleRead>();
    }
}
