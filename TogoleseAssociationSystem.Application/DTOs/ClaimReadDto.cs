namespace TogoleseAssociationSystem.Application.DTOs
{
    public class ClaimReadDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TotalClaimPerMember { get; set; } = 3;
        public int CurrentClaim { get; set; }
        public int ClaimRemain { get; set; }
        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }
    }
}
