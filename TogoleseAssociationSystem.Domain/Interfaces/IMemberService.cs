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
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void CreateMembership(MembershipContribution membership);
        Task<IEnumerable<Member>> GetAllExisitingMembersAsync();
        bool SaveChanges();
    }
}
