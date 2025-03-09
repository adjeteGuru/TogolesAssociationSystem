using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.DTOs
{
    public class ClaimToUpdate
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid MemberId { get; set; }
        public ClaimType ClaimType { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinContact { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
    }
}
