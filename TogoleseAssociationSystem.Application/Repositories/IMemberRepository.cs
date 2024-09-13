using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member> GetMemberByIdAsync(Guid id);        
          Task<Member> RetrieveMember(string firsname, string lastname);     
        Task<IEnumerable<HasRole>> GetMemberRolesByIdAsync(Guid memberId);
        Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(Guid memberId);
        bool SaveChanges();
        Task<IEnumerable<MembershipContribution>> GetContributionsAsync();
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void CreateMembership(MembershipContribution membership);
        Task<MembershipContribution> GetMembershipByIdAsync(Guid id);
        Task<IEnumerable<Member>> GetAllExisitingMembersAsync();
    }
}
