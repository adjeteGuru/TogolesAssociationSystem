namespace TogoleseAssociationSystem.Domain.Models
{

    public class Claim : BaseEntity
    {
        //public Claim()
        //{
        //    ClaimRemain = Math.Max(0, TotalClaimPerMember - CurrentClaim);
        //}

        public string? Name { get; set; }
        public string? Description { get; set; }        
        public Guid MemberId { get; set; }
        public ClaimType ClaimType { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinContact { get; set; }
        public DateTime? ClaimDate { get; set; } = DateTime.Today;
        //public int ClaimRemain { get; set; }
        //public int TotalClaimPerMember { get; set; }

        //public int ClaimRemain { get; private set; }
        //public bool IsEligibleForClaim => ClaimRemain > 0;
        //private int totalClaimPerMember = 2;
        //public int TotalClaimPerMember
        //{
        //    get => totalClaimPerMember;
        //    set
        //    {
        //        totalClaimPerMember = value;
        //        ClaimRemain = Math.Max(0, totalClaimPerMember - CurrentClaim);
        //    }
        //}

        //private int currentClaim;
        //public int CurrentClaim
        //{
        //    get => currentClaim;
        //    set
        //    {
        //        currentClaim = value;
        //        ClaimRemain = Math.Max(0, TotalClaimPerMember - currentClaim);
        //    }
        //}
    }
}
