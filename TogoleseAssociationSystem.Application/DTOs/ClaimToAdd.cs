using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.DTOs
{
    public class ClaimToAdd
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ClaimRemain { get; private set; }
        public bool IsEligibleForClaim => ClaimRemain > 0;
        public Guid MemberId { get; set; }
        public ClaimType ClaimType { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberLastName { get; set; }
        public string? NextOfKinName { get; set; }

        public DateTime? ClaimDate { get; set; } = DateTime.Today;
        public string? NextOfKinContact { get; set; }

        private int totalClaimPerMember = 2;
        public int TotalClaimPerMember
        {
            get => totalClaimPerMember;
            set
            {
                totalClaimPerMember = value;
                ClaimRemain = Math.Max(0, totalClaimPerMember - CurrentClaim);
            }
        }

        private int currentClaim;
        public int CurrentClaim
        {
            get => currentClaim;
            set
            {
                currentClaim = value;
                ClaimRemain = Math.Max(0, TotalClaimPerMember - currentClaim);
            }
        }
    }
}
