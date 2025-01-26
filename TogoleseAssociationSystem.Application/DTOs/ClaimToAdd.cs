using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.DTOs
{
    public class ClaimToAdd
    {
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
