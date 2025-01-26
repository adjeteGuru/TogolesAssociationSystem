using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Core.DTOs
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
        public ClaimType ClaimType { get; set; }
    }
}
