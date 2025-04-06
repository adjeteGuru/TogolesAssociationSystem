using TogoleseSolidarity.Domain.Models;

namespace TogoleseSolidarity.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member?> GetMemberByIdAsync(Guid id);
        Task<Member?> RetrieveMember(string firsname, string lastname);
        Task CreateMember(Member member);
        void UpdateMember(Member member);
        Task<IEnumerable<Member>> GetAllExisitingMembersAsync();
        Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(Guid memberId);
        bool SaveChanges();
        Task<IEnumerable<MembershipContribution>> GetContributionsAsync();

        Task CreateMembership(MembershipContribution membership);
        Task<MembershipContribution?> GetMembershipByIdAsync(Guid id);
        Task CreateClaimAsync(Claim claim);
        Task<Claim?> GetClaimByIdAsync(Guid id);
        Task<IEnumerable<Claim>> GetClaimsAsync();
        Task<IEnumerable<Claim>> GetClaimsByMemberIdAsync(Guid memberId);
    }
}
