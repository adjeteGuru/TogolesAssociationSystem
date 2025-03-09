using TogoleseAssociationSystem.Core.DTOs;

namespace TogoleseAssociationSystem.Core.ServiceProvider.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberRead>> GetMembersAsync(int currentPage, int itemsPerPage, string? filter);
        Task<IEnumerable<MemberRead>> GetAllExistingMembersAsync();
        Task<IEnumerable<MembershipContributionReadDto>> GetAllMembershipsAsync();
        Task<MemberRead> GetMemberByIdAsync(Guid id);       
        Task<MemberRead> CreateMemberAsync(MemberToAdd memberToAdd);
        Task<MemberRead> UpdateMemberDetails(MemberUpdateDto member);
        Task<MembershipContributionReadDto> CreateMembershipAsync(MembershipContributionToAdd contributionToAdd);
        Task<ClaimReadDto> CreateClaimAsync(ClaimToAdd claimToAdd);

        Task<ClaimReadDto> GetClaimByIdAsync(Guid id);
        Task UpdateClaimAsync(ClaimToUpdate claim);
    }
}
