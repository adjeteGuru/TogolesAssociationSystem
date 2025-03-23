using TogoleseSolidarity.Core.DTOs;

namespace TogoleseSolidarity.Core.ServiceProvider.Interfaces;

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
    Task<IEnumerable<ClaimReadDto>> GetClaimsByMemberIdAsync(Guid id);
    Task UpdateClaimAsync(ClaimToUpdate claim);
}
