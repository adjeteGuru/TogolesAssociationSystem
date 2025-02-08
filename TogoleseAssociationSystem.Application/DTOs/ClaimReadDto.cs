using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.DTOs
{
    public class ClaimReadDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TotalClaimPerMember { get; set; }
        public int CurrentClaim { get; set; }
        public int ClaimRemain { get; set; }
        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinContact { get; set; }
        public DateTime? ClaimDate { get; set; }
        public ClaimType ClaimType { get; set; }
    }
}
