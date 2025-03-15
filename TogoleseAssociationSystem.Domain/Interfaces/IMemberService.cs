using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Domain.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member> GetMemberByIdAsync(Guid id);
        Task<MembershipContribution> GetMembershipByIdAsync(Guid id);
        Task<Member> RetrieveMember(string firsname, string lastname);      
        Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(Guid memberId);
        Task<IEnumerable<MembershipContribution>> GetContributionsAsync();
        Task CreateClaimAsync(Claim claim);
        void UpdateMember(Member member);
        void CreateMembership(MembershipContribution membership);
        Task<IEnumerable<Member>> GetAllExisitingMembersAsync();      
        Task<Claim> GetClaimByIdAsync(Guid id);
        Task<IEnumerable<Claim>> GetClaimsAsync();
        bool SaveChanges();
        void CreateMember(Member member);
        Task<IEnumerable<Claim>> GetClaimsByMemberIdAsync(Guid id);
    }
}
