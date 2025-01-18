namespace TogoleseAssociationSystem.Domain.Models
{
    public class Claim : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }        
        public int TotalClaimPerMember { get; set; } = 3;
        public int CurrentClaim { get; set; }
        public int ClaimRemain { get; set; }
        public Guid MemberId { get; set; }

        //•	If TotalClaimPerMember is 3 and CurrentClaim is 1, then ClaimRemain will be 2.
        //•	If TotalClaimPerMember is 3 and CurrentClaim is 2, then ClaimRemain will be 1.
        //•	If TotalClaimPerMember is 3 and CurrentClaim is 3, then ClaimRemain will be 0.
        //•	If TotalClaimPerMember is 3 and CurrentClaim is 4, then ClaimRemain will be 0 (since the member has exceeded the allowed claims).
        public void ClaimRemainCalculation()
        {

            ClaimRemain = Math.Max(0, TotalClaimPerMember - CurrentClaim);
        }
    }
}
